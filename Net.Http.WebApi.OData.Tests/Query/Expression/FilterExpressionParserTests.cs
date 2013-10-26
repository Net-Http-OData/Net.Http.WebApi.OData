namespace Net.Http.WebApi.Tests.OData.Query.Expression
{
    using System;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query.Expression;
    using Xunit;

    public class FilterExpressionParserTests
    {
        [Fact]
        public void BindFilterThrowsODataExceptionForUnspportedExpression()
        {
            var exception = Assert.Throws<ODataException>(() => FilterExpressionParser.Parse("length(trim(CompanyName)) eq length(CompanyName)"));

            Assert.Equal("The expression 'length(trim(CompanyName)) eq length(CompanyName)' is not currently supported.", exception.Message);
        }

        public class ParseEndswithFunctionExpression
        {
            private readonly BinaryOperatorNode node;

            public ParseEndswithFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("endswith(Name, 'yes') eq true");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheLeftNodeFirstArgumentShouldBeThePropertyNode()
            {
                Assert.IsType<SingleValuePropertyAccessNode>(((SingleValueFunctionCallNode)this.node.Left).Arguments[0]);
            }

            [Fact]
            public void TheLeftNodeFirstArgumentShouldContainThePropertyName()
            {
                var queryNode = (SingleValuePropertyAccessNode)((SingleValueFunctionCallNode)this.node.Left).Arguments[0];

                Assert.Equal("Name", queryNode.PropertyName);
            }

            [Fact]
            public void TheLeftNodeSecondArgumentShouldBeTheConstantNode()
            {
                Assert.IsType<ConstantNode>(((SingleValueFunctionCallNode)this.node.Left).Arguments[1]);
            }

            [Fact]
            public void TheLeftNodeSecondArgumentShouldContainTheConstantValue()
            {
                var queryNode = (ConstantNode)((SingleValueFunctionCallNode)this.node.Left).Arguments[1];

                Assert.Equal("yes", queryNode.Value);
            }

            [Fact]
            public void TheLeftNodeShouldBeTheSingleValueFunctionCallNode()
            {
                Assert.IsType<SingleValueFunctionCallNode>(this.node.Left);
            }

            [Fact]
            public void TheLeftNodeShouldContainTheFunctionName()
            {
                Assert.Equal("endswith", ((SingleValueFunctionCallNode)this.node.Left).Name);
            }

            [Fact]
            public void TheNodeReturnedShouldBeABinaryOperatorNode()
            {
                Assert.NotNull(this.node);
            }

            [Fact]
            public void TheOperatorKindShouldBeEqual()
            {
                Assert.Equal(BinaryOperatorKind.Equal, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightNodeShouldBeTheConstantNode()
            {
                Assert.IsType<ConstantNode>(this.node.Right);
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("true", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(true, ((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParsePropertyEqDateHourMinuteValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqDateHourMinuteValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Created eq datetime'2013-06-18T09:30'");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("2013-06-18T09:30", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(new DateTime(2013, 6, 18, 9, 30, 0), ((ConstantNode)this.node.Right).Value);
            }

            [Fact]
            public void TheRightNodeValueShouldBeADateTime()
            {
                Assert.IsType<DateTime>(((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParsePropertyEqDateOnlyValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqDateOnlyValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Created eq datetime'2013-06-18'");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("2013-06-18", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(new DateTime(2013, 6, 18), ((ConstantNode)this.node.Right).Value);
            }

            [Fact]
            public void TheRightNodeValueShouldBeADateTime()
            {
                Assert.IsType<DateTime>(((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParsePropertyEqDateTimeSFormatValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqDateTimeSFormatValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Created eq datetime'2013-06-18T09:30:20'");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("2013-06-18T09:30:20", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(new DateTime(2013, 6, 18, 9, 30, 20), ((ConstantNode)this.node.Right).Value);
            }

            [Fact]
            public void TheRightNodeValueShouldBeADateTime()
            {
                Assert.IsType<DateTime>(((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParsePropertyEqDecimalValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqDecimalValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq 1234.567M");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("1234.567M", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(1234.567M, ((ConstantNode)this.node.Right).Value);
            }

            [Fact]
            public void TheRightNodeValueShouldBeADecimal()
            {
                Assert.IsType<Decimal>(((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParsePropertyEqDoubleValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqDoubleValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq 1234.567D");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("1234.567D", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(1234.567D, ((ConstantNode)this.node.Right).Value);
            }

            [Fact]
            public void TheRightNodeValueShouldBeADouble()
            {
                Assert.IsType<Double>(((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParsePropertyEqFalseValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqFalseValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Deleted eq false");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("false", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(false, ((ConstantNode)this.node.Right).Value);
            }

            [Fact]
            public void TheRightNodeValueShouldBeABoolean()
            {
                Assert.IsType<Boolean>(((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParsePropertyEqGuidValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqGuidValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("TxID eq guid'0D01B09B-38CD-4C53-AA04-181371087A00'");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("0D01B09B-38CD-4C53-AA04-181371087A00", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(Guid.Parse("0D01B09B-38CD-4C53-AA04-181371087A00"), ((ConstantNode)this.node.Right).Value);
            }

            [Fact]
            public void TheRightNodeValueShouldBeAGuid()
            {
                Assert.IsType<Guid>(((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParsePropertyEqIntValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqIntValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq 1234");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("1234", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(1234, ((ConstantNode)this.node.Right).Value);
            }

            [Fact]
            public void TheRightNodeValueShouldBeAnInt32()
            {
                Assert.IsType<int>(((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParsePropertyEqSingleValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqSingleValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Amount eq 1234.567F");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("1234.567F", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(1234.567F, ((ConstantNode)this.node.Right).Value);
            }

            [Fact]
            public void TheRightNodeValueShouldBeASingle()
            {
                Assert.IsType<Single>(((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParsePropertyEqStringValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqStringValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John'");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheLeftNodeShouldBeThePropertyAccessNode()
            {
                Assert.IsType<SingleValuePropertyAccessNode>(this.node.Left);
            }

            [Fact]
            public void TheLeftNodeShouldContainThePropertyName()
            {
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)this.node.Left).PropertyName);
            }

            [Fact]
            public void TheNodeReturnedShouldBeABinaryOperatorNode()
            {
                Assert.NotNull(this.node);
            }

            [Fact]
            public void TheOperatorKindShouldBeEqual()
            {
                Assert.Equal(BinaryOperatorKind.Equal, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightNodeShouldBeTheConstantNode()
            {
                Assert.IsType<ConstantNode>(this.node.Right);
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("John", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal("John", ((ConstantNode)this.node.Right).Value);
            }

            [Fact]
            public void TheRightNodeValueShouldBeAString()
            {
                Assert.IsType<string>(((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParsePropertyEqTrueValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqTrueValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Enabled eq true");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("true", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(true, ((ConstantNode)this.node.Right).Value);
            }

            [Fact]
            public void TheRightNodeValueShouldBeABoolean()
            {
                Assert.IsType<Boolean>(((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParsePropertyEqValueAndPropertyEqValueAndPropertyEqValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqValueAndPropertyEqValueAndPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and Middlename eq 'Albert' and Surname eq 'Smith'");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheLeftNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(this.node.Left);
            }

            [Fact]
            public void TheOperatorKindShouldBeAnd()
            {
                Assert.Equal(BinaryOperatorKind.And, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(this.node.Right);
            }

            [Fact]
            public void TheRightNodesLeftNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(((BinaryOperatorNode)this.node.Right).Left);
            }

            [Fact]
            public void TheRightNodesOperatorKindShouldBeAnd()
            {
                Assert.Equal(BinaryOperatorKind.And, ((BinaryOperatorNode)this.node.Right).OperatorKind);
            }

            [Fact]
            public void TheRightNodesRightNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(((BinaryOperatorNode)this.node.Right).Right);
            }
        }

        public class ParsePropertyEqValueAndPropertyEqValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqValueAndPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and Surname eq 'Smith'");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheLeftNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(this.node.Left);
            }

            [Fact]
            public void TheLeftNodesLeftNodeShouldBeASingleValuePropertyAccessNode()
            {
                Assert.IsType<SingleValuePropertyAccessNode>(((BinaryOperatorNode)this.node.Left).Left);
            }

            [Fact]
            public void TheLeftNodesOperatorKindShouldBeEqual()
            {
                Assert.Equal(BinaryOperatorKind.Equal, ((BinaryOperatorNode)this.node.Left).OperatorKind);
            }

            [Fact]
            public void TheLeftNodesRightNodeShouldBeASingleValuePropertyAccessNode()
            {
                Assert.IsType<ConstantNode>(((BinaryOperatorNode)this.node.Left).Right);
            }

            [Fact]
            public void TheOperatorKindShouldBeAnd()
            {
                Assert.Equal(BinaryOperatorKind.And, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(this.node.Right);
            }

            [Fact]
            public void TheRightNodesOperatorKindShouldBeEqual()
            {
                Assert.Equal(BinaryOperatorKind.Equal, ((BinaryOperatorNode)this.node.Right).OperatorKind);
            }

            [Fact]
            public void TheRightNodesRightNodeShouldBeASingleValuePropertyAccessNode()
            {
                Assert.IsType<ConstantNode>(((BinaryOperatorNode)this.node.Right).Right);
            }
        }

        public class ParsePropertyEqValueAndPropertyEqValueOrPropertyEqValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqValueAndPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and Middlename eq 'Albert' or Surname eq 'Smith'");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheLeftNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(this.node.Left);
            }

            [Fact]
            public void TheOperatorKindShouldBeAnd()
            {
                Assert.Equal(BinaryOperatorKind.And, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(this.node.Right);
            }

            [Fact]
            public void TheRightNodesLeftNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(((BinaryOperatorNode)this.node.Right).Left);
            }

            [Fact]
            public void TheRightNodesOperatorKindShouldBeOr()
            {
                Assert.Equal(BinaryOperatorKind.Or, ((BinaryOperatorNode)this.node.Right).OperatorKind);
            }

            [Fact]
            public void TheRightNodesRightNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(((BinaryOperatorNode)this.node.Right).Right);
            }
        }

        public class ParsePropertyEqValueOrPropertyEqValueAndPropertyEqValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqValueOrPropertyEqValueAndPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' or Middlename eq 'Albert' and Surname eq 'Smith'");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheLeftNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(this.node.Left);
            }

            [Fact]
            public void TheOperatorKindShouldBeOr()
            {
                Assert.Equal(BinaryOperatorKind.Or, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(this.node.Right);
            }

            [Fact]
            public void TheRightNodesLeftNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(((BinaryOperatorNode)this.node.Right).Left);
            }

            [Fact]
            public void TheRightNodesOperatorKindShouldBeAnd()
            {
                Assert.Equal(BinaryOperatorKind.And, ((BinaryOperatorNode)this.node.Right).OperatorKind);
            }

            [Fact]
            public void TheRightNodesRightNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(((BinaryOperatorNode)this.node.Right).Right);
            }
        }

        public class ParsePropertyEqValueOrPropertyEqValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' or Surname eq 'Smith'");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheLeftNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(this.node.Left);
            }

            [Fact]
            public void TheLeftNodesLeftNodeShouldBeASingleValuePropertyAccessNode()
            {
                Assert.IsType<SingleValuePropertyAccessNode>(((BinaryOperatorNode)this.node.Left).Left);
            }

            [Fact]
            public void TheLeftNodesOperatorKindShouldBeEqual()
            {
                Assert.Equal(BinaryOperatorKind.Equal, ((BinaryOperatorNode)this.node.Left).OperatorKind);
            }

            [Fact]
            public void TheLeftNodesRightNodeShouldBeASingleValuePropertyAccessNode()
            {
                Assert.IsType<ConstantNode>(((BinaryOperatorNode)this.node.Left).Right);
            }

            [Fact]
            public void TheOperatorKindShouldBeOr()
            {
                Assert.Equal(BinaryOperatorKind.Or, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightNodeShouldBeABinaryOperatorNode()
            {
                Assert.IsType<BinaryOperatorNode>(this.node.Right);
            }

            [Fact]
            public void TheRightNodesOperatorKindShouldBeEqual()
            {
                Assert.Equal(BinaryOperatorKind.Equal, ((BinaryOperatorNode)this.node.Right).OperatorKind);
            }

            [Fact]
            public void TheRightNodesRightNodeShouldBeASingleValuePropertyAccessNode()
            {
                Assert.IsType<ConstantNode>(((BinaryOperatorNode)this.node.Right).Right);
            }
        }

        public class ParsePropertyNeValueExpression
        {
            private readonly BinaryOperatorNode node;

            public ParsePropertyNeValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename ne 'John'");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheLeftNodeShouldBeThePropertyAccessNode()
            {
                Assert.IsType<SingleValuePropertyAccessNode>(this.node.Left);
            }

            [Fact]
            public void TheLeftNodeShouldContainThePropertyName()
            {
                Assert.Equal("Forename", ((SingleValuePropertyAccessNode)this.node.Left).PropertyName);
            }

            [Fact]
            public void TheNodeReturnedShouldBeABinaryOperatorNode()
            {
                Assert.NotNull(this.node);
            }

            [Fact]
            public void TheOperatorKindShouldBeEqual()
            {
                Assert.Equal(BinaryOperatorKind.NotEqual, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightNodeShouldBeTheConstantNode()
            {
                Assert.IsType<ConstantNode>(this.node.Right);
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("John", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal("John", ((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParseStartswithFunctionExpression
        {
            private readonly BinaryOperatorNode node;

            public ParseStartswithFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("startswith(Name, 'Hay') eq true");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheLeftNodeFirstArgumentShouldBeThePropertyNode()
            {
                Assert.IsType<SingleValuePropertyAccessNode>(((SingleValueFunctionCallNode)this.node.Left).Arguments[0]);
            }

            [Fact]
            public void TheLeftNodeFirstArgumentShouldContainThePropertyName()
            {
                var queryNode = (SingleValuePropertyAccessNode)((SingleValueFunctionCallNode)this.node.Left).Arguments[0];

                Assert.Equal("Name", queryNode.PropertyName);
            }

            [Fact]
            public void TheLeftNodeSecondArgumentShouldBeTheConstantNode()
            {
                Assert.IsType<ConstantNode>(((SingleValueFunctionCallNode)this.node.Left).Arguments[1]);
            }

            [Fact]
            public void TheLeftNodeSecondArgumentShouldContainTheConstantValue()
            {
                var queryNode = (ConstantNode)((SingleValueFunctionCallNode)this.node.Left).Arguments[1];

                Assert.Equal("Hay", queryNode.Value);
            }

            [Fact]
            public void TheLeftNodeShouldBeTheSingleValueFunctionCallNode()
            {
                Assert.IsType<SingleValueFunctionCallNode>(this.node.Left);
            }

            [Fact]
            public void TheLeftNodeShouldContainTheFunctionName()
            {
                Assert.Equal("startswith", ((SingleValueFunctionCallNode)this.node.Left).Name);
            }

            [Fact]
            public void TheNodeReturnedShouldBeABinaryOperatorNode()
            {
                Assert.NotNull(this.node);
            }

            [Fact]
            public void TheOperatorKindShouldBeEqual()
            {
                Assert.Equal(BinaryOperatorKind.Equal, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightNodeShouldBeTheConstantNode()
            {
                Assert.IsType<ConstantNode>(this.node.Right);
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("true", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(true, ((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParseSubstringofFunctionExpression
        {
            private readonly BinaryOperatorNode node;

            public ParseSubstringofFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("substringof('Hayes', Name) eq true");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheLeftNodeFirstArgumentShouldBeTheConstantNode()
            {
                Assert.IsType<ConstantNode>(((SingleValueFunctionCallNode)this.node.Left).Arguments[0]);
            }

            [Fact]
            public void TheLeftNodeFirstArgumentShouldContainTheConstantValue()
            {
                var queryNode = (ConstantNode)((SingleValueFunctionCallNode)this.node.Left).Arguments[0];

                Assert.Equal("Hayes", queryNode.Value);
            }

            [Fact]
            public void TheLeftNodeSecondArgumentShouldBeThePropertyNode()
            {
                Assert.IsType<SingleValuePropertyAccessNode>(((SingleValueFunctionCallNode)this.node.Left).Arguments[1]);
            }

            [Fact]
            public void TheLeftNodeSecondArgumentShouldContainThePropertyName()
            {
                var queryNode = (SingleValuePropertyAccessNode)((SingleValueFunctionCallNode)this.node.Left).Arguments[1];

                Assert.Equal("Name", queryNode.PropertyName);
            }

            [Fact]
            public void TheLeftNodeShouldBeTheSingleValueFunctionCallNode()
            {
                Assert.IsType<SingleValueFunctionCallNode>(this.node.Left);
            }

            [Fact]
            public void TheLeftNodeShouldContainTheFunctionName()
            {
                Assert.Equal("substringof", ((SingleValueFunctionCallNode)this.node.Left).Name);
            }

            [Fact]
            public void TheNodeReturnedShouldBeABinaryOperatorNode()
            {
                Assert.NotNull(this.node);
            }

            [Fact]
            public void TheOperatorKindShouldBeEqual()
            {
                Assert.Equal(BinaryOperatorKind.Equal, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightNodeShouldBeTheConstantNode()
            {
                Assert.IsType<ConstantNode>(this.node.Right);
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("true", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal(true, ((ConstantNode)this.node.Right).Value);
            }
        }

        public class ParseToLowerFunctionExpression
        {
            private readonly BinaryOperatorNode node;

            public ParseToLowerFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("tolower(CompanyName) eq 'alfreds futterkiste'");

                this.node = queryNode as BinaryOperatorNode;
            }

            [Fact]
            public void TheLeftNodeFirstArgumentShouldBeThePropertyNode()
            {
                Assert.IsType<SingleValuePropertyAccessNode>(((SingleValueFunctionCallNode)this.node.Left).Arguments[0]);
            }

            [Fact]
            public void TheLeftNodeFirstArgumentShouldContainThePropertyName()
            {
                var queryNode = (SingleValuePropertyAccessNode)((SingleValueFunctionCallNode)this.node.Left).Arguments[0];

                Assert.Equal("CompanyName", queryNode.PropertyName);
            }

            [Fact]
            public void TheLeftNodeSecondArgumentShouldBeTheConstantNode()
            {
                Assert.IsType<ConstantNode>(((SingleValueFunctionCallNode)this.node.Left).Arguments[1]);
            }

            [Fact]
            public void TheLeftNodeSecondArgumentShouldContainTheConstantValue()
            {
                var queryNode = (ConstantNode)((SingleValueFunctionCallNode)this.node.Left).Arguments[1];

                Assert.Equal(string.Empty, queryNode.Value);
            }

            [Fact]
            public void TheLeftNodeShouldBeTheSingleValueFunctionCallNode()
            {
                Assert.IsType<SingleValueFunctionCallNode>(this.node.Left);
            }

            [Fact]
            public void TheLeftNodeShouldContainTheFunctionName()
            {
                Assert.Equal("tolower", ((SingleValueFunctionCallNode)this.node.Left).Name);
            }

            [Fact]
            public void TheNodeReturnedShouldBeABinaryOperatorNode()
            {
                Assert.NotNull(this.node);
            }

            [Fact]
            public void TheOperatorKindShouldBeEqual()
            {
                Assert.Equal(BinaryOperatorKind.Equal, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightNodeShouldBeTheConstantNode()
            {
                Assert.IsType<ConstantNode>(this.node.Right);
            }

            [Fact]
            public void TheRightNodeShouldContainTheLiteralText()
            {
                Assert.Equal("alfreds futterkiste", ((ConstantNode)this.node.Right).LiteralText);
            }

            [Fact]
            public void TheRightNodeShouldContainTheValue()
            {
                Assert.Equal("alfreds futterkiste", ((ConstantNode)this.node.Right).Value);
            }
        }
    }
}