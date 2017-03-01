namespace Net.Http.WebApi.Tests.OData.Query
{
    using Net.Http.WebApi.OData.Query;
    using WebApi.OData.Model;
    using Xunit;

    public class SelectExpandQueryOptionTests
    {
        public class WhenConstructedWithAValidValue
        {
            private readonly SelectExpandQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithAValidValue()
            {
                this.rawValue = "$select=Name,Age,Joined";
                this.option = new SelectExpandQueryOption(this.rawValue);
            }

            [Fact]
            public void ThePropertiesShouldContainEachSpecifiedValue()
            {
                foreach (var property in new[] { EdmProperty.From("Name"), EdmProperty.From("Age"), EdmProperty.From("Joined") })
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