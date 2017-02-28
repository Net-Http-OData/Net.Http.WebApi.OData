namespace Net.Http.WebApi.Tests.OData.Query
{
    using Net.Http.WebApi.OData.Query;
    using Xunit;

    public class SearchQueryOptionTests
    {
        public class WhenConstructedWithAValidValue
        {
            private readonly SearchQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithAValidValue()
            {
                this.rawValue = "$search=blue OR green";
                this.option = new SearchQueryOption(this.rawValue);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }
    }
}