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
            public void ParseFunctionCallAndGroupedPropertyEqValueOrPropertyEqValue()
            {
                var queryNode = FilterExpressionParser.Parse("endswith(CompanyName, 'Futterkiste') and (Surname eq 'Smith' or Surname eq 'Smythe')");

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

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<BinaryOperatorNode>(nodeRight.Left);
                var nodeRightLeft = (BinaryOperatorNode)nodeRight.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRightLeft.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeRightLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightLeft.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeRightLeft.Right).LiteralText);
                Assert.Equal(BinaryOperatorKind.Or, nodeRight.OperatorKind);
                var nodeRightRight = (BinaryOperatorNode)nodeRight.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRightRight.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeRightRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightRight.Right);
                Assert.Equal("Smythe", ((ConstantNode)nodeRightRight.Right).LiteralText);
            }

            [Fact]
            public void ParseFunctionCallAndPropertyEqValue()
            {
                var queryNode = FilterExpressionParser.Parse("endswith(CompanyName, 'Futterkiste') and Surname eq 'Smith'");

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

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRight.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeRight.Right).LiteralText);
            }

            [Fact]
            public void ParseGroupedPropertyEqValueAndPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("(Forename eq 'John' and Surname eq 'Smith')");

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
            public void ParseGroupedPropertyEqValueAndPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("(Forename eq 'John' and Surname eq 'Smith') or Age eq 35");

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

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                var nodeRightLeft = (SingleValuePropertyAccessNode)nodeRight.Left;
                Assert.Equal("Age", ((SingleValuePropertyAccessNode)nodeRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                var nodeRightRight = (ConstantNode)nodeRight.Right;
                Assert.Equal(35, ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParseGroupedPropertyEqValueorPropertyEqValueAndGroupedPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("(Forename eq 'John' or Forename eq 'Joe') and (Surname eq 'Smith' or Surname eq 'Bloggs')");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)nodeLeftLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeftLeft.Right);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeLeft.OperatorKind);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)nodeLeftRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeftRight.Right);
                Assert.Equal("Joe", ((ConstantNode)nodeLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<BinaryOperatorNode>(nodeRight.Left);
                var nodeRightLeft = (BinaryOperatorNode)nodeRight.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRightLeft.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeRightLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightLeft.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeRightLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeRight.OperatorKind);
                var nodeRightRight = (BinaryOperatorNode)nodeRight.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRightRight.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeRightRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightRight.Right);
                Assert.Equal("Bloggs", ((ConstantNode)nodeRightRight.Right).Value);
            }

            [Fact]
            public void ParseNestedFunctionCallEqFunctionCallExpression()
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
            public void ParseOuterGroupedPropertyEqValueAndGroupedPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("(Forename eq 'John' and (Surname eq 'Smith' or Surname eq 'Smythe'))");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)nodeLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("John", ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<BinaryOperatorNode>(nodeRight.Left);
                var nodeRightLeft = (BinaryOperatorNode)nodeRight.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRightLeft.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeRightLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightLeft.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeRightLeft.Right).LiteralText);
                Assert.Equal(BinaryOperatorKind.Or, nodeRight.OperatorKind);
                var nodeRightRight = (BinaryOperatorNode)nodeRight.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRightRight.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeRightRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightRight.Right);
                Assert.Equal("Smythe", ((ConstantNode)nodeRightRight.Right).LiteralText);
            }

            [Fact]
            public void ParsePropertyEqValueAndGroupedPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and (Surname eq 'Smith' or Surname eq 'Smythe')");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)nodeLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("John", ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<BinaryOperatorNode>(nodeRight.Left);
                var nodeRightLeft = (BinaryOperatorNode)nodeRight.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRightLeft.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeRightLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightLeft.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeRightLeft.Right).LiteralText);
                Assert.Equal(BinaryOperatorKind.Or, nodeRight.OperatorKind);
                var nodeRightRight = (BinaryOperatorNode)nodeRight.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRightRight.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeRightRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightRight.Right);
                Assert.Equal("Smythe", ((ConstantNode)nodeRightRight.Right).LiteralText);
            }

            [Fact]
            public void ParsePropertyEqValueAndPropertyEqValueAndPropertyEqValueAndPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and Surname eq 'Smith' and Age eq 35 and Title eq 'Mr'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeftLeft.Left);
                var nodeLeftLeftLeft = (BinaryOperatorNode)nodeLeftLeft.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftLeftLeft.Left);
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)nodeLeftLeftLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeftLeft.OperatorKind);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.And, nodeLeftLeft.OperatorKind);
                var nodeLeftLeftRight = (BinaryOperatorNode)nodeLeftLeft.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftLeftRight.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeLeftLeftRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeftRight.OperatorKind);
                Assert.Equal("Smith", ((ConstantNode)nodeLeftLeftRight.Right).Value);
                Assert.Equal(BinaryOperatorKind.And, nodeLeft.OperatorKind);
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Right);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Age", ((SingleValuePropertyAccessNode)nodeLeftRight.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRight.Left);
                Assert.Equal("Title", ((SingleValuePropertyAccessNode)nodeRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal("Mr", ((ConstantNode)nodeRight.Right).Value);
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
            public void ParsePropertyEqValueAndPropertyEqValueOrPropertyEqValueUnGroupedExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and Surname eq 'Smith' or Age eq 35");

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

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                var nodeRightLeft = (SingleValuePropertyAccessNode)nodeRight.Left;
                Assert.Equal("Age", ((SingleValuePropertyAccessNode)nodeRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                var nodeRightRight = (ConstantNode)nodeRight.Right;
                Assert.Equal(35, ((ConstantNode)nodeRight.Right).Value);
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

            [Fact]
            public void ParsePropertyEqValueOrPropertyEqValueOrPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' or Surname eq 'Smith' or Age eq 35 or Title eq 'Mr'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeftLeft.Left);
                var nodeLeftLeftLeft = (BinaryOperatorNode)nodeLeftLeft.Left;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftLeftLeft.Left);
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)nodeLeftLeftLeft.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeftLeft.OperatorKind);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeLeftLeft.OperatorKind);
                var nodeLeftLeftRight = (BinaryOperatorNode)nodeLeftLeft.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftLeftRight.Left);
                Assert.Equal("Surname", ((SingleValuePropertyAccessNode)nodeLeftLeftRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeftRight.OperatorKind);
                Assert.Equal("Smith", ((ConstantNode)nodeLeftLeftRight.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeLeft.OperatorKind);
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Right);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Age", ((SingleValuePropertyAccessNode)nodeLeftRight.Left).PropertyName);

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<SingleValuePropertyAccessNode>(nodeRight.Left);
                Assert.Equal("Title", ((SingleValuePropertyAccessNode)nodeRight.Left).PropertyName);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal("Mr", ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueOrYearFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Surname eq 'Smith' or year(BirthDate) eq 1971");

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

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

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
        }
    }
}