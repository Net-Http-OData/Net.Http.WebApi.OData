// -----------------------------------------------------------------------
// <copyright file="ContentResult.cs" company="Project Contributors">
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Net.Http.WebApi.OData
{
    /// <summary>
    /// Represents an action result that returns a <see cref="HttpStatusCode.OK"/> response and string content.
    /// </summary>
    public class ContentResult : IHttpActionResult
    {
        private readonly ApiController _apiController;

        /// <summary>
        /// Initialises a new instance of the <see cref="ContentResult"/> class.
        /// </summary>
        /// <param name="content">The string content to return in the response.</param>
        /// <param name="encoding">The encoding for the content.</param>
        /// <param name="mediaType">The media type for the content.</param>
        /// <param name="apiController">The ApiController handling the request.</param>
        public ContentResult(string content, Encoding encoding, string mediaType, ApiController apiController)
        {
            if (string.IsNullOrWhiteSpace(mediaType))
            {
                throw new ArgumentException("message", nameof(mediaType));
            }

            Content = content;
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            MediaType = mediaType;
            _apiController = apiController ?? throw new ArgumentNullException(nameof(apiController));
        }

        /// <summary>
        /// Gets the string content to return in the response.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Gets the encoding to use for the content.
        /// </summary>
        public Encoding Encoding { get; }

        /// <summary>
        /// Gets the media type for the content.
        /// </summary>
        public string MediaType { get; }

        /// <inheritdoc/>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = _apiController.Request.CreateResponse(Content is null ? HttpStatusCode.NoContent : HttpStatusCode.OK);

            if (Content != null)
            {
                response.Content = new StringContent(Content, Encoding, MediaType);
            }

            return Task.FromResult(response);
        }
    }
}
