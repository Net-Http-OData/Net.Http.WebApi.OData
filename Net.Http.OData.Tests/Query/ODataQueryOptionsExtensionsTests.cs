namespace Net.Http.OData.Tests.Query
{
    using System.Net.Http;
    using Net.Http.OData.Model;
    using Net.Http.OData.Query;
    using Net.Http.OData.Tests;
    using Xunit;

    public class ODataQueryOptionsExtensionsTests
    {
        [Fact]
        public void NextLink_WithAllQueryOptions()
        {
            TestHelper.EnsureEDM();

            var queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$count=true&$expand=Category&$filter=Name eq 'Milk'&$format=json&$orderby=Name&$search=blue OR green&$select=Name,Price$top=25"),
                EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("http://services.odata.org/OData/Products?$skip=75&$count=true&$expand=Category&$filter=Name eq 'Milk'&$format=json&$orderby=Name&$search=blue OR green&$select=Name,Price$top=25", queryOptions.NextLink(50, 25).ToString());
        }

        [Fact]
        public void NextLink_WithNoQueryOptions()
        {
            TestHelper.EnsureEDM();

            var queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers"),
                EntityDataModel.Current.EntitySets["Customers"]);

            Assert.Equal("http://services.odata.org/OData/Customers?$skip=75", queryOptions.NextLink(50, 25).ToString());
        }
    }
}