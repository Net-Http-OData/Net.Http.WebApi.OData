namespace Net.Http.WebApi.Tests.OData.Query.Expressions
{
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query.Expressions;
    using Xunit;

    public class UnaryOperatorKindParserTests
    {
        [Fact]
        public void ToUnaryOperatorKindReturnsNotForNot()
        {
            Assert.Equal(UnaryOperatorKind.Not, UnaryOperatorKindParser.ToUnaryOperatorKind("not"));
        }

        [Fact]
        public void ToUnaryOperatorKindThrowsODataExceptionForUnsupportedOperatorKind()
        {
            Assert.Throws<ODataException>(() => UnaryOperatorKindParser.ToUnaryOperatorKind("wibble"));
        }
    }
}