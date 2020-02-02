using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Net.Http.OData;
using Xunit;

namespace Net.Http.WebApi.OData.Tests.Integration
{
    public class GetServiceDocumentTests : IntegrationTest
    {
        private HttpResponseMessage _httpResponseMessage;

        public GetServiceDocumentTests()
        {
            _httpResponseMessage = HttpClient.GetAsync("http://server/odata").Result;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ContentType_ApplicationJson()
        {
            Assert.Equal("application/json", _httpResponseMessage.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ContentType_Parameter_ODataMetadata()
        {
            Assert.Equal("minimal", _httpResponseMessage.Content.Headers.ContentType.Parameters.Single(x => x.Name == ODataMetadataLevelExtensions.HeaderName).Value);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ODataVersion()
        {
            Assert.Equal(ODataVersion.OData40.ToString(), _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Contains_ODataResponseContent()
        {
            Assert.NotNull(_httpResponseMessage.Content);

            string result = await _httpResponseMessage.Content.ReadAsStringAsync();

            Assert.Equal(
                "{\"@odata.context\":\"http://server/odata/$metadata\",\"value\":[{\"name\":\"Categories\",\"kind\":\"EntitySet\",\"url\":\"Categories\"},{\"name\":\"Customers\",\"kind\":\"EntitySet\",\"url\":\"Customers\"},{\"name\":\"Employees\",\"kind\":\"EntitySet\",\"url\":\"Employees\"},{\"name\":\"Managers\",\"kind\":\"EntitySet\",\"url\":\"Managers\"},{\"name\":\"Orders\",\"kind\":\"EntitySet\",\"url\":\"Orders\"},{\"name\":\"Products\",\"kind\":\"EntitySet\",\"url\":\"Products\"}]}",
                result);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void StatusCode_OK()
        {
            Assert.Equal(HttpStatusCode.OK, _httpResponseMessage.StatusCode);
        }
    }
}
