namespace Net.Http.WebApi.Tests.OData.Query
{
    using Net.Http.WebApi.OData.Query;
    using Xunit;

    public class OrderByQueryOptionTests
    {
        public class WhenConstructedWithAMultipleValues
        {
            private readonly OrderByQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithAMultipleValues()
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

        public class WhenConstructedWithASingleValue
        {
            private readonly OrderByQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithASingleValue()
            {
                this.rawValue = "$orderby=Name";
                this.option = new OrderByQueryOption(this.rawValue);
            }

            [Fact]
            public void ThePropertiesShouldContainTheCorrectNumberOfItems()
            {
                Assert.Equal(1, this.option.Properties.Count);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }
    }
}