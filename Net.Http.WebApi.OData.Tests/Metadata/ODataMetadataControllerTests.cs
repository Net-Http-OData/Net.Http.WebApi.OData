using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
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
                Request = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/$metadata")
            };
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

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
