namespace Net.Http.WebApi.Tests.OData
{
    using System.Collections.Generic;
    using Net.Http.WebApi.OData;
    using Xunit;

    public class InlineCountTests
    {
        public class WhenCreated
        {
            private readonly int count = 5;
            private readonly InlineCount<int> inlineCount;
            private readonly List<int> results = new List<int> { 1, 2, 3 };

            public WhenCreated()
            {
                this.inlineCount = new InlineCount<int>(results, count);
            }

            [Fact]
            public void TheCountIsSet()
            {
                Assert.Equal(count, inlineCount.Count);
            }

            [Fact]
            public void TheResultsAreSet()
            {
                Assert.Same(results, inlineCount.Results);
            }
        }
    }
}