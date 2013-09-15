namespace Net.Http.WebApi.Tests.OData.Query.Expression
{
    using Net.Http.WebApi.OData.Query.Expression;
    using Xunit;

    public class ConstantNodeTests
    {
        public class WhenConstructed
        {
            private readonly string literal = "2";
            private readonly ConstantNode node;
            private readonly object value = 2;

            public WhenConstructed()
            {
                this.node = new ConstantNode(this.literal, this.value);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal(this.literal, this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.Equal(this.value, this.node.Value);
            }
        }
    }
}