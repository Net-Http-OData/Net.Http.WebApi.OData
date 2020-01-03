namespace Net.Http.OData.Tests.Query.Parsers
{
    using System.Net;
    using Net.Http.OData;
    using Net.Http.OData.Query.Expressions;
    using Net.Http.OData.Query.Parsers;
    using Xunit;

    public class BinaryOperatorKindParserTests
    {
        [Fact]
        public void ToBinaryOperatorKindReturnsAddForAdd()
        {
            Assert.Equal(BinaryOperatorKind.Add, "add".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsAndForAnd()
        {
            Assert.Equal(BinaryOperatorKind.And, "and".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsDivideForDiv()
        {
            Assert.Equal(BinaryOperatorKind.Divide, "div".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsEqualForEq()
        {
            Assert.Equal(BinaryOperatorKind.Equal, "eq".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsGreaterThanForGt()
        {
            Assert.Equal(BinaryOperatorKind.GreaterThan, "gt".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsGreaterThanOrEqualForGe()
        {
            Assert.Equal(BinaryOperatorKind.GreaterThanOrEqual, "ge".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsHasForHas()
        {
            Assert.Equal(BinaryOperatorKind.Has, "has".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsLessThanForLt()
        {
            Assert.Equal(BinaryOperatorKind.LessThan, "lt".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsLessThanOrEqualForLe()
        {
            Assert.Equal(BinaryOperatorKind.LessThanOrEqual, "le".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsModuloForMod()
        {
            Assert.Equal(BinaryOperatorKind.Modulo, "mod".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsMultiplyForMul()
        {
            Assert.Equal(BinaryOperatorKind.Multiply, "mul".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsNotEqualForNe()
        {
            Assert.Equal(BinaryOperatorKind.NotEqual, "ne".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsOrForOr()
        {
            Assert.Equal(BinaryOperatorKind.Or, "or".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindReturnsSubtractForSub()
        {
            Assert.Equal(BinaryOperatorKind.Subtract, "sub".ToBinaryOperatorKind());
        }

        [Fact]
        public void ToBinaryOperatorKindThrowsArgumentExceptionForUnsupportedOperatorKind()
        {
            var exception = Assert.Throws<ODataException>(() => "wibble".ToBinaryOperatorKind());

            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
            Assert.Equal("The operator 'wibble' is not a valid OData operator.", exception.Message);
        }
    }
}