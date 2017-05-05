namespace Net.Http.WebApi.OData.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class ODataResponseContentTests
    {
        public class WhenConstructed
        {
            private readonly Uri context = new Uri("http://services.odata.org/OData/OData.svc/$metadata#Products");
            private readonly int count = 5;
            private readonly Uri nextLink = new Uri("http://services.odata.org/OData/OData.svc/Products?$skip=5");
            private readonly ODataResponseContent responseContent;
            private readonly List<int> value = new List<int> { 1, 2, 3 };

            public WhenConstructed()
            {
                this.responseContent = new ODataResponseContent(context, value, count, nextLink);
            }

            [Fact]
            public void TheContextIsSet()
            {
                Assert.Equal(context, responseContent.Context);
            }

            [Fact]
            public void TheCountIsSet()
            {
                Assert.Equal(count, responseContent.Count);
            }

            [Fact]
            public void TheNextLinkIsSet()
            {
                Assert.Equal(nextLink, responseContent.NextLink);
            }

            [Fact]
            public void TheValueIsSet()
            {
                Assert.Same(value, responseContent.Value);
            }
        }
    }
}