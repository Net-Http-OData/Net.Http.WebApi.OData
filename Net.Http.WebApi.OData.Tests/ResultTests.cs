namespace Net.Http.WebApi.OData.Tests
{
    using System.Collections.Generic;
    using Xunit;

    public class ResultTests
    {
        public class WhenConstructed
        {
            private readonly Result<int> result;
            private readonly List<int> results = new List<int> { 1, 2, 3 };

            public WhenConstructed()
            {
                this.result = new Result<int>(results);
            }

            [Fact]
            public void TheResultsAreSet()
            {
                Assert.Same(results, result.Results);
            }
        }
    }
}