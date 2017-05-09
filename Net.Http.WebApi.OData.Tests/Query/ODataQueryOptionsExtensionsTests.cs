namespace Net.Http.WebApi.OData.Tests.Query
{
    using System;
    using System.Net.Http;
    using OData.Model;
    using OData.Query;
    using Xunit;

    public class ODataQueryOptionsExtensionsTests
    {
        [Fact]
        public void NextLink_WithAllQueryOptions()
        {
            TestHelper.EnsureEDM();

            var queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$expand=Category&$filter=Name eq 'Milk'&$format=json&$inlinecount=allpages&$orderby=Name&$select=Name,Price&$skip=25&$top=25"),
                EntityDataModel.Current.EntitySets["Customers"]);

            Assert.Equal(new Uri("http://services.odata.org/OData/Customers?skip=75&$expand=Category&$filter=Name eq 'Milk'&$format=json&$inlinecount=allpages&$orderby=Name&$select=Name,Price&$top=25"), queryOptions.NextLink(50, 25));
        }

        [Fact]
        public void NextLink_WithNoQueryOptions()
        {
            TestHelper.EnsureEDM();

            var queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers"),
                EntityDataModel.Current.EntitySets["Customers"]);

            Assert.Equal(new Uri("http://services.odata.org/OData/Customers?skip=75"), queryOptions.NextLink(50, 25));
        }
    }
}