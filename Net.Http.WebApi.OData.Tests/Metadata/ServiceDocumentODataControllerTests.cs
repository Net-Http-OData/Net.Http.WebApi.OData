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
            controller.Request.Headers.Add("Accept", "application/json;odata=verbose");
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var response = controller.Get();
            var result = (ODataResponseContent)((ObjectContent<ODataResponseContent>)response.Content).Value;

            Assert.Null(result.Context);
            Assert.Null(result.Count);
            Assert.Null(result.NextLink);

            var serviceDocument = JsonConvert.SerializeObject(result);

            Assert.Equal(
                "{\"value\":[{\"name\":\"Categories\",\"url\":\"Categories\"},{\"name\":\"Customers\",\"url\":\"Customers\"},{\"name\":\"Employees\",\"url\":\"Employees\"},{\"name\":\"Orders\",\"url\":\"Orders\"},{\"name\":\"Products\",\"url\":\"Products\"}]}",
                serviceDocument);
        }

        [Fact]
        public void WhenMinimalMetadataIsRequested_TheEntitySetUrlIsRelative()
        {
            TestHelper.EnsureEDM();

            var controller = new ServiceDocumentODataController();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/");
            controller.Request.Headers.Add("Accept", "application/json;odata=minimalmetadata");
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var response = controller.Get();
            var result = (ODataResponseContent)((ObjectContent<ODataResponseContent>)response.Content).Value;

            Assert.Null(result.Context);
            Assert.Null(result.Count);
            Assert.Null(result.NextLink);

            var serviceDocument = JsonConvert.SerializeObject(result);

            Assert.Equal(
                "{\"value\":[{\"name\":\"Categories\",\"url\":\"Categories\"},{\"name\":\"Customers\",\"url\":\"Customers\"},{\"name\":\"Employees\",\"url\":\"Employees\"},{\"name\":\"Orders\",\"url\":\"Orders\"},{\"name\":\"Products\",\"url\":\"Products\"}]}",
                serviceDocument);
        }

        [Fact]
        public void WhenNoMetadataIsRequested_TheEntitySetUrlIsFullUrl()
        {
            TestHelper.EnsureEDM();

            var controller = new ServiceDocumentODataController();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/");
            controller.Request.Headers.Add("Accept", "application/json;odata=nometadata");
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var response = controller.Get();
            var result = (ODataResponseContent)((ObjectContent<ODataResponseContent>)response.Content).Value;

            Assert.Null(result.Context);
            Assert.Null(result.Count);
            Assert.Null(result.NextLink);

            var serviceDocument = JsonConvert.SerializeObject(result);

            Assert.Equal(
                "{\"value\":[{\"name\":\"Categories\",\"url\":\"http://services.odata.org/OData/Categories\"},{\"name\":\"Customers\",\"url\":\"http://services.odata.org/OData/Customers\"},{\"name\":\"Employees\",\"url\":\"http://services.odata.org/OData/Employees\"},{\"name\":\"Orders\",\"url\":\"http://services.odata.org/OData/Orders\"},{\"name\":\"Products\",\"url\":\"http://services.odata.org/OData/Products\"}]}",
                serviceDocument);
        }
    }
}