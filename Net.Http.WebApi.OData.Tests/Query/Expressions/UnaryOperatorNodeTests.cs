namespace Net.Http.WebApi.Tests.OData.Query.Expressions
{
    using Net.Http.WebApi.OData.Query.Expressions;
    using Xunit;

    public class UnaryOperatorNodeTests
    {
        public class WhenConstructed
        {
            private readonly UnaryOperatorNode node;
            private readonly SingleValueNode operand = new SingleValuePropertyAccessNode("Name");
            private readonly UnaryOperatorKind unaryOperatorKind = UnaryOperatorKind.Not;

            public WhenConstructed()
            {
                this.node = new UnaryOperatorNode(this.operand, unaryOperatorKind);
            }

            [Fact]
            public void TheKindIsQueryNodeKindUnaryOperator()
            {
                Assert.Equal(QueryNodeKind.UnaryOperator, this.node.Kind);
            }

            [Fact]
            public void TheOperandPropertyIsSet()
            {
                Assert.Equal(this.operand, this.node.Operand);
            }

            [Fact]
            public void TheOperatorKindIsSet()
            {
                Assert.Equal(this.unaryOperatorKind, this.node.OperatorKind);
            }
        }
    }
}