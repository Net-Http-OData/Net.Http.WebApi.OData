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

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new PagedResult<dynamic>(new object[0], 0));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }
        }

        public class ReadODataRequestOptions
        {
            [Fact]
            public void CachesInRequestProperties()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/OData.svc/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

                var requestOptions1 = httpRequestMessage.ReadODataRequestOptions();
                var requestOptions2 = httpRequestMessage.ReadODataRequestOptions();

                Assert.Same(requestOptions1, requestOptions2);

                Assert.Same(requestOptions1, httpRequestMessage.Properties[typeof(ODataRequestOptions).FullName]);
            }
        }

        public class ReadODataRequestOptionsODataIsolationHeaderContainingSnapshot
        {
            [Fact]
            public void TheIsolationLevelIsSet()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/OData.svc/Products");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataIsolation, "Snapshot");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(ODataIsolationLevel.Snapshot, requestOptions.IsolationLevel);
            }
        }

        public class ReadODataRequestOptionsODataIsolationHeaderNotContainingSnapshot
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrownWhenReadingTheIsolationLevel()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/OData.svc/Products");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataIsolation, "ReadCommitted");

                var exception = Assert.Throws<HttpResponseException>(() => httpRequestMessage.ReadODataRequestOptions());

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedIsolationLevel, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }

        public class ReadODataRequestOptionsWithAcceptHeaderContaininAnInvalidODataMetadataValue
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrown()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/OData.svc/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=all");

                var exception = Assert.Throws<HttpResponseException>(() => httpRequestMessage.ReadODataRequestOptions());

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.ODataMetadataValueInvalid, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }

        public class ReadODataRequestOptionsWithAcceptHeaderContainingODataMetadataFull
        {
            [Fact]
            public void TheMetadataLevelIsSetToVerbose()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/OData.svc/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(MetadataLevel.Full, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptionsWithAcceptHeaderContainingODataMetadataMinimal
        {
            [Fact]
            public void TheMetadataLevelIsSetToMinimal()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/OData.svc/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(MetadataLevel.Minimal, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptionsWithAcceptHeaderContainingODataMetadataNone
        {
            [Fact]
            public void TheMetadataLevelIsSetToNone()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/OData.svc/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(MetadataLevel.None, requestOptions.MetadataLevel);
            }
        }
    }
}