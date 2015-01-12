namespace Net.Http.WebApi.Tests.OData.Query
{
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Expressions;
    using Xunit;

    public partial class FilterExpressionParserTests
    {
        public class ComplexExpressionTests
        {
            [Fact]
            public void ParseNestedFunctionCallEqPropertyExpression()
            {
                var queryNode = FilterExpressionParser.Parse("length(trim(CompanyName)) eq length(CompanyName)");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("length", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValueFunctionCallNode>(nodeLeft.Arguments[0]);
                var nodeLeftArg0 = (SingleValueFunctionCallNode)nodeLeft.Arguments[0];
                Assert.Equal("trim", nodeLeftArg0.Name);
                Assert.Equal(1, nodeLeftArg0.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftArg0.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeftArg0.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<SingleValueFunctionCallNode>(node.Right);
                var nodeRight = (SingleValueFunctionCallNode)node.Right;
                Assert.Equal("length", nodeRight.Name);
                Assert.Equal(1, nodeRight.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRight.Arguments[0]);
                var nodeRightArg0 = (SingleValuePropertyAccessNode)nodeRight.Arguments[0];
                Assert.Equal("CompanyName", nodeRightArg0.PropertyName);
            }

            [Fact]
            public void ParseNestedFunctionCallEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("length(trim(CompanyName)) eq 50");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<SingleValueFunctionCallNode>(node.Left);
                var nodeLeft = (SingleValueFunctionCallNode)node.Left;
                Assert.Equal("length", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Arguments.Count);
                Assert.IsType<SingleValueFunctionCallNode>(nodeLeft.Arguments[0]);
                var nodeLeftArg0 = (SingleValueFunctionCallNode)nodeLeft.Arguments[0];
                Assert.Equal("trim", nodeLeftArg0.Name);
                Assert.Equal(1, nodeLeftArg0.Arguments.Count);
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftArg0.Arguments[0]);
                Assert.Equal("CompanyName", ((SingleValuePropertyAccessNode)nodeLeftArg0.Arguments[0]).PropertyName);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("50", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(50, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueAndPropertyEqValueAndPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and Surname eq 'Smith' and Age eq 35");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;

                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)nodeLeftLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.And, nodeLeft.OperatorKind);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeLeftRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.Equal("Smith", ((ConstantNode)nodeLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                var nodeRightLeft = (SingleValuePropertyAccessNode)nodeRight.Left;
                Assert.Equal("Age", ((SingleValuePropertyAccessNode)nodeRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                var nodeRightRight = (ConstantNode)nodeRight.Right;
                Assert.Equal(35, ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueAndPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and Surname eq 'Smith'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)nodeLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("John", ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRight.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueAndYearFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Surname eq 'Smith' and year(BirthDate) eq 1971");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<SingleValueFunctionCallNode>(nodeRight.Left);
                Assert.Equal("year", ((SingleValueFunctionCallNode)nodeRight.Left).Name);
                var rightNodeLeft = (SingleValueFunctionCallNode)nodeRight.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(rightNodeLeft.Arguments[0]);
                Assert.Equal("BirthDate", ((SingleValuePropertyAccessNode)rightNodeLeft.Arguments[0]).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal(1971, ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' or Surname eq 'Smith'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)nodeLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("John", ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRight.Left);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeRight.Left).PropertyName);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueOrPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' or Surname eq 'Smith' or Age eq 35");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;

                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)nodeLeftLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeLeft.OperatorKind);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeLeftRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.Equal("Smith", ((ConstantNode)nodeLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                var nodeRightLeft = (SingleValuePropertyAccessNode)nodeRight.Left;
                Assert.Equal("Age", ((SingleValuePropertyAccessNode)nodeRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                var nodeRightRight = (ConstantNode)nodeRight.Right;
                Assert.Equal(35, ((ConstantNode)nodeRight.Right).Value);
            }
        }
    }
}