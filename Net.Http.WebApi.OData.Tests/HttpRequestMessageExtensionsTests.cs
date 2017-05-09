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
        public class CreateODataResponse_WithAcceptHeaderContainingODataMinimalMetadata
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_WithAcceptHeaderContainingODataMinimalMetadata()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Products?$filter=Price eq 21.39M");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=minimalmetadata");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.DataServiceVersion));
                Assert.Equal("3.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.DataServiceVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == MetadataLevelExtensions.HeaderKey);

                Assert.NotNull(metadataParameter);
                Assert.Equal("minimalmetadata", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_WithAcceptHeaderContainingODataNoMetadata
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_WithAcceptHeaderContainingODataNoMetadata()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Products?$filter=Price eq 21.39M");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=nometadata");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.DataServiceVersion));
                Assert.Equal("3.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.DataServiceVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == MetadataLevelExtensions.HeaderKey);

                Assert.NotNull(metadataParameter);
                Assert.Equal("nometadata", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_WithAcceptHeaderContainingODataVerboseMetadata
        {
            private readonly HttpResponseMessage httpResponseMessage;

            public CreateODataResponse_WithAcceptHeaderContainingODataVerboseMetadata()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Products?$filter=Price eq 21.39M");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=verbose");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                this.httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.DataServiceVersion));
                Assert.Equal("3.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.DataServiceVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == MetadataLevelExtensions.HeaderKey);

                Assert.NotNull(metadataParameter);
                Assert.Equal("verbose", metadataParameter.Value);
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
                Assert.True(this.httpResponseMessage.Headers.Contains(ODataHeaderNames.DataServiceVersion));
                Assert.Equal("3.0", this.httpResponseMessage.Headers.GetValues(ODataHeaderNames.DataServiceVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                var metadataParameter = this.httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == MetadataLevelExtensions.HeaderKey);

                Assert.NotNull(metadataParameter);
                Assert.Equal("minimalmetadata", metadataParameter.Value);
            }
        }

        public class ReadODataRequestOptions
        {
            [Fact]
            public void CachesInRequestProperties()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=minimalmetadata");

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
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=all");

                var exception = Assert.Throws<HttpResponseException>(() => httpRequestMessage.ReadODataRequestOptions());

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.ODataMetadataValueInvalid, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContainingODataMinimalMetadata
        {
            [Fact]
            public void TheMetadataLevelIsSetToMinimal()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=minimalmetadata");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(MetadataLevel.Minimal, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContainingODataNoMetadata
        {
            [Fact]
            public void TheMetadataLevelIsSetToNone()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=nometadata");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(MetadataLevel.None, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContainingODataVerboseMetadata
        {
            [Fact]
            public void TheMetadataLevelIsSetToVerbose()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add("Accept", "application/json;odata=verbose");

                var requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(MetadataLevel.Verbose, requestOptions.MetadataLevel);
            }
        }
    }
}