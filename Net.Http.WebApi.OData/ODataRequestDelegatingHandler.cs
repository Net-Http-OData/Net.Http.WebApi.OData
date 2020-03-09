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
                    if (!request.IsODataMetadataRequest())
                    {
                        _odataServiceOptions.Validate(request.Headers.Accept.Select(x => x.MediaType));
                    }

                    requestOptions = new ODataRequestOptions(
                        ODataUtility.ODataServiceRootUri(request.RequestUri.Scheme, request.RequestUri.Host, request.RequestUri.LocalPath),
                        ReadIsolationLevel(request),
                        ReadMetadataLevel(request),
                        ReadODataVersion(request),
                        ReadODataMaxVersion(request));

                    _odataServiceOptions.Validate(requestOptions);

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

                response.Headers.Add(ODataResponseHeaderNames.ODataVersion, requestOptions.ODataMaxVersion.ToString());
            }

            return response;
        }

        private static string ReadHeaderValue(HttpRequestMessage request, string name)
            => request.Headers.TryGetValues(name, out IEnumerable<string> values) ? values.FirstOrDefault() : default;

        private ODataIsolationLevel ReadIsolationLevel(HttpRequestMessage request)
        {
            string headerValue = ReadHeaderValue(request, ODataRequestHeaderNames.ODataIsolation);

            if (headerValue != null)
            {
                if (headerValue == "Snapshot")
                {
                    return ODataIsolationLevel.Snapshot;
                }

                throw ODataException.BadRequest($"If specified, the {ODataRequestHeaderNames.ODataIsolation} must be 'Snapshot'.");
            }

            return ODataIsolationLevel.None;
        }

        private ODataMetadataLevel ReadMetadataLevel(HttpRequestMessage request)
        {
            foreach (MediaTypeWithQualityHeaderValue header in request.Headers.Accept)
            {
                foreach (NameValueHeaderValue parameter in header.Parameters)
                {
                    if (parameter.Name == ODataMetadataLevelExtensions.HeaderName)
                    {
                        switch (parameter.Value)
                        {
                            case "none":
                                return ODataMetadataLevel.None;

                            case "minimal":
                                return ODataMetadataLevel.Minimal;

                            case "full":
                                return ODataMetadataLevel.Full;

                            default:
                                throw ODataException.BadRequest(
                                    $"If specified, the {ODataMetadataLevelExtensions.HeaderName} value in the Accept header must be 'none', 'minimal' or 'full'.");
                        }
                    }
                }
            }

            return ODataMetadataLevel.Minimal;
        }

        private ODataVersion ReadODataMaxVersion(HttpRequestMessage request)
        {
            string headerValue = ReadHeaderValue(request, ODataRequestHeaderNames.ODataMaxVersion);

            if (headerValue != null)
            {
                if (ODataVersion.TryParse(headerValue, out ODataVersion odataVersion))
                {
                    return odataVersion;
                }

                throw ODataException.BadRequest(
                    $"If specified, the {ODataRequestHeaderNames.ODataMaxVersion} header must be a valid OData version supported by this service between version {_odataServiceOptions.MinVersion} and {_odataServiceOptions.MaxVersion}.");
            }

            return ODataVersion.MaxVersion;
        }

        private ODataVersion ReadODataVersion(HttpRequestMessage request)
        {
            string headerValue = ReadHeaderValue(request, ODataRequestHeaderNames.ODataVersion);

            if (headerValue != null)
            {
                if (ODataVersion.TryParse(headerValue, out ODataVersion odataVersion))
                {
                    return odataVersion;
                }

                throw ODataException.BadRequest(
                    $"If specified, the {ODataRequestHeaderNames.ODataVersion} header must be a valid OData version supported by this service between version {_odataServiceOptions.MinVersion} and {_odataServiceOptions.MaxVersion}.");
            }

            return ReadODataMaxVersion(request);
        }
    }
}
