namespace Net.Http.WebApi.Tests.OData.Query
{
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Expressions;
    using Xunit;

    public partial class FilterExpressionParserTests
    {
        public class SingleValueFunctionCallTests
        {
            [Fact]
            public void ParseCeilingFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ceiling(Freight) eq 32");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("ceiling", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("Freight", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("32", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(32, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseConcatFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("concat(concat(City, ', '), Country) eq 'Berlin, Germany'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("concat", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValueFunctionCallNode>(nodeLeft.Arguments[0]);
                var nodeLeftArg0 = (SingleValueFunctionCallNode)nodeLeft.Arguments[0];
                Assert.Equal("concat", nodeLeftArg0.Name);
                Assert.Equal(2, nodeLeftArg0.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftArg0.Arguments[0]);
                Assert.Equal("City", ((SingleValuePropertyAccessNode)nodeLeftArg0.Arguments[0]).PropertyName);
                Assert.IsType<ConstantNode>(nodeLeftArg0.Arguments[1]);
                Assert.Equal(", ", ((ConstantNode)nodeLeftArg0.Arguments[1]).Value);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[1]);
                var nodeLeftArg1 = (SingleValuePropertyAccessNode)nodeLeft.Arguments[1];
                Assert.Equal("Country", nodeLeftArg1.PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                var nodeRight = (ConstantNode)node.Right;
                Assert.Equal("Berlin, Germany", nodeRight.LiteralText);
            }

            [Fact]
            public void ParseDayFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("day(BirthDate) eq 8");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("day", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("BirthDate", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("8", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(8, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseEndswithFunctionEqTrueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("endswith(CompanyName, 'Futterkiste') eq true");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("endswith", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);
                Assert.IsType<ConstantNode>(nodeLeft.Arguments[1]);
                Assert.Equal("Futterkiste", ((ConstantNode)nodeLeft.Arguments[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Arguments[1]).Value);
                Assert.Equal("Futterkiste", ((ConstantNode)nodeLeft.Arguments[1]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("true", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<bool>(((ConstantNode)node.Right).Value);
                Assert.True((bool)((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseEndswithFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("endswith(CompanyName, 'Futterkiste')");

                Assert.NotNull(queryNode);
                Assert.IsType<SingleValueFunctionCallNode>(queryNode);

                var node = (SingleValueFunctionCallNode)queryNode;

                Assert.Equal("endswith", node.Name);
                Assert.Equal(2, node.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(node.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)node.Arguments[0]).PropertyName);
                Assert.IsType<ConstantNode>(node.Arguments[1]);
                Assert.Equal("Futterkiste", ((ConstantNode)node.Arguments[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Arguments[1]).Value);
                Assert.Equal("Futterkiste", ((ConstantNode)node.Arguments[1]).Value);
            }

            [Fact]
            public void ParseFloorFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("floor(Freight) eq 32");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("floor", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("Freight", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("32", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(32, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseHourFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("hour(BirthDate) eq 4");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("hour", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("BirthDate", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("4", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(4, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseIndexOfFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("indexof(CompanyName, 'lfreds') eq 1");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("indexof", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);
                Assert.IsType<ConstantNode>(nodeLeft.Arguments[1]);
                Assert.Equal("lfreds", ((ConstantNode)nodeLeft.Arguments[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Arguments[1]).Value);
                Assert.Equal("lfreds", ((ConstantNode)nodeLeft.Arguments[1]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(1, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseLengthFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("length(CompanyName) eq 19");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("length", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("19", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(19, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseMinuteFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("minute(BirthDate) eq 40");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("minute", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("BirthDate", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("40", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(40, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseMonthFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("month(BirthDate) eq 5");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("month", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("BirthDate", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("5", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(5, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseNotEndsWithFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("not endswith(Name, 'ilk')");

                Assert.NotNull(queryNode);
                Assert.IsType<UnaryOperatorNode>(queryNode);

                var node = (UnaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Operand);
                var nodeOperand = (SingleValueFunctionCallNode)node.Operand;
                Assert.Equal("endswith", nodeOperand.Name);
                Assert.Equal(2, nodeOperand.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeOperand.Arguments[0]);
                Assert.Equal("Name", ((SingleValuePropertyAccessNode)nodeOperand.Arguments[0]).PropertyName);
                Assert.IsType<ConstantNode>(nodeOperand.Arguments[1]);
                Assert.Equal("ilk", ((ConstantNode)nodeOperand.Arguments[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeOperand.Arguments[1]).Value);
                Assert.Equal("ilk", ((ConstantNode)nodeOperand.Arguments[1]).Value);

                Assert.Equal(UnaryOperatorKind.Not, node.OperatorKind);
            }

            [Fact]
            public void ParseReplaceFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("replace(CompanyName, ' ', '') eq 'AlfredsFutterkiste'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("replace", nodeLeft.Name);
                Assert.Equal(3, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);
                Assert.IsType<ConstantNode>(nodeLeft.Arguments[1]);
                Assert.Equal(" ", ((ConstantNode)nodeLeft.Arguments[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Arguments[1]).Value);
                Assert.Equal(" ", ((ConstantNode)nodeLeft.Arguments[1]).Value);
                Assert.IsType<ConstantNode>(nodeLeft.Arguments[2]);
                Assert.Equal("", ((ConstantNode)nodeLeft.Arguments[2]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Arguments[2]).Value);
                Assert.Equal("", ((ConstantNode)nodeLeft.Arguments[2]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("AlfredsFutterkiste", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("AlfredsFutterkiste", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseRoundFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("round(Freight) eq 32");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("round", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("Freight", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("32", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(32, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseSecondFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("second(BirthDate) eq 40");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("second", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("BirthDate", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("40", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(40, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseStartswithFunctionEqTrueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("startswith(CompanyName, 'Alfr') eq true");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("startswith", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);
                Assert.IsType<ConstantNode>(nodeLeft.Arguments[1]);
                Assert.Equal("Alfr", ((ConstantNode)nodeLeft.Arguments[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Arguments[1]).Value);
                Assert.Equal("Alfr", ((ConstantNode)nodeLeft.Arguments[1]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("true", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<bool>(((ConstantNode)node.Right).Value);
                Assert.True((bool)((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseStartswithFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("startswith(CompanyName, 'Alfr')");

                Assert.NotNull(queryNode);
                Assert.IsType<SingleValueFunctionCallNode>(queryNode);

                var node = (SingleValueFunctionCallNode)queryNode;

                Assert.Equal("startswith", node.Name);
                Assert.Equal(2, node.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(node.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)node.Arguments[0]).PropertyName);
                Assert.IsType<ConstantNode>(node.Arguments[1]);
                Assert.Equal("Alfr", ((ConstantNode)node.Arguments[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Arguments[1]).Value);
                Assert.Equal("Alfr", ((ConstantNode)node.Arguments[1]).Value);
            }

            [Fact]
            public void ParseSubstringFunctionExpressionWithOneArgument()
            {
                var queryNode = FilterExpressionParser.Parse("substring(CompanyName, 1) eq 'lfreds Futterkiste'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("substring", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);
                Assert.IsType<ConstantNode>(nodeLeft.Arguments[1]);
                Assert.Equal("1", ((ConstantNode)nodeLeft.Arguments[1]).LiteralText);
                Assert.IsType<int>(((ConstantNode)nodeLeft.Arguments[1]).Value);
                Assert.Equal(1, ((ConstantNode)nodeLeft.Arguments[1]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("lfreds Futterkiste", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("lfreds Futterkiste", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseSubstringFunctionExpressionWithTwoArguments()
            {
                var queryNode = FilterExpressionParser.Parse("substring(CompanyName,1,2) eq 'lf'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("substring", nodeLeft.Name);
                Assert.Equal(3, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);
                Assert.IsType<ConstantNode>(nodeLeft.Arguments[1]);
                Assert.Equal("1", ((ConstantNode)nodeLeft.Arguments[1]).LiteralText);
                Assert.IsType<int>(((ConstantNode)nodeLeft.Arguments[1]).Value);
                Assert.Equal(1, ((ConstantNode)nodeLeft.Arguments[1]).Value);
                Assert.IsType<ConstantNode>(nodeLeft.Arguments[2]);
                Assert.Equal("2", ((ConstantNode)nodeLeft.Arguments[2]).LiteralText);
                Assert.IsType<int>(((ConstantNode)nodeLeft.Arguments[2]).Value);
                Assert.Equal(2, ((ConstantNode)nodeLeft.Arguments[2]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("lf", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("lf", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseSubstringOfFunctionEqTrueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("substringof('Alfreds', CompanyName) eq true");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("substringof", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Arguments.Count);
                Assert.IsType<ConstantNode>(nodeLeft.Arguments[0]);
                Assert.Equal("Alfreds", ((ConstantNode)nodeLeft.Arguments[0]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Arguments[0]).Value);
                Assert.Equal("Alfreds", ((ConstantNode)nodeLeft.Arguments[0]).Value);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[1]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[1]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("true", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<bool>(((ConstantNode)node.Right).Value);
                Assert.True((bool)((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseSubstringOfFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("substringof('Alfreds', CompanyName)");

                Assert.NotNull(queryNode);
                Assert.IsType<SingleValueFunctionCallNode>(queryNode);

                var node = (SingleValueFunctionCallNode)queryNode;

                Assert.Equal("substringof", node.Name);
                Assert.Equal(2, node.Arguments.Count);
                Assert.IsType<ConstantNode>(node.Arguments[0]);
                Assert.Equal("Alfreds", ((ConstantNode)node.Arguments[0]).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Arguments[0]).Value);
                Assert.Equal("Alfreds", ((ConstantNode)node.Arguments[0]).Value);
                Assert.IsType<SingleValuePropertyAccessNode>(node.Arguments[1]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)node.Arguments[1]).PropertyName);
            }

            [Fact]
            public void ParseToLowerFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("tolower(CompanyName) eq 'alfreds futterkiste'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("tolower", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("alfreds futterkiste", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("alfreds futterkiste", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseToUpperFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("toupper(CompanyName) eq 'ALFREDS FUTTERKISTE'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("toupper", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("ALFREDS FUTTERKISTE", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("ALFREDS FUTTERKISTE", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseTrimFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("trim(CompanyName) eq CompanyName");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("trim", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<SingleValuePropertyAccessNode>(node.Right);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)node.Right).PropertyName);
            }

            [Fact]
            public void ParseYearFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("year(BirthDate) eq 1971");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("year", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Arguments[0]);
                Assert.Equal("BirthDate", ((SingleValuePropertyAccessNode)nodeLeft.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1971", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(1971, ((ConstantNode)node.Right).Value);
            }
        }
    }
}