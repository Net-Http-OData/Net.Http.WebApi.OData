namespace Net.Http.WebApi.Tests.OData.Query.Expression
{
    using Net.Http.WebApi.OData.Query.Expression;
    using Xunit;

    public class BinaryOperatorNodeTests
    {
        public class WhenConstructed
        {
            private readonly BinaryOperatorKind binaryOperatorKind = BinaryOperatorKind.And;
            private readonly SingleValueNode left = new SingleValuePropertyAccessNode("Name");
            private readonly BinaryOperatorNode node;
            private readonly SingleValueNode right = new ConstantNode("Fred", "Fred");

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

        public class WhenConstructedWithDefaultConstructor
        {
            private readonly BinaryOperatorNode node;

            public WhenConstructedWithDefaultConstructor()
            {
                this.node = new BinaryOperatorNode();
            }

            [Fact]
            public void TheKindIsQueryNodeKindBinaryOperator()
            {
                Assert.Equal(QueryNodeKind.BinaryOperator, this.node.Kind);
            }

            [Fact]
            public void TheLeftPropertyIsNull()
            {
                Assert.Null(this.node.Left);
            }

            [Fact]
            public void TheOperatorKindIsBinaryOperatorKindNone()
            {
                Assert.Equal(BinaryOperatorKind.None, this.node.OperatorKind);
            }

            [Fact]
            public void TheRightPropertyIsNull()
            {
                Assert.Null(this.node.Right);
            }
        }
    }
}