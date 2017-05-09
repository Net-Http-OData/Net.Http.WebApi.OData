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
        public class CreateODataResponse_WithAcceptHeaderContainingODataMetadataFull
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_WithAcceptHeaderContainingODataMetadataFull()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Products?$filter=Price eq 21.39M");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == MetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("full", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_WithAcceptHeaderContainingODataMetadataMinimal
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_WithAcceptHeaderContainingODataMetadataMinimal()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Products?$filter=Price eq 21.39M");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == MetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("minimal", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_WithAcceptHeaderContainingODataMetadataNone
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_WithAcceptHeaderContainingODataMetadataNone()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Products?$filter=Price eq 21.39M");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == MetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("none", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_WithoutMetadataLevelSpecifiedInRequest
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_WithoutMetadataLevelSpecifiedInRequest()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Products?$filter=Price eq 21.39M");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == MetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("minimal", metadataParameter.Value);
            }
        }

        public class ReadODataRequestOptions
        {
            [Fact]
            public void CachesInRequestProperties()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

                var requestOptions1 = httpRequestMessage.ReadODataRequestOptions();
                var requestOptions2 = httpRequestMessage.ReadODataRequestOptions();

                Assert.Same(requestOptions1, requestOptions2);

                Assert.Same(requestOptions1, httpRequestMessage.Properties[typeof(ODataRequestOptions).FullName]);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContaininAnInvalidODataMetadataValue
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrown()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=all");

                var exception = Assert.Throws<HttpResponseException>(() => httpRequestMessage.ReadODataRequestOptions());

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.ODataMetadataValueInvalid, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContainingODataMetadataFull
        {
            [Fact]
            public void TheMetadataLevelIsSetToVerbose()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(MetadataLevel.Full, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContainingODataMetadataMinimal
        {
            [Fact]
            public void TheMetadataLevelIsSetToMinimal()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(MetadataLevel.Minimal, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContainingODataMetadataNone
        {
            [Fact]
            public void TheMetadataLevelIsSetToNone()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(MetadataLevel.None, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptions_WithODataIsolationHeaderContainingSnapshot
        {
            [Fact]
            public void TheIsolationLevelIsSet()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataIsolation, "Snapshot");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(ODataIsolationLevel.Snapshot, requestOptions.IsolationLevel);
            }
        }

        public class ReadODataRequestOptions_WithODataIsolationHeaderNotContainingSnapshot
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrownWhenReadingTheIsolationLevel()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataIsolation, "ReadCommitted");

                var exception = Assert.Throws<HttpResponseException>(() => httpRequestMessage.ReadODataRequestOptions());

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedIsolationLevel, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }
    }
}