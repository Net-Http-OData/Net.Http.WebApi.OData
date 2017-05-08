namespace Net.Http.WebApi.OData.Tests.Metadata
{
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Hosting;
    using Newtonsoft.Json;
    using OData.Metadata;
    using Xunit;

    public class ServiceDocumentODataControllerTests
    {
        [Fact]
        public void WhenFullMetadataIsRequested_TheEntitySetUrlIsRelative()
        {
            TestHelper.EnsureEDM();

            var controller = new ServiceDocumentODataController();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/");
            controller.Request.Headers.Add("Accept", "application/json;odata.metadata=full");
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var response = controller.Get();
            var result = (ODataResponseContent)((ObjectContent<ODataResponseContent>)response.Content).Value;

            Assert.Null(result.Context);
            Assert.Null(result.Count);
            Assert.Null(result.NextLink);

            var serviceDocument = JsonConvert.SerializeObject(result);

            Assert.Equal(
                "{\"value\":[{\"name\":\"Categories\",\"kind\":\"EntitySet\",\"url\":\"Categories\"},{\"name\":\"Customers\",\"kind\":\"EntitySet\",\"url\":\"Customers\"},{\"name\":\"Employees\",\"kind\":\"EntitySet\",\"url\":\"Employees\"},{\"name\":\"Orders\",\"kind\":\"EntitySet\",\"url\":\"Orders\"},{\"name\":\"Products\",\"kind\":\"EntitySet\",\"url\":\"Products\"}]}",
                serviceDocument);
        }

        [Fact]
        public void WhenMinimalMetadataIsRequested_TheEntitySetUrlIsRelative()
        {
            TestHelper.EnsureEDM();

            var controller = new ServiceDocumentODataController();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/");
            controller.Request.Headers.Add("Accept", "application/json;odata.metadata=minimal");
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var response = controller.Get();
            var result = (ODataResponseContent)((ObjectContent<ODataResponseContent>)response.Content).Value;

            Assert.Null(result.Context);
            Assert.Null(result.Count);
            Assert.Null(result.NextLink);

            var serviceDocument = JsonConvert.SerializeObject(result);

            Assert.Equal(
                "{\"value\":[{\"name\":\"Categories\",\"kind\":\"EntitySet\",\"url\":\"Categories\"},{\"name\":\"Customers\",\"kind\":\"EntitySet\",\"url\":\"Customers\"},{\"name\":\"Employees\",\"kind\":\"EntitySet\",\"url\":\"Employees\"},{\"name\":\"Orders\",\"kind\":\"EntitySet\",\"url\":\"Orders\"},{\"name\":\"Products\",\"kind\":\"EntitySet\",\"url\":\"Products\"}]}",
                serviceDocument);
        }

        [Fact]
        public void WhenNoMetadataIsRequested_TheEntitySetUrlIsFullUrl()
        {
            TestHelper.EnsureEDM();

            var controller = new ServiceDocumentODataController();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/");
            controller.Request.Headers.Add("Accept", "application/json;odata.metadata=none");
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var response = controller.Get();
            var result = (ODataResponseContent)((ObjectContent<ODataResponseContent>)response.Content).Value;

            Assert.Null(result.Context);
            Assert.Null(result.Count);
            Assert.Null(result.NextLink);

            var serviceDocument = JsonConvert.SerializeObject(result);

            Assert.Equal(
                "{\"value\":[{\"name\":\"Categories\",\"kind\":\"EntitySet\",\"url\":\"http://services.odata.org/OData/Categories\"},{\"name\":\"Customers\",\"kind\":\"EntitySet\",\"url\":\"http://services.odata.org/OData/Customers\"},{\"name\":\"Employees\",\"kind\":\"EntitySet\",\"url\":\"http://services.odata.org/OData/Employees\"},{\"name\":\"Orders\",\"kind\":\"EntitySet\",\"url\":\"http://services.odata.org/OData/Orders\"},{\"name\":\"Products\",\"kind\":\"EntitySet\",\"url\":\"http://services.odata.org/OData/Products\"}]}",
                serviceDocument);
        }
    }
}