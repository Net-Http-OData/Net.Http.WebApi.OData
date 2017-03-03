namespace Net.Http.WebApi.OData.Tests
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Hosting;
    using Xunit;

    public class HttpRequestMessageExtensionsTests
    {
        public class CreateODataResponse
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://localhost/Business?$filter=LegacyId eq 2139");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new PagedResult<dynamic>(new object[0], 0));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }
        }
    }
}