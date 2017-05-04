namespace Net.Http.WebApi.OData.Tests
{
    using System.Collections.Generic;
    using Xunit;

    public class PagedResultTests
    {
        public class WhenConstructed
        {
            private readonly int count = 5;
            private readonly PagedResult<int> pagedResult;
            private readonly List<int> results = new List<int> { 1, 2, 3 };

            public WhenConstructed()
            {
                this.pagedResult = new PagedResult<int>(results, count);
            }

            [Fact]
            public void TheCountIsSet()
            {
                Assert.Equal(count, pagedResult.Count);
            }

            [Fact]
            public void TheResultsAreSet()
            {
                Assert.Same(results, pagedResult.Results);
            }
        }
    }
}