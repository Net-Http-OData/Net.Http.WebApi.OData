namespace Net.Http.WebApi.OData.Tests
{
    using System.Linq;
    using System.Web.Http;
    using Xunit;

    public class HttpConfigurationODataExtensionsTests
    {
        [Fact]
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
        public void UseOData_Adds_ODataQueryOptionsHttpParameterBinding_ToParameterBindingRules()
        {
            var configuration = new HttpConfiguration();
            var parameterBindingRuleCount = configuration.ParameterBindingRules.Count;

            configuration.UseOData(
                _ =>
                {
                });

            // TODO: is there a way to know for certain since we're adding an anonymous method which returns the type? If not expecting the count to increase may be the only option.
            Assert.Equal(parameterBindingRuleCount + 1, configuration.ParameterBindingRules.Count);
        }

        [Fact]
        public void UseOData_Adds_ODataVersionHeaderValidationAttribute_ToFilters()
        {
            var configuration = new HttpConfiguration();

            configuration.UseOData(
                _ =>
                {
                });

            Assert.IsType<ODataVersionHeaderValidationAttribute>(configuration.Filters.ToList()[1].Instance);
        }
    }
}