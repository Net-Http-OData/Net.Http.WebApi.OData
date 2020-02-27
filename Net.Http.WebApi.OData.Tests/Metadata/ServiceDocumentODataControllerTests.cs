using System.Web.Http;
using System.Web.Http.Results;
using Net.Http.OData;
using Net.Http.WebApi.OData.Metadata;
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
                Request = TestHelper.CreateODataHttpRequestMessage("/OData", ODataMetadataLevel.Full)
            };

            IHttpActionResult result = controller.Get();

            Assert.IsType<OkNegotiatedContentResult<ODataResponseContent>>(result);

            var okResult = (OkNegotiatedContentResult<ODataResponseContent>)result;

            ODataResponseContent odataResponseContent = okResult.Content;

            Assert.Equal("https://services.odata.org/OData/$metadata", odataResponseContent.Context);
            Assert.Null(odataResponseContent.Count);
            Assert.Null(odataResponseContent.NextLink);
            Assert.NotNull(odataResponseContent.Value);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void WhenMinimalMetadataIsRequested_TheEntitySetUrlIsRelative_AndTheContextUriIsSet()
        {
            TestHelper.EnsureEDM();

            var controller = new ServiceDocumentODataController
            {
                Request = TestHelper.CreateODataHttpRequestMessage("/OData", ODataMetadataLevel.Minimal)
            };

            IHttpActionResult result = controller.Get();

            Assert.IsType<OkNegotiatedContentResult<ODataResponseContent>>(result);

            var okResult = (OkNegotiatedContentResult<ODataResponseContent>)result;

            ODataResponseContent odataResponseContent = okResult.Content;

            Assert.Equal("https://services.odata.org/OData/$metadata", odataResponseContent.Context);
            Assert.Null(odataResponseContent.Count);
            Assert.Null(odataResponseContent.NextLink);
            Assert.NotNull(odataResponseContent.Value);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void WhenNoMetadataIsRequested_TheEntitySetUrlIsFullUrl_AndTheContextUriIsNotSet()
        {
            TestHelper.EnsureEDM();

            var controller = new ServiceDocumentODataController
            {
                Request = TestHelper.CreateODataHttpRequestMessage("/OData", ODataMetadataLevel.None)
            };

            IHttpActionResult result = controller.Get();

            Assert.IsType<OkNegotiatedContentResult<ODataResponseContent>>(result);

            var okResult = (OkNegotiatedContentResult<ODataResponseContent>)result;

            ODataResponseContent odataResponseContent = okResult.Content;

            Assert.Null(odataResponseContent.Context);
            Assert.Null(odataResponseContent.Count);
            Assert.Null(odataResponseContent.NextLink);
            Assert.NotNull(odataResponseContent.Value);
        }
    }
}
