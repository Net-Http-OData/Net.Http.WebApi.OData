namespace Net.Http.WebApi.Tests.OData.Query.Parsers
{
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query.Expressions;
    using Net.Http.WebApi.OData.Query.Parsers;
    using Xunit;

    public class BinaryOperatorKindParserTests
    {
        [Fact]
        public void ToBinaryOperatorKindReturnsAddForAdd()
        {
            Assert.Equal(BinaryOperatorKind.Add, BinaryOperatorKindParser.ToBinaryOperatorKind("add"));
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsAndForAnd()
        {
            Assert.Equal(BinaryOperatorKind.And, BinaryOperatorKindParser.ToBinaryOperatorKind("and"));
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsDivideForDiv()
        {
            Assert.Equal(BinaryOperatorKind.Divide, BinaryOperatorKindParser.ToBinaryOperatorKind("div"));
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsEqualForEq()
        {
            Assert.Equal(BinaryOperatorKind.Equal, BinaryOperatorKindParser.ToBinaryOperatorKind("eq"));
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsGreaterThanForGt()
        {
            Assert.Equal(BinaryOperatorKind.GreaterThan, BinaryOperatorKindParser.ToBinaryOperatorKind("gt"));
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsGreaterThanOrEqualForGe()
        {
            Assert.Equal(BinaryOperatorKind.GreaterThanOrEqual, BinaryOperatorKindParser.ToBinaryOperatorKind("ge"));
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsLessThanForLt()
        {
            Assert.Equal(BinaryOperatorKind.LessThan, BinaryOperatorKindParser.ToBinaryOperatorKind("lt"));
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsLessThanOrEqualForLe()
        {
            Assert.Equal(BinaryOperatorKind.LessThanOrEqual, BinaryOperatorKindParser.ToBinaryOperatorKind("le"));
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsModuloForMod()
        {
            Assert.Equal(BinaryOperatorKind.Modulo, BinaryOperatorKindParser.ToBinaryOperatorKind("mod"));
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsMultiplyForMul()
        {
            Assert.Equal(BinaryOperatorKind.Multiply, BinaryOperatorKindParser.ToBinaryOperatorKind("mul"));
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsNotEqualForNe()
        {
            Assert.Equal(BinaryOperatorKind.NotEqual, BinaryOperatorKindParser.ToBinaryOperatorKind("ne"));
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsOrForOr()
        {
            Assert.Equal(BinaryOperatorKind.Or, BinaryOperatorKindParser.ToBinaryOperatorKind("or"));
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsSubtractForSub()
        {
            Assert.Equal(BinaryOperatorKind.Subtract, BinaryOperatorKindParser.ToBinaryOperatorKind("sub"));
        }

        [Fact]
        public void ToBinaryOperatorKindThrowsODataExceptionForUnsupportedOperatorKind()
        {
            Assert.Throws<ODataException>(() => BinaryOperatorKindParser.ToBinaryOperatorKind("wibble"));
        }
    }
}