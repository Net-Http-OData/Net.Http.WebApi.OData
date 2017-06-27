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
        public class CreateODataResponse_String_WithNonNullValue
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_String_WithNonNullValue()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Products('Milk')/Code/$value");

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse("MLK");
            }

            [Fact]
            public void TheContentIsSet()
            {
                Assert.Equal("MLK", ((StringContent)this.httpResponseMessage.Content).ReadAsStringAsync().Result);
            }

            [Fact]
            public void TheContentIsStringContent()
            {
                Assert.IsType<StringContent>(this.httpResponseMessage.Content);
            }

            [Fact]
            public void TheContentTypeIsTextPlain()
            {
                Assert.Equal("text/plain", this.httpResponseMessage.Content.Headers.ContentType.MediaType);
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsNotSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.Null(metadataParameter);
            }

            [Fact]
            public void TheStatusCodeIsOk()
            {
                Assert.Equal(HttpStatusCode.OK, this.httpResponseMessage.StatusCode);
            }
        }

        public class CreateODataResponse_String_WithNullValue
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_String_WithNullValue()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Products('Milk')/Code/$value");

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(default(string));
            }

            [Fact]
            public void TheContentIsNull()
            {
                Assert.Null(this.httpResponseMessage.Content);
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal("4.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheStatusCodeIsNoContent()
            {
                Assert.Equal(HttpStatusCode.NoContent, this.httpResponseMessage.StatusCode);
            }
        }

        public class CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataFull
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataFull()
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
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("full", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataMinimal
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataMinimal()
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
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("minimal", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataNone
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataNone()
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
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("none", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_T_WithoutMetadataLevelSpecifiedInRequest
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_T_WithoutMetadataLevelSpecifiedInRequest()
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
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

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

                Assert.Equal(ODataMetadataLevel.Full, requestOptions.MetadataLevel);
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

                Assert.Equal(ODataMetadataLevel.Minimal, requestOptions.MetadataLevel);
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

                Assert.Equal(ODataMetadataLevel.None, requestOptions.MetadataLevel);
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