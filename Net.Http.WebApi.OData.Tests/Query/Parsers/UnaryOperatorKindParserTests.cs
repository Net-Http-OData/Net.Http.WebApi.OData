namespace Net.Http.WebApi.Tests.OData.Query.Parsers
{
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query.Expressions;
    using Net.Http.WebApi.OData.Query.Parsers;
    using Xunit;

    public class UnaryOperatorKindParserTests
    {
        [Fact]
        public void ToUnaryOperatorKindReturnsNotForNot()
        {
            Assert.Equal(UnaryOperatorKind.Not, "not".ToUnaryOperatorKind());
        }

        [Fact]
        public void ToUnaryOperatorKindThrowsODataExceptionForUnsupportedOperatorKind()
        {
            Assert.Throws<ODataException>(() => "wibble".ToUnaryOperatorKind());
        }
    }
}