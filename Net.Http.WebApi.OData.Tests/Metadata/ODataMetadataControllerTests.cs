using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using Net.Http.WebApi.OData.Metadata;
using Xunit;

namespace Net.Http.WebApi.OData.Tests.Metadata
{
    public class ODataMetadataControllerTests
    {
        [Fact]
        public async Task GetReturnsCsdlXmlDocument()
        {
            TestHelper.EnsureEDM();

            var controller = new ODataMetadataController
            {
                Request = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/$metadata")
            };
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            HttpResponseMessage response = controller.Get();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string result = await ((StringContent)response.Content).ReadAsStringAsync();

            Assert.NotNull(result);
        }
    }
}
