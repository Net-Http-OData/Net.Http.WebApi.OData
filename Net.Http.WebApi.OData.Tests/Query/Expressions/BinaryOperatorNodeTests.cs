namespace Net.Http.WebApi.Tests.OData.Query.Expressions
{
    using Net.Http.WebApi.OData.Query.Expressions;
    using Xunit;

    public class BinaryOperatorNodeTests
    {
        public class WhenConstructed
        {
            private readonly BinaryOperatorKind binaryOperatorKind = BinaryOperatorKind.And;
            private readonly SingleValueNode left = new SingleValuePropertyAccessNode("Name");
            private readonly BinaryOperatorNode node;
            private readonly SingleValueNode right = ConstantNode.String("Fred", "Fred");

            public WhenConstructed()
            {
                this.node = new BinaryOperatorNode(this.left, binaryOperatorKind, this.right);
            }

            [Fact]
            public void TheKindIsQueryNodeKindBinaryOperator()
            {
                Assert.Equal(QueryNodeKind.BinaryOperator, this.node.Kind);
            }

            [Fact]
            public void TheLeftPropertyIsSet()
            {
                Assert.Equal(this.left, this.node.Left);
            }

            [Fact]
            public void TheOperatorKindIsSet()
            {
                Assert.Equal(this.binaryOperatorKind, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightPropertyIsSet()
            {
                Assert.Equal(this.right, this.node.Right);
            }
        }
    }
}