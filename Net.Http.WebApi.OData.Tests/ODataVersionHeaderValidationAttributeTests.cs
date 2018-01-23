namespace Net.Http.WebApi.OData.Tests
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Routing;
    using Moq;
    using Xunit;

    public class ODataVersionHeaderValidationAttributeTests
    {
        public class WhenCallingOnActionExecuting_WithODataMaxVersionHeaderContaining1_0
        {
            private readonly ODataVersionHeaderValidationAttribute attribute = new ODataVersionHeaderValidationAttribute();

            [Fact]
            public void AnHttpResponseExceptionIsThrown()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataMaxVersion, "1.0");
                var controllerContext = new HttpControllerContext(new HttpConfiguration(), new Mock<IHttpRouteData>().Object, httpRequestMessage);
                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);
                actionContext.ModelState.AddModelError("Foo", "Error");

                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedODataVersion, ((HttpError)((ObjectContent<HttpError>)actionContext.Response.Content).Value).Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataMaxVersionHeaderContaining2_0
        {
            private readonly ODataVersionHeaderValidationAttribute attribute = new ODataVersionHeaderValidationAttribute();

            [Fact]
            public void AnHttpResponseExceptionIsThrown()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataMaxVersion, "2.0");
                var controllerContext = new HttpControllerContext(new HttpConfiguration(), new Mock<IHttpRouteData>().Object, httpRequestMessage);
                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);
                actionContext.ModelState.AddModelError("Foo", "Error");

                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedODataVersion, ((HttpError)((ObjectContent<HttpError>)actionContext.Response.Content).Value).Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataMaxVersionHeaderContaining3_0
        {
            private readonly ODataVersionHeaderValidationAttribute attribute = new ODataVersionHeaderValidationAttribute();

            [Fact]
            public void AnHttpResponseExceptionIsThrown()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataMaxVersion, "3.0");
                var controllerContext = new HttpControllerContext(new HttpConfiguration(), new Mock<IHttpRouteData>().Object, httpRequestMessage);
                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);
                actionContext.ModelState.AddModelError("Foo", "Error");

                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedODataVersion, ((HttpError)((ObjectContent<HttpError>)actionContext.Response.Content).Value).Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataMaxVersionHeaderContaining4_0
        {
            private readonly ODataVersionHeaderValidationAttribute attribute = new ODataVersionHeaderValidationAttribute();

            [Fact]
            public void TheResponseShouldNotBeSet()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataMaxVersion, "4.0");
                var controllerContext = new HttpControllerContext(new HttpConfiguration(), new Mock<IHttpRouteData>().Object, httpRequestMessage);
                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);
                actionContext.ModelState.Clear();

                attribute.OnActionExecuting(actionContext);

                Assert.Null(actionContext.Response);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataVersionHeaderContaining1_0
        {
            private readonly ODataVersionHeaderValidationAttribute attribute = new ODataVersionHeaderValidationAttribute();

            [Fact]
            public void TheResponseStatusCodeShouldBeSetToNotAcceptable()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "1.0");
                var controllerContext = new HttpControllerContext(new HttpConfiguration(), new Mock<IHttpRouteData>().Object, httpRequestMessage);
                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);
                actionContext.ModelState.AddModelError("Foo", "Error");

                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedODataVersion, ((HttpError)((ObjectContent<HttpError>)actionContext.Response.Content).Value).Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataVersionHeaderContaining2_0
        {
            private readonly ODataVersionHeaderValidationAttribute attribute = new ODataVersionHeaderValidationAttribute();

            [Fact]
            public void TheResponseStatusCodeShouldBeSetToNotAcceptable()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "2.0");
                var controllerContext = new HttpControllerContext(new HttpConfiguration(), new Mock<IHttpRouteData>().Object, httpRequestMessage);
                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);
                actionContext.ModelState.AddModelError("Foo", "Error");

                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedODataVersion, ((HttpError)((ObjectContent<HttpError>)actionContext.Response.Content).Value).Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataVersionHeaderContaining3_0
        {
            private readonly ODataVersionHeaderValidationAttribute attribute = new ODataVersionHeaderValidationAttribute();

            [Fact]
            public void TheResponseStatusCodeShouldBeSetToNotAcceptable()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "3.0");
                var controllerContext = new HttpControllerContext(new HttpConfiguration(), new Mock<IHttpRouteData>().Object, httpRequestMessage);
                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);
                actionContext.ModelState.AddModelError("Foo", "Error");

                attribute.OnActionExecuting(actionContext);

                Assert.Equal(HttpStatusCode.NotAcceptable, actionContext.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedODataVersion, ((HttpError)((ObjectContent<HttpError>)actionContext.Response.Content).Value).Message);
            }
        }

        public class WhenCallingOnActionExecuting_WithODataVersionHeaderContaining4_0
        {
            private readonly ODataVersionHeaderValidationAttribute attribute = new ODataVersionHeaderValidationAttribute();

            [Fact]
            public void TheResponseShouldNotBeSet()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataVersion, "4.0");
                var controllerContext = new HttpControllerContext(new HttpConfiguration(), new Mock<IHttpRouteData>().Object, httpRequestMessage);
                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);
                actionContext.ModelState.Clear();

                attribute.OnActionExecuting(actionContext);

                Assert.Null(actionContext.Response);
            }
        }

        public class WhenCallingOnActionExecuting_WithoutODataMaxVersionHeader
        {
            private readonly ODataVersionHeaderValidationAttribute attribute = new ODataVersionHeaderValidationAttribute();

            [Fact]
            public void TheResponseShouldNotBeSet()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                var controllerContext = new HttpControllerContext(new HttpConfiguration(), new Mock<IHttpRouteData>().Object, httpRequestMessage);
                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);
                actionContext.ModelState.Clear();

                attribute.OnActionExecuting(actionContext);

                Assert.Null(actionContext.Response);
            }
        }

        public class WhenCallingOnActionExecuting_WithoutODataVersionHeader
        {
            private readonly ODataVersionHeaderValidationAttribute attribute = new ODataVersionHeaderValidationAttribute();

            [Fact]
            public void TheResponseShouldNotBeSet()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products");
                var controllerContext = new HttpControllerContext(new HttpConfiguration(), new Mock<IHttpRouteData>().Object, httpRequestMessage);
                var actionContext = new HttpActionContext(controllerContext, new Mock<HttpActionDescriptor>().Object);
                actionContext.ModelState.Clear();

                attribute.OnActionExecuting(actionContext);

                Assert.Null(actionContext.Response);
            }
        }
    }
}