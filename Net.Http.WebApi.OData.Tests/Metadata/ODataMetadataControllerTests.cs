using System.Web.Http;
using System.Xml.Linq;
using Net.Http.WebApi.OData.Metadata;
using Xunit;

namespace Net.Http.WebApi.OData.Tests.Metadata
{
    public class ODataMetadataControllerTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void GetReturnsCsdlXmlDocument()
        {
            TestHelper.EnsureEDM();

            var controller = new ODataMetadataController
            {
                Request = TestHelper.CreateHttpRequestMessage("/OData/$metadata")
            };

            IHttpActionResult response = controller.Get();

            Assert.IsType<ContentResult>(response);

            var contentResult = (ContentResult)response;

            string result = contentResult.Content;

            Assert.NotNull(result);

            var resultXml = XDocument.Parse(result);

            Assert.Equal("Edmx", resultXml.Root.Name.LocalName);
        }
    }
}
