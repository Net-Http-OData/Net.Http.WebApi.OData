namespace Net.Http.WebApi.OData.Tests.Metadata
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Hosting;
    using OData.Metadata;
    using Xunit;

    public class ODataMetadataControllerTests
    {
        [Fact]
        public void GetReturnsCsdlXmlDocument()
        {
            TestHelper.EnsureEDM();

            var controller = new ODataMetadataController();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/$metadata");
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            var response = controller.Get();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.Headers.Contains(ODataHeaderNames.ODataVersion));
            Assert.Equal(ODataHeaderValues.ODataVersionString, response.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());

            var result = ((StringContent)response.Content).ReadAsStringAsync().Result;

            Assert.NotNull(result);
        }
    }
}