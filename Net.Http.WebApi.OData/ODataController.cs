// -----------------------------------------------------------------------
// <copyright file="ODataController.cs" company="Project Contributors">
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
using System.Net;
using System.Text;
using System.Web.Http;
using Net.Http.OData;

namespace Net.Http.WebApi.OData
{
    /// <summary>
    /// A base controller for OData controllers.
    /// </summary>
    public abstract class ODataController : ApiController
    {
        /// <summary>
        /// Creates a <see cref="ContentResult"/> with the specified content.
        /// </summary>
        /// <param name="content">The string content to return in the response.</param>
        /// <param name="mediaType">The media type for the content.</param>
        /// <param name="encoding">The encoding for the content.</param>
        /// <returns>A <see cref="ContentResult"/>.</returns>
        protected virtual ContentResult Content(string content, string mediaType, Encoding encoding)
            => new ContentResult(content, encoding, mediaType, this);

        /// <summary>
        /// Creates an <see cref="ODataErrorContentResult"/> with the specified content.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> to use for the response.</param>
        /// <param name="errorContent">The <see cref="ODataErrorContent"/> to use for the response.</param>
        /// <returns>An <see cref="ODataErrorContentResult"/>.</returns>
        protected virtual ODataErrorContentResult ODataError(HttpStatusCode statusCode, ODataErrorContent errorContent)
            => new ODataErrorContentResult(statusCode, errorContent, this);
    }
}
