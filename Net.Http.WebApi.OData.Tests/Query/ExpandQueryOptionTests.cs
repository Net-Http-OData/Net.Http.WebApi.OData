namespace Net.Http.WebApi.Tests.OData.Query
{
    using Net.Http.WebApi.OData.Query;
    using Xunit;

    public class ExpandQueryOptionTests
    {
        public class WhenConstructedWithAValidValue
        {
            private readonly ExpandQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithAValidValue()
            {
                this.rawValue = "$expand=Invoices,Orders";
                this.option = new ExpandQueryOption(this.rawValue);
            }

            [Fact]
            public void ThePropertiesShouldContainEachSpecifiedValue()
            {
                foreach (var property in new[] { "Invoices", "Orders" })
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