// -----------------------------------------------------------------------
// <copyright file="ODataMetadataController.cs" company="Project Contributors">
// Copyright 2012 - 2019 Project Contributors
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
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;
    using Net.Http.WebApi.OData.Model;

    /// <summary>
    /// An API controller which exposes the OData service metadata.
    /// </summary>
    [RoutePrefix("odata")]
    public sealed class ODataMetadataController : ApiController
    {
        private static string metadataXml;

        /// <summary>
        /// Gets the <see cref="HttpResponseMessage"/> which contains the service metadata.
        /// </summary>
        /// <returns>The <see cref="HttpResponseMessage"/> which contains the service metadata.</returns>
        [HttpGet]
        [Route("$metadata")]
#pragma warning disable CA1822 // Mark members as static
        public HttpResponseMessage Get()
#pragma warning restore CA1822 // Mark members as static
        {
            if (metadataXml is null)
            {
                using (var stringWriter = new Utf8StringWriter())
                {
                    var metadataDocument = MetadataProvider.Create(EntityDataModel.Current);
                    metadataDocument.Save(stringWriter);

                    metadataXml = stringWriter.ToString();
                }
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(metadataXml, Encoding.UTF8, "application/xml"),
            };
            response.Headers.Add(ODataHeaderNames.ODataVersion, ODataHeaderValues.ODataVersionString);

            return response;
        }
    }
}