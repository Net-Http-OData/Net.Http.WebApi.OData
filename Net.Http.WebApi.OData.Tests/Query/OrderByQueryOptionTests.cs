namespace Net.Http.WebApi.Tests.OData.Query
{
    using Net.Http.WebApi.OData.Query;
    using Xunit;

    public class OrderByQueryOptionTests
    {
        public class WhenConstructedWithAValidValue
        {
            private readonly OrderByQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithAValidValue()
            {
                this.rawValue = "$orderby=Name,Age desc";
                this.option = new OrderByQueryOption(this.rawValue);
            }

            [Fact]
            public void ThePropertiesShouldContainTheCorrectNumberOfItems()
            {
                Assert.Equal(2, this.option.Properties.Count);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }
    }
}