using System.Web.Http;
using System.Web.Http.Results;
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
                Request = TestHelper.CreateHttpRequestMessage("/OData", ODataMetadataLevel.Full)
            };

            IHttpActionResult result = controller.Get();

            Assert.IsType<OkNegotiatedContentResult<ODataResponseContent>>(result);

            var okResult = (OkNegotiatedContentResult<ODataResponseContent>)result;

            ODataResponseContent odataResponseContent = okResult.Content;

            Assert.Equal("https://services.odata.org/OData/$metadata", odataResponseContent.Context);
            Assert.Null(odataResponseContent.Count);
            Assert.Null(odataResponseContent.NextLink);
            Assert.NotNull(odataResponseContent.Value);

            string serviceDocument = JsonConvert.SerializeObject(odataResponseContent);

            Assert.Equal(
                "{\"@odata.context\":\"https://services.odata.org/OData/$metadata\",\"value\":[{\"name\":\"Categories\",\"kind\":\"EntitySet\",\"url\":\"Categories\"},{\"name\":\"Customers\",\"kind\":\"EntitySet\",\"url\":\"Customers\"},{\"name\":\"Employees\",\"kind\":\"EntitySet\",\"url\":\"Employees\"},{\"name\":\"Managers\",\"kind\":\"EntitySet\",\"url\":\"Managers\"},{\"name\":\"Orders\",\"kind\":\"EntitySet\",\"url\":\"Orders\"},{\"name\":\"Products\",\"kind\":\"EntitySet\",\"url\":\"Products\"}]}",
                serviceDocument);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void WhenMinimalMetadataIsRequested_TheEntitySetUrlIsRelative_AndTheContextUriIsSet()
        {
            TestHelper.EnsureEDM();

            var controller = new ServiceDocumentODataController
            {
                Request = TestHelper.CreateHttpRequestMessage("/OData", ODataMetadataLevel.Minimal)
            };

            IHttpActionResult result = controller.Get();

            Assert.IsType<OkNegotiatedContentResult<ODataResponseContent>>(result);

            var okResult = (OkNegotiatedContentResult<ODataResponseContent>)result;

            ODataResponseContent odataResponseContent = okResult.Content;

            Assert.Equal("https://services.odata.org/OData/$metadata", odataResponseContent.Context);
            Assert.Null(odataResponseContent.Count);
            Assert.Null(odataResponseContent.NextLink);
            Assert.NotNull(odataResponseContent.Value);

            string serviceDocument = JsonConvert.SerializeObject(odataResponseContent);

            Assert.Equal(
                "{\"@odata.context\":\"https://services.odata.org/OData/$metadata\",\"value\":[{\"name\":\"Categories\",\"kind\":\"EntitySet\",\"url\":\"Categories\"},{\"name\":\"Customers\",\"kind\":\"EntitySet\",\"url\":\"Customers\"},{\"name\":\"Employees\",\"kind\":\"EntitySet\",\"url\":\"Employees\"},{\"name\":\"Managers\",\"kind\":\"EntitySet\",\"url\":\"Managers\"},{\"name\":\"Orders\",\"kind\":\"EntitySet\",\"url\":\"Orders\"},{\"name\":\"Products\",\"kind\":\"EntitySet\",\"url\":\"Products\"}]}",
                serviceDocument);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void WhenNoMetadataIsRequested_TheEntitySetUrlIsFullUrl_AndTheContextUriIsNotSet()
        {
            TestHelper.EnsureEDM();

            var controller = new ServiceDocumentODataController
            {
                Request = TestHelper.CreateHttpRequestMessage("/OData", ODataMetadataLevel.None)
            };

            IHttpActionResult result = controller.Get();

            Assert.IsType<OkNegotiatedContentResult<ODataResponseContent>>(result);

            var okResult = (OkNegotiatedContentResult<ODataResponseContent>)result;

            ODataResponseContent odataResponseContent = okResult.Content;

            Assert.Null(odataResponseContent.Context);
            Assert.Null(odataResponseContent.Count);
            Assert.Null(odataResponseContent.NextLink);
            Assert.NotNull(odataResponseContent.Value);

            string serviceDocument = JsonConvert.SerializeObject(odataResponseContent);

            Assert.Equal(
                "{\"value\":[{\"name\":\"Categories\",\"kind\":\"EntitySet\",\"url\":\"https://services.odata.org/OData/Categories\"},{\"name\":\"Customers\",\"kind\":\"EntitySet\",\"url\":\"https://services.odata.org/OData/Customers\"},{\"name\":\"Employees\",\"kind\":\"EntitySet\",\"url\":\"https://services.odata.org/OData/Employees\"},{\"name\":\"Managers\",\"kind\":\"EntitySet\",\"url\":\"https://services.odata.org/OData/Managers\"},{\"name\":\"Orders\",\"kind\":\"EntitySet\",\"url\":\"https://services.odata.org/OData/Orders\"},{\"name\":\"Products\",\"kind\":\"EntitySet\",\"url\":\"https://services.odata.org/OData/Products\"}]}",
                serviceDocument);
        }
    }
}
