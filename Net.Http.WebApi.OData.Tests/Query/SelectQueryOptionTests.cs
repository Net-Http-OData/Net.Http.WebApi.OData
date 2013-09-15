namespace Net.Http.WebApi.Tests.OData.Query
{
    using Net.Http.WebApi.OData.Query;
    using Xunit;

    public class SelectQueryOptionTests
    {
        public class WhenConstructedWithAValidValue
        {
            private readonly SelectQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithAValidValue()
            {
                this.rawValue = "$select=Name,Age,Joined";
                this.option = new SelectQueryOption(this.rawValue);
            }

            [Fact]
            public void ThePropertiesShouldContainEachSpecifiedValue()
            {
                foreach (var property in new[] { "Name", "Age", "Joined" })
                {
                    Assert.Contains(property, this.option.Properties);
                }
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }
    }
}