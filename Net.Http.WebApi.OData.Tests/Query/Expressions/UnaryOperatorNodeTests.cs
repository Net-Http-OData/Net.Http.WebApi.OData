namespace Net.Http.WebApi.OData.Tests.Query.Expressions
{
    using Net.Http.WebApi.OData.Query.Expressions;
    using WebApi.OData.Model;
    using Xunit;

    public class UnaryOperatorNodeTests
    {
        public class WhenConstructed
        {
            private readonly UnaryOperatorNode node;
            private readonly QueryNode operand;
            private readonly UnaryOperatorKind unaryOperatorKind = UnaryOperatorKind.Not;

            public WhenConstructed()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Customers"].EdmType;

                this.operand = new PropertyAccessNode(model.GetProperty("CompanyName"));
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