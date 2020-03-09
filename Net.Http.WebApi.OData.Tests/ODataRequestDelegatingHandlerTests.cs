using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Net.Http.OData;
using Xunit;

namespace Net.Http.WebApi.OData.Tests
{
    public class ODataRequestActionFilterAttributeTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void DoesNotAdd_MetadataLevel_ForMetadataRequest()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/$metadata");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler(TestHelper.ODataServiceOptions) { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(request, CancellationToken.None).Result;

            NameValueHeaderValue metadataParameter =
                httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

            Assert.Null(metadataParameter);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void SendAsync_ReturnsODataErrorContent_ForApplicationXml()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Accept.Clear();
            request.Headers.Add("Accept", "application/xml");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler(TestHelper.ODataServiceOptions) { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(request, CancellationToken.None).Result;

            Assert.Equal(HttpStatusCode.UnsupportedMediaType, httpResponseMessage.StatusCode);

            Assert.IsType<ObjectContent<ODataErrorContent>>(httpResponseMessage.Content);

            var objectContent = (ObjectContent<ODataErrorContent>)httpResponseMessage.Content;

            var odataErrorContent = (ODataErrorContent)objectContent.Value;

            Assert.Equal("415", odataErrorContent.Error.Code);
            Assert.Equal("A supported MIME type could not be found that matches the acceptable MIME types for the request. The supported type(s) 'application/json;odata.metadata=none, application/json;odata.metadata=minimal, application/json, text/plain' do not match any of the acceptable MIME types 'application/xml'.", odataErrorContent.Error.Message);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void SendAsync_ReturnsODataErrorContent_ForInvalidIsolationLevel()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Add(ODataRequestHeaderNames.ODataIsolation, "ReadCommitted");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler(TestHelper.ODataServiceOptions) { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(request, CancellationToken.None).Result;

            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);

            Assert.IsType<ObjectContent<ODataErrorContent>>(httpResponseMessage.Content);

            var objectContent = (ObjectContent<ODataErrorContent>)httpResponseMessage.Content;

            var odataErrorContent = (ODataErrorContent)objectContent.Value;

            Assert.Equal("400", odataErrorContent.Error.Code);
            Assert.Equal("If specified, the OData-Isolation must be 'Snapshot'.", odataErrorContent.Error.Message);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void SendAsync_ReturnsODataErrorContent_ForInvalidMaxVersion()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Add(ODataRequestHeaderNames.ODataVersion, "4.0");
            request.Headers.Add(ODataRequestHeaderNames.ODataMaxVersion, "3.0");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler(TestHelper.ODataServiceOptions) { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(request, CancellationToken.None).Result;

            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);

            Assert.IsType<ObjectContent<ODataErrorContent>>(httpResponseMessage.Content);

            var objectContent = (ObjectContent<ODataErrorContent>)httpResponseMessage.Content;

            var odataErrorContent = (ODataErrorContent)objectContent.Value;

            Assert.Equal("400", odataErrorContent.Error.Code);
            Assert.Equal("The OData version '3.0' specified in the OData-MaxVersion header indicating the maximum acceptable version of the response must be a valid OData version supported by this service between version '4.0' and '4.0'.", odataErrorContent.Error.Message);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void SendAsync_ReturnsODataErrorContent_ForInvalidMetadataLevel()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Add("Accept", "application/json;odata.metadata=all");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler(TestHelper.ODataServiceOptions) { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(request, CancellationToken.None).Result;

            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);

            Assert.IsType<ObjectContent<ODataErrorContent>>(httpResponseMessage.Content);

            var objectContent = (ObjectContent<ODataErrorContent>)httpResponseMessage.Content;

            var odataErrorContent = (ODataErrorContent)objectContent.Value;

            Assert.Equal("400", odataErrorContent.Error.Code);
            Assert.Equal("If specified, the odata.metadata value in the Accept header must be 'none', 'minimal' or 'full'.", odataErrorContent.Error.Message);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void SendAsync_ReturnsODataErrorContent_ForIsolationSnapshot()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Add(ODataRequestHeaderNames.ODataIsolation, "Snapshot");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler(TestHelper.ODataServiceOptions) { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(request, CancellationToken.None).Result;

            Assert.Equal(HttpStatusCode.PreconditionFailed, httpResponseMessage.StatusCode);

            Assert.IsType<ObjectContent<ODataErrorContent>>(httpResponseMessage.Content);

            var objectContent = (ObjectContent<ODataErrorContent>)httpResponseMessage.Content;

            var odataErrorContent = (ODataErrorContent)objectContent.Value;

            Assert.Equal("412", odataErrorContent.Error.Code);
            Assert.Equal($"{ODataRequestHeaderNames.ODataIsolation} 'Snapshot' is not supported by this service.", odataErrorContent.Error.Message);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void SendAsync_ReturnsODataErrorContent_ForMetadataLevelFull()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Add("Accept", "application/json;odata.metadata=full");

            var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler(TestHelper.ODataServiceOptions) { InnerHandler = new MockHttpMessageHandler() });

            HttpResponseMessage httpResponseMessage = invoker.SendAsync(request, CancellationToken.None).Result;

            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);

            Assert.IsType<ObjectContent<ODataErrorContent>>(httpResponseMessage.Content);

            var objectContent = (ObjectContent<ODataErrorContent>)httpResponseMessage.Content;

            var odataErrorContent = (ODataErrorContent)objectContent.Value;

            Assert.Equal("400", odataErrorContent.Error.Code);
            Assert.Equal("odata.metadata 'full' is not supported by this service, the metadata levels supported by this service are 'none, minimal'.", odataErrorContent.Error.Message);
        }

        public class WhenCalling_SendAsync_AndTheResponseHasNoContent
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public WhenCalling_SendAsync_AndTheResponseHasNoContent()
            {
                HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");

                var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler(TestHelper.ODataServiceOptions) { InnerHandler = new MockHttpMessageHandler(includeContentInResponse: false) });

                _httpResponseMessage = invoker.SendAsync(request, CancellationToken.None).Result;
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheMetadataLevelContentTypeParameterIsNotSetInTheResponse()
            {
                Assert.Null(_httpResponseMessage.Content);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheODataVersionHeaderIsSetInTheResponse()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataResponseHeaderNames.ODataVersion));
                Assert.Equal(ODataVersion.MaxVersion.ToString(), _httpResponseMessage.Headers.GetValues(ODataResponseHeaderNames.ODataVersion).Single());
            }
        }

        public class WhenCalling_SendAsync_WithAnODataUri_AndAllRequestOptionsInRequest
        {
            private readonly HttpResponseMessage _httpResponseMessage;
            private readonly ODataRequestOptions _odataRequestOptions;

            public WhenCalling_SendAsync_WithAnODataUri_AndAllRequestOptionsInRequest()
            {
                HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
                request.Headers.Add("Accept", "application/json;odata.metadata=none");
                request.Headers.Add(ODataRequestHeaderNames.ODataIsolation, "Snapshot");
                request.Headers.Add(ODataRequestHeaderNames.ODataMaxVersion, "4.0");
                request.Headers.Add(ODataRequestHeaderNames.ODataVersion, "4.0");

                var odataServiceOptions = new ODataServiceOptions(
                    ODataVersion.MinVersion,
                    ODataVersion.MaxVersion,
                    new[] { ODataIsolationLevel.Snapshot },
                    new[] { "application/json", "text/plain" });

                var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler(odataServiceOptions) { InnerHandler = new MockHttpMessageHandler() });

                _httpResponseMessage = invoker.SendAsync(request, CancellationToken.None).Result;

                request.Properties.TryGetValue(typeof(ODataRequestOptions).FullName, out object odataRequestOptions);
                _odataRequestOptions = odataRequestOptions as ODataRequestOptions;
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_DataServiceRoot_IsSet()
            {
                Assert.Equal("https://services.odata.org/OData/", _odataRequestOptions.ServiceRootUri.ToString());
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_IsolationLevel_IsSetTo_Snapshot()
            {
                Assert.Equal(ODataIsolationLevel.Snapshot, _odataRequestOptions.IsolationLevel);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_MaxVersion_IsSetTo_4_0()
            {
                Assert.Equal(ODataVersion.OData40, _odataRequestOptions.ODataMaxVersion);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_MetadataLevel_IsSetTo_None()
            {
                Assert.Equal(ODataMetadataLevel.None, _odataRequestOptions.MetadataLevel);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_Version_IsSetTo_4_0()
            {
                Assert.Equal(ODataVersion.OData40, _odataRequestOptions.ODataVersion);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void RequestParameters_ContainsODataRequestOptions()
            {
                Assert.NotNull(_odataRequestOptions);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheMetadataLevelContentTypeParameterIsSetInTheResponse()
            {
                NameValueHeaderValue metadataParameter =
                    _httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("none", metadataParameter.Value);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheODataVersionHeaderIsSetInTheResponse()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataResponseHeaderNames.ODataVersion));
                Assert.Equal(ODataVersion.OData40.ToString(), _httpResponseMessage.Headers.GetValues(ODataResponseHeaderNames.ODataVersion).Single());
            }
        }

        public class WhenCalling_SendAsync_WithAnODataUri_AndNoRequestOptionsInRequest
        {
            private readonly HttpResponseMessage _httpResponseMessage;
            private readonly ODataRequestOptions _odataRequestOptions;

            public WhenCalling_SendAsync_WithAnODataUri_AndNoRequestOptionsInRequest()
            {
                HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");

                var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler(TestHelper.ODataServiceOptions) { InnerHandler = new MockHttpMessageHandler() });

                _httpResponseMessage = invoker.SendAsync(request, CancellationToken.None).Result;

                request.Properties.TryGetValue(typeof(ODataRequestOptions).FullName, out object odataRequestOptions);
                _odataRequestOptions = odataRequestOptions as ODataRequestOptions;
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_DataServiceRoot_IsSet()
            {
                Assert.Equal("https://services.odata.org/OData/", _odataRequestOptions.ServiceRootUri.ToString());
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_IsolationLevel_DefaultsTo_None()
            {
                Assert.Equal(ODataIsolationLevel.None, _odataRequestOptions.IsolationLevel);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_MaxVersion_DefaultsTo_MaxVersion()
            {
                Assert.Equal(ODataVersion.MaxVersion, _odataRequestOptions.ODataMaxVersion);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_MetadataLevel_DefaultsTo_Minimal()
            {
                Assert.Equal(ODataMetadataLevel.Minimal, _odataRequestOptions.MetadataLevel);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void ODataRequestOptions_Version_DefaultsTo_MaxVersion()
            {
                Assert.Equal(ODataVersion.MaxVersion, _odataRequestOptions.ODataVersion);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void RequestParameters_ContainsODataRequestOptions()
            {
                Assert.NotNull(_odataRequestOptions);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheMetadataLevelContentTypeParameterIsSetInTheResponse()
            {
                NameValueHeaderValue metadataParameter =
                    _httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("minimal", metadataParameter.Value);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheODataVersionHeaderIsSetInTheResponse()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataResponseHeaderNames.ODataVersion));
                Assert.Equal(ODataVersion.MaxVersion.ToString(), _httpResponseMessage.Headers.GetValues(ODataResponseHeaderNames.ODataVersion).Single());
            }
        }

        public class WhenCalling_SendAsync_WithoutAnODataUri
        {
            private readonly HttpRequestMessage _request;
            private readonly HttpResponseMessage _httpResponseMessage;

            public WhenCalling_SendAsync_WithoutAnODataUri()
            {
                _request = TestHelper.CreateHttpRequest("/api/Products");

                var invoker = new HttpMessageInvoker(new ODataRequestDelegatingHandler(TestHelper.ODataServiceOptions) { InnerHandler = new MockHttpMessageHandler() });

                _httpResponseMessage = invoker.SendAsync(_request, CancellationToken.None).Result;
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void RequestParameters_DoesNotContainsODataRequestOptions()
            {
                Assert.False(_request.Properties.TryGetValue(typeof(ODataRequestOptions).FullName, out object odataRequestOptions));
                Assert.Null(odataRequestOptions);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheMetadataLevelContentTypeParameterIsNotSet()
            {
                Assert.DoesNotContain(_httpResponseMessage.Content.Headers.ContentType.Parameters, x => x.Name == ODataMetadataLevelExtensions.HeaderName);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheODataVersionHeaderIsNotSet()
            {
                Assert.False(_httpResponseMessage.Headers.Contains(ODataResponseHeaderNames.ODataVersion));
            }
        }

        private class MockHttpMessageHandler : HttpMessageHandler
        {
            private readonly bool _includeContentInResponse;

            public MockHttpMessageHandler(bool includeContentInResponse = true)
            {
                _includeContentInResponse = includeContentInResponse;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (_includeContentInResponse)
                {
                    return Task.FromResult(new HttpResponseMessage { Content = new StringContent("data") });
                }

                return Task.FromResult(new HttpResponseMessage());
            }
        }
    }
}
