﻿// -----------------------------------------------------------------------
// <copyright file="ServiceDocumentODataController.cs" company="Project Contributors">
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
using System.Net.Http;
using System.Web.Http;
using Net.Http.OData;
using Net.Http.OData.Metadata;
using Net.Http.OData.Model;

namespace Net.Http.WebApi.OData.Metadata
{
    /// <summary>
    /// An API controller which exposes the OData service document.
    /// </summary>
    [RoutePrefix("odata")]
    public sealed class ServiceDocumentODataController : ApiController
    {
        /// <summary>
        /// Gets the <see cref="IHttpActionResult"/> which contains the service document.
        /// </summary>
        /// <returns>The <see cref="IHttpActionResult"/> which contains the service document.</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            ODataRequestOptions requestOptions = Request.ReadODataRequestOptions();
            string contextUri = Request.ResolveODataContext();
            IEnumerable<ServiceDocumentItem> serviceDocumentItems = ServiceDocumentProvider.Create(EntityDataModel.Current, requestOptions);

            var serviceDocumentResponse = new ODataResponseContent(serviceDocumentItems, contextUri);

            return Ok(serviceDocumentResponse);
        }
    }
}
