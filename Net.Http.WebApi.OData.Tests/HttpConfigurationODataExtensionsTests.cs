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