// -----------------------------------------------------------------------
// <copyright file="ServiceDocumentODataController.cs" company="Project Contributors">
// Copyright 2012 - 2017 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData.Metadata
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Model;

    /// <summary>
    /// An API controller which exposes the OData service document.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("odata")]
    public sealed class ServiceDocumentODataController : ApiController
    {
        /// <summary>
        /// Gets the <see cref="HttpResponseMessage"/> which contains the service document.
        /// </summary>
        /// <returns>The <see cref="HttpResponseMessage"/> which contains the service document.</returns>
        [HttpGet, Route("")]
        public HttpResponseMessage Get()
        {
            Uri serviceUri = null;

            if (this.Request.ReadODataRequestOptions().MetadataLevel == MetadataLevel.None)
            {
                serviceUri = this.Request.RequestUri.GetODataServiceUri();
            }

            var serviceDocumentResponse = new ODataResponseContent(
                     null,
                     EntityDataModel.Current.EntitySets.Select(
                         kvp =>
                         {
                             var setUri = new Uri(kvp.Key, UriKind.Relative);

                             return ServiceDocumentItem.EntitySet(
                                  kvp.Key,
                                  serviceUri != null ? new Uri(serviceUri, setUri) : setUri);
                         }));

            return this.Request.CreateODataResponse(HttpStatusCode.OK, serviceDocumentResponse);
        }
    }
}