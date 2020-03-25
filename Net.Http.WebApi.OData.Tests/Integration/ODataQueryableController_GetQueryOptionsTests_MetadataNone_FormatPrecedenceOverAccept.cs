using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Net.Http.OData;
using Xunit;

namespace Net.Http.WebApi.OData.Tests.Integration
{
    public class ODataQueryableController_GetQueryOptionsTests_MetadataNone_FormatPrecedenceOverAccept : IntegrationTest
    {
        private readonly HttpResponseMessage _response;

        public ODataQueryableController_GetQueryOptionsTests_MetadataNone_FormatPrecedenceOverAccept()
        {
            // The $format query option, if present in a request, MUST take precedence over the value(s) specified in the Accept request header.
            var request = new HttpRequestMessage(HttpMethod.Get, "http://server/odata/Employees?$format=application/json;odata.metadata=none");
            request.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            _response = HttpClient.SendAsync(request).Result;
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ContentType_ApplicationJson()
            => Assert.Equal("application/json", _response.Content.Headers.ContentType.MediaType);

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ContentType_Parameter_ODataMetadata()
            => Assert.Equal("none", _response.Content.Headers.ContentType.Parameters.Single(x => x.Name == ODataMetadataLevelExtensions.HeaderName).Value);

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ODataVersion()
            => Assert.Equal(ODataVersion.OData40.ToString(), _response.Headers.GetValues(ODataResponseHeaderNames.ODataVersion).Single());

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Contains_ODataResponseContent()
        {
            Assert.NotNull(_response.Content);

            string result = await _response.Content.ReadAsStringAsync();

            Assert.Equal(
                "{\"value\":[]}",
                result);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void StatusCode_OK()
            => Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
    }
}
