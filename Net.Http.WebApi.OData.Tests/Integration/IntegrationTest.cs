﻿using System;
using System.Net.Http;
using System.Web.Http;
using Net.Http.OData;
using Net.Http.OData.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NorthwindModel;

namespace Net.Http.WebApi.OData.Tests.Integration
{
    public abstract class IntegrationTest : IDisposable
    {
        private readonly HttpConfiguration _httpConfiguration;
        private readonly HttpServer _httpServer;

        protected IntegrationTest()
        {
            _httpConfiguration = new HttpConfiguration();

            _httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            _httpConfiguration.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            _httpConfiguration.UseOData(entityDataModelBuilder =>
            {
                entityDataModelBuilder
                    .RegisterEntitySet<Category>("Categories", x => x.Name, Capabilities.Insertable | Capabilities.Updatable | Capabilities.Deletable)
                    .RegisterEntitySet<Customer>("Customers", x => x.CompanyName, Capabilities.Updatable)
                    .RegisterEntitySet<Employee>("Employees", x => x.Id)
                    .RegisterEntitySet<Manager>("Managers", x => x.Id)
                    .RegisterEntitySet<Order>("Orders", x => x.OrderId, Capabilities.Insertable | Capabilities.Updatable)
                    .RegisterEntitySet<Product>("Products", x => x.ProductId, Capabilities.Insertable | Capabilities.Updatable);
            });

            _httpConfiguration.MapHttpAttributeRoutes();

            _httpServer = new HttpServer(_httpConfiguration);

            HttpClient = new HttpClient(_httpServer);
            HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            HttpClient.DefaultRequestHeaders.Add(ODataRequestHeaderNames.ODataVersion, "4.0");
            HttpClient.DefaultRequestHeaders.Add(ODataRequestHeaderNames.ODataMaxVersion, "4.0");
        }

        protected HttpClient HttpClient { get; }

        public void Dispose()
        {
            HttpClient.Dispose();

            _httpServer.Dispose();
            _httpConfiguration.Dispose();
        }
    }
}
