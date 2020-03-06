// -----------------------------------------------------------------------
// <copyright file="ODataMetadataController.cs" company="Project Contributors">
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
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml.Linq;
using Net.Http.OData;
using Net.Http.OData.Metadata;
using Net.Http.OData.Model;

namespace Net.Http.WebApi.OData.Metadata
{
    /// <summary>
    /// An API controller which exposes the OData service metadata.
    /// </summary>
    [RoutePrefix("odata")]
    public sealed class ODataMetadataController : ODataController
    {
        private static string s_metadataXml;

        /// <summary>
        /// Gets the <see cref="HttpResponseMessage"/> which contains the service metadata.
        /// </summary>
        /// <returns>The <see cref="HttpResponseMessage"/> which contains the service metadata.</returns>
        [HttpGet]
        [Route("$metadata")]
        public IHttpActionResult Get()
        {
            EnsureMetadata();

            return Content(s_metadataXml, "application/xml", Encoding.UTF8);
        }

        private static void EnsureMetadata()
        {
            if (s_metadataXml is null)
            {
                using (var stringWriter = new Utf8StringWriter())
                {
                    XDocument metadataDocument = XmlMetadataProvider.Create(EntityDataModel.Current, ODataServiceOptions.Current);
                    metadataDocument.Save(stringWriter);

                    s_metadataXml = stringWriter.ToString();
                }
            }
        }
    }
}
