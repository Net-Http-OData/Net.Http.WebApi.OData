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
                Request = TestHelper.CreateODataHttpRequest("/OData/$metadata")
            };

            IHttpActionResult result = controller.Get();

            Assert.IsType<ContentResult>(result);

            var contentResult = (ContentResult)result;

            string content = contentResult.Content;

            Assert.NotNull(content);

            var resultXml = XDocument.Parse(content);

            Assert.Equal("Edmx", resultXml.Root.Name.LocalName);
        }
    }
}
