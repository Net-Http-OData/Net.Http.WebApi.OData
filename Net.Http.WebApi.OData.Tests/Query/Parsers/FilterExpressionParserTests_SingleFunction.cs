namespace Net.Http.WebApi.OData.Tests.Query.Parsers
{
    using Net.Http.WebApi.OData.Model;
    using Net.Http.WebApi.OData.Query.Expressions;
    using Net.Http.WebApi.OData.Query.Parsers;
    using Xunit;

    public partial class FilterExpressionParserTests
    {
        public class SingleValueFunctionCallTests
        {
            public SingleValueFunctionCallTests()
            {
                TestHelper.EnsureEDM();
            }

            [Fact]
            public void ParseCastFunctionWithExpressionAndTypeExpression()
            {
                var queryNode = FilterExpressionParser.Parse("cast(Rating, 'Edm.Int64') eq 20", EntityDataModel.Current.EntitySets["Products"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("cast", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("Rating", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeLeft.Parameters[1]);
                Assert.Equal("'Edm.Int64'", ((ConstantNode)nodeLeft.Parameters[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Parameters[1]).Value);
                Assert.Equal("Edm.Int64", ((ConstantNode)nodeLeft.Parameters[1]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("20", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(20, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseCastFunctionWithTypeOnlyExpression()
            {
                var queryNode = FilterExpressionParser.Parse("cast('Edm.Int64')", EntityDataModel.Current.EntitySets["Products"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<FunctionCallNode>(queryNode);

                var node = (FunctionCallNode)queryNode;

                Assert.Equal("cast", node.Name);
                Assert.Equal(1, node.Parameters.Count);
                Assert.IsType<ConstantNode>(node.Parameters[0]);
                Assert.Equal("'Edm.Int64'", ((ConstantNode)node.Parameters[0]).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Parameters[0]).Value);
                Assert.Equal("Edm.Int64", ((ConstantNode)node.Parameters[0]).Value);
            }

            [Fact]
            public void ParseCeilingFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ceiling(Freight) eq 32", EntityDataModel.Current.EntitySets["Orders"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("ceiling", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("Freight", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("32", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(32, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseConcatFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("concat(concat(City, ', '), Country) eq 'Berlin, Germany'", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("concat", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Parameters.Count);
                Assert.IsType<FunctionCallNode>(nodeLeft.Parameters[0]);
                var nodeLeftArg0 = (FunctionCallNode)nodeLeft.Parameters[0];
                Assert.Equal("concat", nodeLeftArg0.Name);
                Assert.Equal(2, nodeLeftArg0.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeftArg0.Parameters[0]);
                Assert.Equal("City", ((PropertyAccessNode)nodeLeftArg0.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeLeftArg0.Parameters[1]);
                Assert.Equal(", ", ((ConstantNode)nodeLeftArg0.Parameters[1]).Value);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[1]);
                var nodeLeftArg1 = (PropertyAccessNode)nodeLeft.Parameters[1];
                Assert.Equal("Country", nodeLeftArg1.Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                var nodeRight = (ConstantNode)node.Right;
                Assert.Equal("'Berlin, Germany'", nodeRight.LiteralText);
            }

            [Fact]
            public void ParseContainsFunctionEqTrueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("contains(CompanyName,'Alfreds') eq true", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("contains", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeLeft.Parameters[1]);
                Assert.Equal("'Alfreds'", ((ConstantNode)nodeLeft.Parameters[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Parameters[1]).Value);
                Assert.Equal("Alfreds", ((ConstantNode)nodeLeft.Parameters[1]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("true", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<bool>(((ConstantNode)node.Right).Value);
                Assert.True((bool)((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseContainsFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("contains(CompanyName,'Alfreds')", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<FunctionCallNode>(queryNode);

                var node = (FunctionCallNode)queryNode;

                Assert.Equal("contains", node.Name);
                Assert.Equal(2, node.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(node.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)node.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(node.Parameters[1]);
                Assert.Equal("'Alfreds'", ((ConstantNode)node.Parameters[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Parameters[1]).Value);
                Assert.Equal("Alfreds", ((ConstantNode)node.Parameters[1]).Value);
            }

            [Fact]
            public void ParseDayFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("day(BirthDate) eq 8", EntityDataModel.Current.EntitySets["Employees"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("day", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("BirthDate", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("8", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(8, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseEndswithFunctionEqTrueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("endswith(CompanyName, 'Futterkiste') eq true", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("endswith", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeLeft.Parameters[1]);
                Assert.Equal("'Futterkiste'", ((ConstantNode)nodeLeft.Parameters[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Parameters[1]).Value);
                Assert.Equal("Futterkiste", ((ConstantNode)nodeLeft.Parameters[1]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Same(ConstantNode.True, node.Right);
            }

            [Fact]
            public void ParseEndswithFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("endswith(CompanyName, 'Futterkiste')", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<FunctionCallNode>(queryNode);

                var node = (FunctionCallNode)queryNode;

                Assert.Equal("endswith", node.Name);
                Assert.Equal(2, node.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(node.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)node.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(node.Parameters[1]);
                Assert.Equal("'Futterkiste'", ((ConstantNode)node.Parameters[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Parameters[1]).Value);
                Assert.Equal("Futterkiste", ((ConstantNode)node.Parameters[1]).Value);
            }

            [Fact]
            public void ParseFloorFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("floor(Freight) eq 32", EntityDataModel.Current.EntitySets["Orders"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("floor", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("Freight", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("32", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(32, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseFractionalSecondsFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("fractionalseconds(BirthDate) lt 0.1m", EntityDataModel.Current.EntitySets["Employees"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("fractionalseconds", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("BirthDate", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.LessThan, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("0.1m", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(0.1m, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseHourFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("hour(BirthDate) eq 4", EntityDataModel.Current.EntitySets["Employees"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("hour", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("BirthDate", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("4", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(4, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseIndexOfFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("indexof(CompanyName, 'lfreds') eq 1", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("indexof", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeLeft.Parameters[1]);
                Assert.Equal("'lfreds'", ((ConstantNode)nodeLeft.Parameters[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Parameters[1]).Value);
                Assert.Equal("lfreds", ((ConstantNode)nodeLeft.Parameters[1]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(1, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseIsOfFunctionWithExpressionAndTypeExpression()
            {
                var queryNode = FilterExpressionParser.Parse("isof(ShipCountry, 'Edm.String')", EntityDataModel.Current.EntitySets["Orders"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<FunctionCallNode>(queryNode);

                var node = (FunctionCallNode)queryNode;

                Assert.Equal("isof", node.Name);
                Assert.Equal(2, node.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(node.Parameters[0]);
                Assert.Equal("ShipCountry", ((PropertyAccessNode)node.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(node.Parameters[1]);
                Assert.Equal("'Edm.String'", ((ConstantNode)node.Parameters[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Parameters[1]).Value);
                Assert.Equal("Edm.String", ((ConstantNode)node.Parameters[1]).Value);
            }

            [Fact]
            public void ParseIsOfFunctionWithTypeOnlyExpression()
            {
                var queryNode = FilterExpressionParser.Parse("isof('NorthwindModel.Order')", EntityDataModel.Current.EntitySets["Orders"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<FunctionCallNode>(queryNode);

                var node = (FunctionCallNode)queryNode;

                Assert.Equal("isof", node.Name);
                Assert.Equal(1, node.Parameters.Count);
                Assert.IsType<ConstantNode>(node.Parameters[0]);
                Assert.Equal("'NorthwindModel.Order'", ((ConstantNode)node.Parameters[0]).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Parameters[0]).Value);
                Assert.Equal("NorthwindModel.Order", ((ConstantNode)node.Parameters[0]).Value);
            }

            [Fact]
            public void ParseLengthFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("length(CompanyName) eq 19", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("length", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("19", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(19, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseMaxDateTimeFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq maxdatetime()", EntityDataModel.Current.EntitySets["Products"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                var nodeLeft = (PropertyAccessNode)node.Left;
                Assert.Equal("ReleaseDate", nodeLeft.Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<FunctionCallNode>(node.Right);
                Assert.Equal("maxdatetime", ((FunctionCallNode)node.Right).Name);
                Assert.Equal(0, ((FunctionCallNode)node.Right).Parameters.Count);
            }

            [Fact]
            public void ParseMinDateTimeFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq mindatetime()", EntityDataModel.Current.EntitySets["Products"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                var nodeLeft = (PropertyAccessNode)node.Left;
                Assert.Equal("ReleaseDate", nodeLeft.Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<FunctionCallNode>(node.Right);
                Assert.Equal("mindatetime", ((FunctionCallNode)node.Right).Name);
                Assert.Equal(0, ((FunctionCallNode)node.Right).Parameters.Count);
            }

            [Fact]
            public void ParseMinuteFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("minute(BirthDate) eq 40", EntityDataModel.Current.EntitySets["Employees"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("minute", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("BirthDate", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("40", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(40, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseMonthFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("month(BirthDate) eq 5", EntityDataModel.Current.EntitySets["Employees"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("month", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("BirthDate", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("5", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(5, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseNotEndsWithFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("not endswith(Description, 'ilk')", EntityDataModel.Current.EntitySets["Products"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<UnaryOperatorNode>(queryNode);

                var node = (UnaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Operand);
                var nodeOperand = (FunctionCallNode)node.Operand;
                Assert.Equal("endswith", nodeOperand.Name);
                Assert.Equal(2, nodeOperand.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeOperand.Parameters[0]);
                Assert.Equal("Description", ((PropertyAccessNode)nodeOperand.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeOperand.Parameters[1]);
                Assert.Equal("'ilk'", ((ConstantNode)nodeOperand.Parameters[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeOperand.Parameters[1]).Value);
                Assert.Equal("ilk", ((ConstantNode)nodeOperand.Parameters[1]).Value);

                Assert.Equal(UnaryOperatorKind.Not, node.OperatorKind);
            }

            [Fact]
            public void ParseNowFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate ge now()", EntityDataModel.Current.EntitySets["Products"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                var nodeLeft = (PropertyAccessNode)node.Left;
                Assert.Equal("ReleaseDate", nodeLeft.Property.Name);

                Assert.Equal(BinaryOperatorKind.GreaterThanOrEqual, node.OperatorKind);

                Assert.IsType<FunctionCallNode>(node.Right);
                Assert.Equal("now", ((FunctionCallNode)node.Right).Name);
                Assert.Equal(0, ((FunctionCallNode)node.Right).Parameters.Count);
            }

            [Fact]
            public void ParseReplaceFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("replace(CompanyName, ' ', '') eq 'AlfredsFutterkiste'", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("replace", nodeLeft.Name);
                Assert.Equal(3, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeLeft.Parameters[1]);
                Assert.Equal("' '", ((ConstantNode)nodeLeft.Parameters[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Parameters[1]).Value);
                Assert.Equal(" ", ((ConstantNode)nodeLeft.Parameters[1]).Value);
                Assert.IsType<ConstantNode>(nodeLeft.Parameters[2]);
                Assert.Equal("''", ((ConstantNode)nodeLeft.Parameters[2]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Parameters[2]).Value);
                Assert.Equal("", ((ConstantNode)nodeLeft.Parameters[2]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'AlfredsFutterkiste'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("AlfredsFutterkiste", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseRoundFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("round(Freight) eq 32", EntityDataModel.Current.EntitySets["Orders"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("round", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("Freight", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("32", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(32, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseSecondFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("second(BirthDate) eq 40", EntityDataModel.Current.EntitySets["Employees"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("second", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("BirthDate", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("40", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(40, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseStartswithFunctionEqTrueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("startswith(CompanyName, 'Alfr') eq true", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("startswith", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeLeft.Parameters[1]);
                Assert.Equal("'Alfr'", ((ConstantNode)nodeLeft.Parameters[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)nodeLeft.Parameters[1]).Value);
                Assert.Equal("Alfr", ((ConstantNode)nodeLeft.Parameters[1]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Same(ConstantNode.True, node.Right);
            }

            [Fact]
            public void ParseStartswithFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("startswith(CompanyName, 'Alfr')", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<FunctionCallNode>(queryNode);

                var node = (FunctionCallNode)queryNode;

                Assert.Equal("startswith", node.Name);
                Assert.Equal(2, node.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(node.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)node.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(node.Parameters[1]);
                Assert.Equal("'Alfr'", ((ConstantNode)node.Parameters[1]).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Parameters[1]).Value);
                Assert.Equal("Alfr", ((ConstantNode)node.Parameters[1]).Value);
            }

            [Fact]
            public void ParseSubstringFunctionExpressionWithOneArgument()
            {
                var queryNode = FilterExpressionParser.Parse("substring(CompanyName, 1) eq 'lfreds Futterkiste'", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("substring", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeLeft.Parameters[1]);
                Assert.Equal("1", ((ConstantNode)nodeLeft.Parameters[1]).LiteralText);
                Assert.IsType<int>(((ConstantNode)nodeLeft.Parameters[1]).Value);
                Assert.Equal(1, ((ConstantNode)nodeLeft.Parameters[1]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'lfreds Futterkiste'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("lfreds Futterkiste", ((ConstantNode)node.Right).Value);
            }

            /// <summary>
            /// https://github.com/TrevorPilley/Net.Http.WebApi.OData/issues/57 - Nested function call parsing error.
            /// </summary>
            [Fact]
            public void ParseSubstringFunctionExpressionWithOneArgumentWhichIsAlsoAFunction()
            {
                var queryNode = FilterExpressionParser.Parse("substring(tolower(CompanyName), 'alfreds futterkiste')", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<FunctionCallNode>(queryNode);

                var node = (FunctionCallNode)queryNode;
                Assert.Equal("substring", node.Name);
                Assert.Equal(2, node.Parameters.Count);
                Assert.IsType<FunctionCallNode>(node.Parameters[0]);

                var firstParameter = (FunctionCallNode)node.Parameters[0];
                Assert.Equal("tolower", firstParameter.Name);
                Assert.Equal(1, firstParameter.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(firstParameter.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)firstParameter.Parameters[0]).Property.Name);

                Assert.IsType<ConstantNode>(node.Parameters[1]);
                Assert.Equal("'alfreds futterkiste'", ((ConstantNode)node.Parameters[1]).LiteralText);
            }

            [Fact]
            public void ParseSubstringFunctionExpressionWithTwoArguments()
            {
                var queryNode = FilterExpressionParser.Parse("substring(CompanyName,1,2) eq 'lf'", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("substring", nodeLeft.Name);
                Assert.Equal(3, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeLeft.Parameters[1]);
                Assert.Equal("1", ((ConstantNode)nodeLeft.Parameters[1]).LiteralText);
                Assert.IsType<int>(((ConstantNode)nodeLeft.Parameters[1]).Value);
                Assert.Equal(1, ((ConstantNode)nodeLeft.Parameters[1]).Value);
                Assert.IsType<ConstantNode>(nodeLeft.Parameters[2]);
                Assert.Equal("2", ((ConstantNode)nodeLeft.Parameters[2]).LiteralText);
                Assert.IsType<int>(((ConstantNode)nodeLeft.Parameters[2]).Value);
                Assert.Equal(2, ((ConstantNode)nodeLeft.Parameters[2]).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'lf'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("lf", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseToLowerFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("tolower(CompanyName) eq 'alfreds futterkiste'", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("tolower", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'alfreds futterkiste'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("alfreds futterkiste", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseToUpperFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("toupper(CompanyName) eq 'ALFREDS FUTTERKISTE'", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("toupper", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'ALFREDS FUTTERKISTE'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("ALFREDS FUTTERKISTE", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParseTrimFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("trim(CompanyName) eq CompanyName", EntityDataModel.Current.EntitySets["Customers"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("trim", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<PropertyAccessNode>(node.Right);
                Assert.Equal("CompanyName", ((PropertyAccessNode)node.Right).Property.Name);
            }

            [Fact]
            public void ParseYearFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("year(BirthDate) eq 1971", EntityDataModel.Current.EntitySets["Employees"].EdmType);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("year", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("BirthDate", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1971", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(1971, ((ConstantNode)node.Right).Value);
            }
        }
    }
}