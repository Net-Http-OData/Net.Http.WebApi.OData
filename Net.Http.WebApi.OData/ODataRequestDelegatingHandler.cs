// -----------------------------------------------------------------------
// <copyright file="ODataRequestDelegatingHandler.cs" company="Project Contributors">
// Copyright Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Net.Http.OData;

namespace Net.Http.WebApi.OData
{
    /// <summary>
    /// An <see cref="DelegatingHandler"/> which parses the OData request.
    /// </summary>
    public sealed class ODataRequestDelegatingHandler : DelegatingHandler
    {
        private readonly ODataServiceOptions _odataServiceOptions;

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataRequestDelegatingHandler"/> class.
        /// </summary>
        /// <param name="odataServiceOptions">The <see cref="ODataServiceOptions"/> for the service.</param>
        public ODataRequestDelegatingHandler(ODataServiceOptions odataServiceOptions) => _odataServiceOptions = odataServiceOptions;

        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            ODataRequestOptions requestOptions = default;

            if (request?.IsODataRequest() == true)
            {
                try
                {
                    requestOptions = new ODataRequestOptions(
                        ODataUtility.ODataServiceRootUri(request.RequestUri.Scheme, request.RequestUri.Host, request.RequestUri.LocalPath),
                        ReadIsolationLevel(request),
                        ReadMetadataLevel(request),
                        ReadODataVersion(request));

                    request.Properties.Add(typeof(ODataRequestOptions).FullName, requestOptions);
                }
                catch (ODataException exception)
                {
                    return request.CreateODataErrorResponse(exception);
                }
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (requestOptions != null)
            {
                if (!request.IsODataMetadataRequest())
                {
                    response.Content?.Headers.ContentType.Parameters.Add(requestOptions.MetadataLevel.ToNameValueHeaderValue());
                }

                response.Headers.Add(ODataResponseHeaderNames.ODataVersion, requestOptions.Version.ToString());
            }

            return response;
        }

        private static string ReadHeaderValue(HttpRequestMessage request, string name)
            => request.Headers.TryGetValues(name, out IEnumerable<string> values) ? values.FirstOrDefault() : default;

        private ODataIsolationLevel ReadIsolationLevel(HttpRequestMessage request)
        {
            ODataIsolationLevel odataIsolationLevel = ODataIsolationLevel.None;

            string headerValue = ReadHeaderValue(request, ODataRequestHeaderNames.ODataIsolation);

            if (headerValue != null)
            {
                if (headerValue == "Snapshot")
                {
                    odataIsolationLevel = ODataIsolationLevel.Snapshot;
                }
                else
                {
                    throw ODataException.BadRequest($"If specified, the {ODataRequestHeaderNames.ODataIsolation} must be 'Snapshot'.");
                }
            }

            if (!_odataServiceOptions.SupportedIsolationLevels.Contains(odataIsolationLevel))
            {
                throw ODataException.PreconditionFailed($"{ODataRequestHeaderNames.ODataIsolation} '{headerValue}' is not supported by this service.");
            }

            return odataIsolationLevel;
        }

        private ODataMetadataLevel ReadMetadataLevel(HttpRequestMessage request)
        {
            ODataMetadataLevel odataMetadataLevel = ODataMetadataLevel.Minimal;

            foreach (MediaTypeWithQualityHeaderValue header in request.Headers.Accept)
            {
                if (!_odataServiceOptions.SupportedMediaTypes.Contains(header.MediaType))
                {
                    throw ODataException.UnsupportedMediaType(
                        $"A supported MIME type could not be found that matches the acceptable MIME types for the request. The supported type(s) 'application/json;odata.metadata=none, application/json;odata.metadata=minimal, application/json, text/plain' do not match any of the acceptable MIME types '{header.MediaType}'.");
                }

                foreach (NameValueHeaderValue parameter in header.Parameters)
                {
                    if (parameter.Name == ODataMetadataLevelExtensions.HeaderName)
                    {
                        switch (parameter.Value)
                        {
                            case "none":
                                odataMetadataLevel = ODataMetadataLevel.None;
                                break;

                            case "minimal":
                                odataMetadataLevel = ODataMetadataLevel.Minimal;
                                break;

                            case "full":
                                odataMetadataLevel = ODataMetadataLevel.Full;
                                break;

                            default:
                                throw ODataException.BadRequest(
                                    $"If specified, the {ODataMetadataLevelExtensions.HeaderName} value in the Accept header must be 'none', 'minimal' or 'full'.");
                        }
                    }
                }
            }

            if (!_odataServiceOptions.SupportedMetadataLevels.Contains(odataMetadataLevel))
            {
                throw ODataException.BadRequest(
#pragma warning disable CA1308 // Normalize strings to uppercase
                    $"{ODataMetadataLevelExtensions.HeaderName} '{odataMetadataLevel.ToNameValueHeaderValue().Value}' is not supported by this service, the metadata levels supported by this service are '{string.Join(", ", _odataServiceOptions.SupportedMetadataLevels.Select(x => x.ToString().ToLowerInvariant()))}'.");
#pragma warning restore CA1308 // Normalize strings to uppercase
            }

            return odataMetadataLevel;
        }

        private ODataVersion ReadODataVersion(HttpRequestMessage request)
        {
            string headerValue = ReadHeaderValue(request, ODataRequestHeaderNames.ODataMaxVersion);

            if (headerValue != null)
            {
                if (ODataVersion.TryParse(headerValue, out ODataVersion odataVersion) && odataVersion >= _odataServiceOptions.MinVersion && odataVersion <= _odataServiceOptions.MaxVersion)
                {
                    return odataVersion;
                }
                else
                {
                    throw ODataException.BadRequest(
                        $"If specified, the {ODataRequestHeaderNames.ODataMaxVersion} header must be a valid OData version supported by this service between version {_odataServiceOptions.MinVersion} and {_odataServiceOptions.MaxVersion}.");
                }
            }

            return ODataVersion.MaxVersion;
        }
    }
}
