using System;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Metadata;
using System.Web.Http.Routing;
using Moq;
using Net.Http.OData;
using Net.Http.OData.Query;
using Xunit;

namespace Net.Http.WebApi.OData.Tests
{
    public class ODataQueryOptionsHttpParameterBindingTests
    {
        [Fact]
        public void ExecuteBindingAsync_SetsODataQueryOptions()
        {
            TestHelper.EnsureEDM();

            var httpConfiguration = new HttpConfiguration();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$count=true");
            httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, httpConfiguration);
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Minimal, ODataVersion.OData40));

            var controllerContext = new HttpControllerContext(httpConfiguration, new Mock<IHttpRouteData>().Object, httpRequestMessage);

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
