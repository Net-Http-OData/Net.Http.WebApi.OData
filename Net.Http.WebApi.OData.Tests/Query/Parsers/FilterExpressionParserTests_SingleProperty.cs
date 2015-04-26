namespace Net.Http.WebApi.Tests.OData.Query.Parsers
{
    using System;
    using Net.Http.WebApi.OData.Query.Expressions;
    using Net.Http.WebApi.OData.Query.Parsers;
    using Xunit;

    public partial class FilterExpressionParserTests
    {
        public class SingleValuePropertyValueTests
        {
            [Fact]
            public void ParsePropertyAddValueEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price add 2.45M eq 5.00M");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Price", ((SingleValuePropertyAccessNode)nodeLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Add, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("2.45M", ((ConstantNode)nodeLeft.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)nodeLeft.Right).Value);
                Assert.Equal(2.45M, ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("5.00M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(5.00M, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyDivValueEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price div 2.55M eq 1M");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Price", ((SingleValuePropertyAccessNode)nodeLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Divide, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("2.55M", ((ConstantNode)nodeLeft.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)nodeLeft.Right).Value);
                Assert.Equal(2.55M, ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(1M, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateHourMinuteValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Created eq datetime'2013-06-18T09:30'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Created", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2013-06-18T09:30", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTime>(((ConstantNode)node.Right).Value);
                Assert.Equal(new DateTime(2013, 6, 18, 9, 30, 0), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateOnlyValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Created eq datetime'2013-06-18'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Created", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2013-06-18", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTime>(((ConstantNode)node.Right).Value);
                Assert.Equal(new DateTime(2013, 6, 18), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateTimeOffsetValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("LastUpdated eq datetimeoffset'2002-10-10T17:00:00Z'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("LastUpdated", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2002-10-10T17:00:00Z", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTimeOffset>(((ConstantNode)node.Right).Value);
                Assert.Equal(new DateTimeOffset(2002, 10, 10, 17, 0, 0, TimeSpan.Zero), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateTimeSFormatValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Created eq datetime'2013-06-18T09:30:20'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Created", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2013-06-18T09:30:20", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTime>(((ConstantNode)node.Right).Value);
                Assert.Equal(new DateTime(2013, 6, 18, 9, 30, 20), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqFalseValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Deleted eq false");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Deleted", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("false", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<bool>(((ConstantNode)node.Right).Value);
                Assert.False((bool)((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqGuidValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("TransactionId eq guid'0D01B09B-38CD-4C53-AA04-181371087A00'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("TransactionId", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("0D01B09B-38CD-4C53-AA04-181371087A00", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<Guid>(((ConstantNode)node.Right).Value);
                Assert.Equal(new Guid("0D01B09B-38CD-4C53-AA04-181371087A00"), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNegativeDecimalValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq -1234.567M");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Amount", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("-1234.567M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(-1234.567M, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNegativeDoubleValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq -1234.567D");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Amount", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("-1234.567D", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<double>(((ConstantNode)node.Right).Value);
                Assert.Equal(-1234.567D, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNegativeFloatValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq -1234.567F");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Amount", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("-1234.567F", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<float>(((ConstantNode)node.Right).Value);
                Assert.Equal(-1234.567F, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNegativeInt32ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq -1234");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Amount", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("-1234", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(-1234, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNegativeInt64ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq -1234L");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Amount", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("-1234L", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<long>(((ConstantNode)node.Right).Value);
                Assert.Equal(-1234L, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNullValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("MiddleName eq null");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("MiddleName", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("null", ((ConstantNode)node.Right).LiteralText);
                Assert.Null(((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPositiveDecimalValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq 1234.567M");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Amount", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1234.567M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(1234.567M, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPositiveDoubleValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq 1234.567D");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Amount", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1234.567D", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<double>(((ConstantNode)node.Right).Value);
                Assert.Equal(1234.567D, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPositiveFloatValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq 1234.567F");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Amount", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1234.567F", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<float>(((ConstantNode)node.Right).Value);
                Assert.Equal(1234.567F, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPositiveInt32ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq 1234");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Amount", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1234", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(1234, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPositiveInt64ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq 1234L");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Amount", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1234L", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<long>(((ConstantNode)node.Right).Value);
                Assert.Equal(1234L, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPropertyExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq Surname");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<SingleValuePropertyAccessNode>(node.Right);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)node.Right).PropertyName);
            }

            [Fact]
            public void ParsePropertyEqStringValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Name eq 'Milk'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Name", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'Milk'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("Milk", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqStringValueWithQuoteCharacterExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Name eq 'O''Brien'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Name", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'O''Brien'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("O'Brien", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqTimeValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("LastUpdated eq time'13:20:00'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("LastUpdated", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("13:20:00", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<TimeSpan>(((ConstantNode)node.Right).Value);
                Assert.Equal(new TimeSpan(13, 20, 0), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqTrueValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Deleted eq true");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Deleted", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("true", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<bool>(((ConstantNode)node.Right).Value);
                Assert.True((bool)((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyGeThanStringValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Name ge 'Milk'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Name", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.GreaterThanOrEqual, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'Milk'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("Milk", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyGtThanStringValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Name gt 'Milk'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Name", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.GreaterThan, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'Milk'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("Milk", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyLeThanStringValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Name le 'Milk'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Name", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.LessThanOrEqual, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'Milk'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("Milk", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyLtThanStringValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Name lt 'Milk'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Name", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.LessThan, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'Milk'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("Milk", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyModValueEqPropertyExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Rating mod 5 eq 0");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Rating", ((SingleValuePropertyAccessNode)nodeLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Modulo, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("5", ((ConstantNode)nodeLeft.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)nodeLeft.Right).Value);
                Assert.Equal(5, ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("0", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(0, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyMulValueEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price mul 2.0M eq 5.10M");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Price", ((SingleValuePropertyAccessNode)nodeLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Multiply, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("2.0M", ((ConstantNode)nodeLeft.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)nodeLeft.Right).Value);
                Assert.Equal(2.0M, ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("5.10M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(5.10M, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyNeStringValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Name ne 'Milk'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
                Assert.Equal("Name", ((SingleValuePropertyAccessNode)node.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.NotEqual, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'Milk'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("Milk", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertySubValueEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price sub 0.55M eq 2.00M");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Price", ((SingleValuePropertyAccessNode)nodeLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Subtract, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("0.55M", ((ConstantNode)nodeLeft.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)nodeLeft.Right).Value);
                Assert.Equal(0.55M, ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2.00M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(2.00M, ((ConstantNode)node.Right).Value);
            }
        }
    }
}