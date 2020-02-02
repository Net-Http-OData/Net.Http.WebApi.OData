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
using System.Net;
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

            if (request?.IsODataRequest() == true)
            {
                if (!request.IsODataMetadataRequest())
                {
                    response.Content.Headers.ContentType.Parameters.Add(requestOptions.MetadataLevel.ToNameValueHeaderValue());
                }

                response.Headers.Add(ODataHeaderNames.ODataVersion, requestOptions.Version.ToString());
            }

            return response;
        }

        private static string ReadHeaderValue(HttpRequestMessage request, string name)
            => request.Headers.TryGetValues(name, out IEnumerable<string> values) ? values.FirstOrDefault() : default;

        private static ODataIsolationLevel ReadIsolationLevel(HttpRequestMessage request)
        {
            string headerValue = ReadHeaderValue(request, ODataHeaderNames.ODataIsolation);

            if (headerValue != null)
            {
                if (headerValue == "Snapshot")
                {
                    return ODataIsolationLevel.Snapshot;
                }

                throw new ODataException(HttpStatusCode.BadRequest, $"If specified, the {ODataHeaderNames.ODataIsolation} must be 'Snapshot'");
            }

            return ODataIsolationLevel.None;
        }

        private static ODataMetadataLevel ReadMetadataLevel(HttpRequestMessage request)
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
                                throw new ODataException(HttpStatusCode.BadRequest, $"If specified, the {ODataMetadataLevelExtensions.HeaderName} value in the Accept header must be 'none', 'minimal' or 'full'");
                        }
                    }
                }
            }

            return ODataMetadataLevel.Minimal;
        }

        private static ODataVersion ReadODataVersion(HttpRequestMessage request)
        {
            string headerValue = ReadHeaderValue(request, ODataHeaderNames.ODataVersion);

            if (headerValue != null)
            {
                if (ODataVersion.TryParse(headerValue, out ODataVersion odataVersion) && odataVersion >= ODataVersion.MinVersion && odataVersion <= ODataVersion.MaxVersion)
                {
                    return odataVersion;
                }
                else
                {
                    throw new ODataException(HttpStatusCode.BadRequest, $"If specified, the {ODataHeaderNames.ODataVersion} header must be a valid OData version supported by this service between version {ODataVersion.MinVersion} and {ODataVersion.MaxVersion}");
                }
            }

            headerValue = ReadHeaderValue(request, ODataHeaderNames.ODataMaxVersion);

            if (headerValue != null)
            {
                if (ODataVersion.TryParse(headerValue, out ODataVersion odataVersion) && odataVersion >= ODataVersion.MinVersion && odataVersion <= ODataVersion.MaxVersion)
                {
                    return odataVersion;
                }
                else
                {
                    throw new ODataException(HttpStatusCode.BadRequest, $"If specified, the {ODataHeaderNames.ODataMaxVersion} header must be a valid OData version supported by this service between version {ODataVersion.MinVersion} and {ODataVersion.MaxVersion}");
                }
            }

            return ODataVersion.MaxVersion;
        }
    }
}
