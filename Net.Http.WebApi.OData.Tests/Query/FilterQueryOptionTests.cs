namespace Net.Http.WebApi.Tests.OData.Query
{
    using Net.Http.WebApi.OData.Query;
    using Xunit;

    public class FilterQueryOptionTests
    {
        public class WhenConstructedWithAValidValue
        {
            private readonly FilterQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithAValidValue()
            {
                this.rawValue = "$filter=Name eq 'John'";
                this.option = new FilterQueryOption(this.rawValue);
            }

            [Fact]
            public void TheExpressionShouldBeSet()
            {
                Assert.NotNull(this.option.Expression);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }
    }
}