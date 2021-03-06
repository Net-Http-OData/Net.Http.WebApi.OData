﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Net.Http.OData;
using Xunit;

namespace Net.Http.WebApi.OData.Tests.Integration
{
    public class GetServiceDocumentTests_MetadataMinimal : IntegrationTest
    {
        private readonly HttpResponseMessage _response;

        public GetServiceDocumentTests_MetadataMinimal()
            => _response = HttpClient.GetAsync("http://server/odata").Result;

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ContentType_ApplicationJson()
            => Assert.Equal("application/json", _response.Content.Headers.ContentType.MediaType);

        [Fact]
        [Trait("Category", "Integration")]
        public void Contains_Header_ContentType_Parameter_ODataMetadata()
            => Assert.Equal("minimal", _response.Content.Headers.ContentType.Parameters.Single(x => x.Name == ODataMetadataLevelExtensions.HeaderName).Value);

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
                "{\"@odata.context\":\"http://server/odata/$metadata\",\"value\":[{\"kind\":\"EntitySet\",\"name\":\"Categories\",\"url\":\"Categories\"},{\"kind\":\"EntitySet\",\"name\":\"Customers\",\"url\":\"Customers\"},{\"kind\":\"EntitySet\",\"name\":\"Employees\",\"url\":\"Employees\"},{\"kind\":\"EntitySet\",\"name\":\"Managers\",\"url\":\"Managers\"},{\"kind\":\"EntitySet\",\"name\":\"Orders\",\"url\":\"Orders\"},{\"kind\":\"EntitySet\",\"name\":\"Products\",\"url\":\"Products\"}]}",
                result);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void StatusCode_OK()
            => Assert.Equal(HttpStatusCode.OK, _response.StatusCode);
    }
}
