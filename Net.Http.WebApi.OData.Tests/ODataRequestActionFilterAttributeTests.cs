using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Moq;
using Net.Http.OData;
using Xunit;

namespace Net.Http.WebApi.OData.Tests
{
    public class ODataRequestActionFilterAttributeTests
    {
        [Fact]
        public void OnActionExecuting_ThrowsODataException_ForInvalidIsolationLevel()
        {
            var httpConfiguration = new HttpConfiguration();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
            httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
            httpRequestMessage.Headers.Add(ODataHeaderNames.ODataIsolation, "ReadCommitted");

            var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

            var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

            var attribute = new ODataRequestActionFilterAttribute();
            var exception = Assert.Throws<ODataException>(() => attribute.OnActionExecuting(actionContext));

            Assert.Equal("If specified, the OData-Isolation must be 'Snapshot'", exception.Message);
            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        }

        [Fact]
        public void OnActionExecuting_ThrowsODataException_ForInvalidMaxVersion()
        {
            var httpConfiguration = new HttpConfiguration();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
            httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
            httpRequestMessage.Headers.Add(ODataHeaderNames.ODataMaxVersion, "3.0");

            var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

            var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

            var attribute = new ODataRequestActionFilterAttribute();
            var exception = Assert.Throws<ODataException>(() => attribute.OnActionExecuting(actionContext));

            Assert.Equal("If specified, the OData-MaxVersion header must be a valid OData version such as 4.0", exception.Message);
            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        }

        [Fact]
        public void OnActionExecuting_ThrowsODataException_ForInvalidMetadataLevel()
        {
            var httpConfiguration = new HttpConfiguration();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
            httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=all");

            var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

            var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

            var attribute = new ODataRequestActionFilterAttribute();
            var exception = Assert.Throws<ODataException>(() => attribute.OnActionExecuting(actionContext));

            Assert.Equal("If specified, the odata.metadata value in the Accept header must be 'none', 'minimal' or 'full'", exception.Message);
            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        }

        [Fact]
        public void OnActionExecuting_ThrowsODataException_ForInvalidVersion()
        {
            var httpConfiguration = new HttpConfiguration();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
            httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
            httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "3.0");

            var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

            var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

            var attribute = new ODataRequestActionFilterAttribute();
            var exception = Assert.Throws<ODataException>(() => attribute.OnActionExecuting(actionContext));

            Assert.Equal("If specified, the OData-Version header must be a valid OData version such as 4.0", exception.Message);
            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        }

        public class WhenCalling_OnActionExecuted_WithAnODataUri
        {
            private readonly HttpActionExecutedContext _actionExecutedContext;
            private readonly ODataVersion _odataVersion = ODataVersion.OData40;

            public WhenCalling_OnActionExecuted_WithAnODataUri()
            {
                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
                httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Minimal, _odataVersion));

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                _actionExecutedContext = new HttpActionExecutedContext(actionContext, null) { Response = new HttpResponseMessage { Content = new StringContent("data") } };

                var attribute = new ODataRequestActionFilterAttribute();
                attribute.OnActionExecuted(_actionExecutedContext);
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                NameValueHeaderValue metadataParameter = _actionExecutedContext.Response.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("minimal", metadataParameter.Value);
            }

            [Fact]
            public void TheODataVersionHeaderIsSet()
            {
                Assert.True(_actionExecutedContext.Response.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(_odataVersion.ToString(), _actionExecutedContext.Response.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }
        }

        public class WhenCalling_OnActionExecuted_WithoutAnODataUri
        {
            private readonly HttpActionExecutedContext _actionExecutedContext;

            public WhenCalling_OnActionExecuted_WithoutAnODataUri()
            {
                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/api/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                _actionExecutedContext = new HttpActionExecutedContext(actionContext, null) { Response = new HttpResponseMessage { Content = new StringContent("data") } };

                var attribute = new ODataRequestActionFilterAttribute();
                attribute.OnActionExecuted(_actionExecutedContext);
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsNotSet()
            {
                NameValueHeaderValue metadataParameter = _actionExecutedContext.Response.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.Null(metadataParameter);
            }

            [Fact]
            public void TheODataVersionHeaderIsNotSet()
            {
                Assert.False(_actionExecutedContext.Response.Headers.Contains(ODataHeaderNames.ODataVersion));
            }
        }

        public class WhenCalling_OnActionExecuting_WithAnODataUri_AndAllRequestOptionsInRequest
        {
            private readonly HttpActionContext _actionContext;
            private readonly ODataRequestOptions _odataRequestOptions;

            public WhenCalling_OnActionExecuting_WithAnODataUri_AndAllRequestOptionsInRequest()
            {
                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataIsolation, "Snapshot");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "4.0");

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                _actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataRequestActionFilterAttribute();
                attribute.OnActionExecuting(_actionContext);

                _actionContext.Request.Properties.TryGetValue(typeof(ODataRequestOptions).FullName, out object odataRequestOptions);
                _odataRequestOptions = odataRequestOptions as ODataRequestOptions;
            }

            [Fact]
            public void ODataRequestOptions_DataServiceRoot_IsSet()
            {
                Assert.Equal("http://services.odata.org/OData/", _odataRequestOptions.DataServiceRoot.ToString());
            }

            [Fact]
            public void ODataRequestOptions_IsolationLevel_IsSetTo_Snapshot()
            {
                Assert.Equal(ODataIsolationLevel.Snapshot, _odataRequestOptions.IsolationLevel);
            }

            [Fact]
            public void ODataRequestOptions_MetadataLevel_IsSetTo_Full()
            {
                Assert.Equal(ODataMetadataLevel.Full, _odataRequestOptions.MetadataLevel);
            }

            [Fact]
            public void ODataRequestOptions_Version_IsSetTo_4_0()
            {
                Assert.Equal(ODataVersion.MaxVersion, _odataRequestOptions.Version);
            }

            [Fact]
            public void RequestParameters_ContainsODataRequestOptions()
            {
                Assert.NotNull(_odataRequestOptions);
            }
        }

        public class WhenCalling_OnActionExecuting_WithAnODataUri_AndNoRequestOptionsInRequest
        {
            private readonly HttpActionContext _actionContext;
            private readonly ODataRequestOptions _odataRequestOptions;

            public WhenCalling_OnActionExecuting_WithAnODataUri_AndNoRequestOptionsInRequest()
            {
                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                _actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataRequestActionFilterAttribute();
                attribute.OnActionExecuting(_actionContext);

                _actionContext.Request.Properties.TryGetValue(typeof(ODataRequestOptions).FullName, out object odataRequestOptions);
                _odataRequestOptions = odataRequestOptions as ODataRequestOptions;
            }

            [Fact]
            public void ODataRequestOptions_DataServiceRoot_IsSet()
            {
                Assert.Equal("http://services.odata.org/OData/", _odataRequestOptions.DataServiceRoot.ToString());
            }

            [Fact]
            public void ODataRequestOptions_IsolationLevel_DefaultsTo_None()
            {
                Assert.Equal(ODataIsolationLevel.None, _odataRequestOptions.IsolationLevel);
            }

            [Fact]
            public void ODataRequestOptions_MetadataLevel_DefaultsTo_Minimal()
            {
                Assert.Equal(ODataMetadataLevel.Minimal, _odataRequestOptions.MetadataLevel);
            }

            [Fact]
            public void ODataRequestOptions_Version_DefaultsTo_MaxVersion()
            {
                Assert.Equal(ODataVersion.MaxVersion, _odataRequestOptions.Version);
            }

            [Fact]
            public void RequestParameters_ContainsODataRequestOptions()
            {
                Assert.NotNull(_odataRequestOptions);
            }
        }
    }
}
