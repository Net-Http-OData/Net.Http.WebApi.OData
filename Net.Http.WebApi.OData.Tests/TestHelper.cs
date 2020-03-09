using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Net.Http.OData;
using Net.Http.OData.Model;
using NorthwindModel;

namespace Net.Http.WebApi.OData.Tests
{
    internal static class TestHelper
    {
        internal static ODataServiceOptions ODataServiceOptions
            => new ODataServiceOptions(
                ODataVersion.MinVersion,
                ODataVersion.MaxVersion,
                new[] { ODataIsolationLevel.None },
                new[] { "application/json", "text/plain" });

        /// <summary>
        /// Creates an <see cref="HttpRequestMessage"/> (without ODataRequestOptions) for the URI 'https://services.odata.org/{path}'.
        /// </summary>
        /// <param name="path">The path for the request URI.</param>
        /// <returns>The HttpRequestMessage without ODataRequestOptions.</returns>
        internal static HttpRequestMessage CreateHttpRequest(string path)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://services.odata.org" + path));
            request.Headers.Add("Accept", "application/json");
            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            return request;
        }

        /// <summary>
        /// Creates an <see cref="HttpRequestMessage"/> (with ODataRequestOptions) for the URI 'https://services.odata.org/{path}'.
        /// </summary>
        /// <param name="path">The path for the request URI.</param>
        /// <param name="metadataLevel">The metadata level to use (defaults to minimal if not specified).</param>
        /// <returns>The HttpRequestMessage with ODataRequestOptions.</returns>
        internal static HttpRequestMessage CreateODataHttpRequest(string path, ODataMetadataLevel metadataLevel = ODataMetadataLevel.Minimal)
        {
            HttpRequestMessage request = CreateHttpRequest(path);
            request.Properties.Add(
                typeof(ODataRequestOptions).FullName,
                new ODataRequestOptions(new Uri("https://services.odata.org/OData/"), ODataIsolationLevel.None, metadataLevel, ODataVersion.OData40, ODataVersion.OData40));

            return request;
        }

        internal static void EnsureEDM()
        {
            ODataServiceOptions.Current = ODataServiceOptions;

            EntityDataModelBuilder entityDataModelBuilder = new EntityDataModelBuilder(StringComparer.OrdinalIgnoreCase)
                .RegisterEntitySet<Category>("Categories", x => x.Name, Capabilities.Insertable | Capabilities.Updatable | Capabilities.Deletable)
                .RegisterEntitySet<Customer>("Customers", x => x.CompanyName, Capabilities.Updatable)
                .RegisterEntitySet<Employee>("Employees", x => x.Id)
                .RegisterEntitySet<Manager>("Managers", x => x.Id)
                .RegisterEntitySet<Order>("Orders", x => x.OrderId, Capabilities.Insertable | Capabilities.Updatable)
                .RegisterEntitySet<Product>("Products", x => x.ProductId, Capabilities.Insertable | Capabilities.Updatable);

            entityDataModelBuilder.BuildModel();
        }
    }
}
