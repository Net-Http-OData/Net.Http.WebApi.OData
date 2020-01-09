namespace Net.Http.OData.Tests.Query.Expressions
{
    using Net.Http.OData.Model;
    using Net.Http.OData.Query;
    using Net.Http.OData.Query.Expressions;
    using Net.Http.OData.Tests;
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

                this.operand = new PropertyAccessNode(new PropertyPathSegment(model.GetProperty("CompanyName")));
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