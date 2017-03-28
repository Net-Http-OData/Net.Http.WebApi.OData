namespace Net.Http.WebApi.OData.Tests.Query.Expressions
{
    using Net.Http.WebApi.OData.Query.Expressions;
    using WebApi.OData.Model;
    using Xunit;

    public class BinaryOperatorNodeTests
    {
        public class WhenConstructed
        {
            private readonly BinaryOperatorKind binaryOperatorKind = BinaryOperatorKind.Equal;
            private readonly QueryNode left;
            private readonly BinaryOperatorNode node;
            private readonly QueryNode right = ConstantNode.String("'Alfreds Futterkiste'", "AlfredsFutterkiste");

            public WhenConstructed()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Customers"];

                this.left = new PropertyAccessNode(model.GetProperty("CompanyName"));
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