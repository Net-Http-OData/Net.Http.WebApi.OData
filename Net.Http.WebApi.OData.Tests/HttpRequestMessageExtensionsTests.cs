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
                    "http://services.odata.org/OData/OData.svc/Products?$filter=Price eq 21.39M");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.DataServiceVersion));
                Assert.Equal("3.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.DataServiceVersion).Single());
            }
        }

        public class ReadODataRequestOptions
        {
            [Fact]
            public void CachesInRequestProperties()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/OData.svc/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=minimalmetadata");

                var requestOptions1 = httpRequestMessage.ReadODataRequestOptions();
                var requestOptions2 = httpRequestMessage.ReadODataRequestOptions();

                Assert.Same(requestOptions1, requestOptions2);

                Assert.Same(requestOptions1, httpRequestMessage.Properties[typeof(ODataRequestOptions).FullName]);
            }
        }

        public class ReadODataRequestOptionsWithAcceptHeaderContaininAnInvalidODataMetadataValue
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrown()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/OData.svc/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=all");

                var exception = Assert.Throws<HttpResponseException>(() => httpRequestMessage.ReadODataRequestOptions());

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.ODataMetadataValueInvalid, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }

        public class ReadODataRequestOptionsWithAcceptHeaderContainingODataMinimalMetadata
        {
            [Fact]
            public void TheMetadataLevelIsSetToMinimal()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/OData.svc/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=minimalmetadata");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(MetadataLevel.Minimal, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptionsWithAcceptHeaderContainingODataNoMetadata
        {
            [Fact]
            public void TheMetadataLevelIsSetToNone()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/OData.svc/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=nometadata");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(MetadataLevel.None, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptionsWithAcceptHeaderContainingODataVerboseMetadata
        {
            [Fact]
            public void TheMetadataLevelIsSetToVerbose()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/OData.svc/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=verbose");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(MetadataLevel.Verbose, requestOptions.MetadataLevel);
            }
        }
    }
}