using System.Linq;
using System.Web.Http;
using Net.Http.OData;
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

        [Fact]
        [Trait("Category", "Unit")]
        public void UseOData_Sets_ODataServiceOptionsCurrent()
        {
            ODataServiceOptions.Current = null;

            var configuration = new HttpConfiguration();

            configuration.UseOData(
                _ =>
                {
                });

            Assert.NotNull(ODataServiceOptions.Current);
            Assert.Equal(ODataVersion.MaxVersion, ODataServiceOptions.Current.MaxVersion);
            Assert.Equal(ODataVersion.MinVersion, ODataServiceOptions.Current.MinVersion);

            Assert.Equal(1, ODataServiceOptions.Current.SupportedIsolationLevels.Count);
            Assert.Contains(ODataIsolationLevel.None, ODataServiceOptions.Current.SupportedIsolationLevels);
            Assert.DoesNotContain(ODataIsolationLevel.Snapshot, ODataServiceOptions.Current.SupportedIsolationLevels);

            Assert.Equal(2, ODataServiceOptions.Current.SupportedMediaTypes.Count);
            Assert.Contains("application/json", ODataServiceOptions.Current.SupportedMediaTypes);
            Assert.Contains("text/plain", ODataServiceOptions.Current.SupportedMediaTypes);
        }
    }
}
