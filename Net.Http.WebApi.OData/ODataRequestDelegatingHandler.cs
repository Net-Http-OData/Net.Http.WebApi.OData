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
using System.Linq;
using System.Net.Http;
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
        public ODataRequestDelegatingHandler(ODataServiceOptions odataServiceOptions)
            => _odataServiceOptions = odataServiceOptions;

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
                        request.ReadIsolationLevel(),
                        request.ReadMetadataLevel(),
                        request.ReadODataVersion(),
                        request.ReadODataMaxVersion());

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
    }
}
