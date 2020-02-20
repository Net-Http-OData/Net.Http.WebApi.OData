// -----------------------------------------------------------------------
// <copyright file="ODataErrorContentResult.cs" company="Project Contributors">
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
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Net.Http.OData;

namespace Net.Http.WebApi.OData
{
    /// <summary>
    /// Represents an action result that returns a <see cref="HttpStatusCode"/> response and performs content negotiation on an <see cref="ODataErrorContent"/>.
    /// </summary>
    public class ODataErrorContentResult : IHttpActionResult
    {
        private readonly ApiController _apiController;

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataErrorContentResult"/> class.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> to use for the response.</param>
        /// <param name="errorContent">The <see cref="ODataErrorContent"/> to use for the response.</param>
        /// <param name="apiController">The ApiController handling the request.</param>
        public ODataErrorContentResult(HttpStatusCode statusCode, ODataErrorContent errorContent, ApiController apiController)
        {
            StatusCode = statusCode;
            ErrorContent = errorContent ?? throw new ArgumentNullException(nameof(errorContent));
            _apiController = apiController ?? throw new ArgumentNullException(nameof(apiController));
        }

        /// <summary>
        /// Gets the <see cref="HttpStatusCode"/> to use for the response.
        /// </summary>
        public ODataErrorContent ErrorContent { get; }

        /// <summary>
        /// Gets the <see cref="HttpStatusCode"/> to use for the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <inheritdoc />
        public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            => new NegotiatedContentResult<ODataErrorContent>(StatusCode, ErrorContent, _apiController).ExecuteAsync(cancellationToken);
    }
}
