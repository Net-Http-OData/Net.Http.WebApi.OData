using System.Linq;
using System.Web.Http;
using Xunit;

namespace Net.Http.WebApi.OData.Tests
{
    public class ODataHttpConfigurationExtensionsTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void UseOData_Adds_ODataExceptionFilterAttribute_ToFilters()
        {
            var configuration = new HttpConfiguration();

            configuration.UseOData(
                _ =>
                {
                });

            Assert.IsType<ODataExceptionFilterAttribute>(configuration.Filters.ToList()[0].Instance);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void UseOData_Adds_ODataQueryOptionsHttpParameterBinding_ToParameterBindingRules()
        {
            var configuration = new HttpConfiguration();
            int parameterBindingRuleCount = configuration.ParameterBindingRules.Count;

            configuration.UseOData(
                _ =>
                {
                });

            // TODO: is there a way to know for certain since we're adding an anonymous method which returns the type? If not expecting the count to increase may be the only option.
            Assert.Equal(parameterBindingRuleCount + 1, configuration.ParameterBindingRules.Count);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void UseOData_Adds_ODataRequestDelegatingHandler_ToMessageHandlers()
        {
            var configuration = new HttpConfiguration();

            configuration.UseOData(
                _ =>
                {
                });

            Assert.IsType<ODataRequestDelegatingHandler>(configuration.MessageHandlers.ToList()[0]);
        }
    }
}
