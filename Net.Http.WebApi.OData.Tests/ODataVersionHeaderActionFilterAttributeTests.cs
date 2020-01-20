using System.Linq;
using System.Net;
using System.Net.Http;
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
    public class ODataVersionHeaderActionFilterAttributeTests
    {
        public class WhenCallingOnActionExecuted_WithAnODataUri
        {
            [Fact]
            public void TheODataVersionHeaderIsSet()
            {
                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var actionExecutedContext = new HttpActionExecutedContext(actionContext, null) { Response = new HttpResponseMessage() };

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuted(actionExecutedContext);

                Assert.True(actionExecutedContext.Response.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataHeaderValues.ODataVersionString, actionExecutedContext.Response.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }
        }

        public class WhenCallingOnActionExecuted_WithoutAnODataUri
        {
            [Fact]
            public void TheODataVersionHeaderIsNotSet()
            {
                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var actionExecutedContext = new HttpActionExecutedContext(actionContext, null) { Response = new HttpResponseMessage() };

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuted(actionExecutedContext);

                Assert.False(actionExecutedContext.Response.Headers.Contains(ODataHeaderNames.ODataVersion));
            }
        }

        public class WhenCallingOnActionExecuting_WithODataMaxVersionHeaderContaining1_0
        {
            [Fact]
            public void TheResponseIsSetToAnODataErrorContent_WithStatusNotAcceptable()
            {
                TestHelper.EnsureEDM();

                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataMaxVersion, "1.0");

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal("406", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Code);
                Assert.Equal("This service only supports OData 4.0", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataMaxVersionHeaderContaining2_0
        {
            [Fact]
            public void TheResponseIsSetToAnODataErrorContent_WithStatusNotAcceptable()
            {
                TestHelper.EnsureEDM();

                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataMaxVersion, "2.0");

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal("406", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Code);
                Assert.Equal("This service only supports OData 4.0", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataMaxVersionHeaderContaining3_0
        {
            [Fact]
            public void TheResponseIsSetToAnODataErrorContent_WithStatusNotAcceptable()
            {
                TestHelper.EnsureEDM();

                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataMaxVersion, "3.0");

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal("406", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Code);
                Assert.Equal("This service only supports OData 4.0", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataMaxVersionHeaderContaining4_0
        {
            [Fact]
            public void TheResponseShouldNotBeSet()
            {
                TestHelper.EnsureEDM();

                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataMaxVersion, "4.0");

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuting(actionContext);

                Assert.Null(actionContext.Response);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataMaxVersionHeaderContaining5_0
        {
            [Fact]
            public void TheResponseIsSetToAnODataErrorContent_WithStatusNotAcceptable()
            {
                TestHelper.EnsureEDM();

                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataMaxVersion, "5.0");

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal("406", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Code);
                Assert.Equal("This service only supports OData 4.0", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataVersionHeaderContaining1_0
        {
            [Fact]
            public void TheResponseIsSetToAnODataErrorContent_WithStatusNotAcceptable()
            {
                TestHelper.EnsureEDM();

                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "1.0");

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal("406", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Code);
                Assert.Equal("This service only supports OData 4.0", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataVersionHeaderContaining2_0
        {
            [Fact]
            public void TheResponseIsSetToAnODataErrorContent_WithStatusNotAcceptable()
            {
                TestHelper.EnsureEDM();

                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "2.0");

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal("406", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Code);
                Assert.Equal("This service only supports OData 4.0", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataVersionHeaderContaining3_0
        {
            [Fact]
            public void TheResponseIsSetToAnODataErrorContent_WithStatusNotAcceptable()
            {
                TestHelper.EnsureEDM();

                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "3.0");

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal("406", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Code);
                Assert.Equal("This service only supports OData 4.0", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataVersionHeaderContaining4_0
        {
            [Fact]
            public void TheResponseShouldNotBeSet()
            {
                TestHelper.EnsureEDM();

                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "4.0");

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuting(actionContext);

                Assert.Null(actionContext.Response);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataVersionHeaderContaining5_0
        {
            [Fact]
            public void TheResponseIsSetToAnODataErrorContent_WithStatusNotAcceptable()
            {
                TestHelper.EnsureEDM();

                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "5.0");

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal("406", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Code);
                Assert.Equal("This service only supports OData 4.0", ((ODataErrorContent)((ObjectContent<ODataErrorContent>)actionContext.Response.Content).Value).Error.Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithoutODataVersionHeaders
        {
            [Fact]
            public void TheResponseShouldNotBeSet()
            {
                TestHelper.EnsureEDM();

                var httpConfiguration = new HttpConfiguration();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);

                var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);

                var attribute = new ODataVersionHeaderActionFilterAttribute();
                attribute.OnActionExecuting(actionContext);

                Assert.Null(actionContext.Response);
            }
        }
    }
}
