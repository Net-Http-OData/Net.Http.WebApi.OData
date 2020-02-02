using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Net.Http.OData;
using Net.Http.WebApi.OData.Metadata;
using Newtonsoft.Json;
using Xunit;

namespace Net.Http.WebApi.OData.Tests.Metadata
{
    public class ServiceDocumentODataControllerTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void WhenFullMetadataIsRequested_TheEntitySetUrlIsRelative_AndTheContextUriIsSet()
        {
            TestHelper.EnsureEDM();

            var controller = new ServiceDocumentODataController
            {
                Request = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/")
            };
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            controller.Request.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Full, ODataVersion.OData40));

            HttpResponseMessage response = controller.Get();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = (ODataResponseContent)((ObjectContent<ODataResponseContent>)response.Content).Value;

            Assert.NotNull(result.Context);
            Assert.Equal("http://services.odata.org/OData/$metadata", result.Context.ToString());

            Assert.Null(result.Count);
            Assert.Null(result.NextLink);

            string serviceDocument = JsonConvert.SerializeObject(result);

            Assert.Equal(
                "{\"@odata.context\":\"http://services.odata.org/OData/$metadata\",\"value\":[{\"name\":\"Categories\",\"kind\":\"EntitySet\",\"url\":\"Categories\"},{\"name\":\"Customers\",\"kind\":\"EntitySet\",\"url\":\"Customers\"},{\"name\":\"Employees\",\"kind\":\"EntitySet\",\"url\":\"Employees\"},{\"name\":\"Managers\",\"kind\":\"EntitySet\",\"url\":\"Managers\"},{\"name\":\"Orders\",\"kind\":\"EntitySet\",\"url\":\"Orders\"},{\"name\":\"Products\",\"kind\":\"EntitySet\",\"url\":\"Products\"}]}",
                serviceDocument);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void WhenMinimalMetadataIsRequested_TheEntitySetUrlIsRelative_AndTheContextUriIsSet()
        {
            TestHelper.EnsureEDM();

            var controller = new ServiceDocumentODataController
            {
                Request = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/")
            };
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            controller.Request.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Minimal, ODataVersion.OData40));

            HttpResponseMessage response = controller.Get();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = (ODataResponseContent)((ObjectContent<ODataResponseContent>)response.Content).Value;

            Assert.NotNull(result.Context);
            Assert.Equal("http://services.odata.org/OData/$metadata", result.Context.ToString());

            Assert.Null(result.Count);
            Assert.Null(result.NextLink);

            string serviceDocument = JsonConvert.SerializeObject(result);

            Assert.Equal(
                "{\"@odata.context\":\"http://services.odata.org/OData/$metadata\",\"value\":[{\"name\":\"Categories\",\"kind\":\"EntitySet\",\"url\":\"Categories\"},{\"name\":\"Customers\",\"kind\":\"EntitySet\",\"url\":\"Customers\"},{\"name\":\"Employees\",\"kind\":\"EntitySet\",\"url\":\"Employees\"},{\"name\":\"Managers\",\"kind\":\"EntitySet\",\"url\":\"Managers\"},{\"name\":\"Orders\",\"kind\":\"EntitySet\",\"url\":\"Orders\"},{\"name\":\"Products\",\"kind\":\"EntitySet\",\"url\":\"Products\"}]}",
                serviceDocument);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void WhenNoMetadataIsRequested_TheEntitySetUrlIsFullUrl_AndTheContextUriIsNotSet()
        {
            TestHelper.EnsureEDM();

            var controller = new ServiceDocumentODataController
            {
                Request = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/")
            };
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            controller.Request.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.None, ODataVersion.OData40));

            HttpResponseMessage response = controller.Get();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = (ODataResponseContent)((ObjectContent<ODataResponseContent>)response.Content).Value;

            Assert.Null(result.Context);
            Assert.Null(result.Count);
            Assert.Null(result.NextLink);

            string serviceDocument = JsonConvert.SerializeObject(result);

            Assert.Equal(
                "{\"value\":[{\"name\":\"Categories\",\"kind\":\"EntitySet\",\"url\":\"http://services.odata.org/OData/Categories\"},{\"name\":\"Customers\",\"kind\":\"EntitySet\",\"url\":\"http://services.odata.org/OData/Customers\"},{\"name\":\"Employees\",\"kind\":\"EntitySet\",\"url\":\"http://services.odata.org/OData/Employees\"},{\"name\":\"Managers\",\"kind\":\"EntitySet\",\"url\":\"http://services.odata.org/OData/Managers\"},{\"name\":\"Orders\",\"kind\":\"EntitySet\",\"url\":\"http://services.odata.org/OData/Orders\"},{\"name\":\"Products\",\"kind\":\"EntitySet\",\"url\":\"http://services.odata.org/OData/Products\"}]}",
                serviceDocument);
        }
    }
}
