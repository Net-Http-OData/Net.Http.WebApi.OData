using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.Routing;
using Moq;
using Net.Http.OData.Query;
using Xunit;

namespace Net.Http.WebApi.OData.Tests
{
    public class ODataQueryOptionsHttpParameterBindingTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void ExecuteBindingAsync_SetsODataQueryOptions()
        {
            TestHelper.EnsureEDM();

            var httpConfiguration = new HttpConfiguration();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products?$count=true");

            var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, request);

            var actionContext = new HttpActionContext(controllerContext, Mock.Of<HttpActionDescriptor>());

            var mockParameterDescriptor = new Mock<HttpParameterDescriptor>();
            mockParameterDescriptor.Setup(x => x.ParameterName).Returns("queryOptions");

            var parameterBinding = new ODataQueryOptionsHttpParameterBinding(mockParameterDescriptor.Object);
            parameterBinding.ExecuteBindingAsync(Mock.Of<ModelMetadataProvider>(), actionContext, CancellationToken.None).Wait();

            Assert.NotNull(actionContext.ActionArguments["queryOptions"]);
            Assert.IsType<ODataQueryOptions>(actionContext.ActionArguments["queryOptions"]);

            var queryOptions = (ODataQueryOptions)actionContext.ActionArguments["queryOptions"];
            Assert.Equal("Products", queryOptions.EntitySet.Name);
            Assert.Equal("?$count=true", queryOptions.RawValues.ToString());
        }
    }
}
