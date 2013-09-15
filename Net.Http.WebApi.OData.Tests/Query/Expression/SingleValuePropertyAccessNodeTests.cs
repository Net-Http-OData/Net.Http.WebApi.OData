namespace Net.Http.WebApi.Tests.OData.Query.Expression
{
    using Net.Http.WebApi.OData.Query.Expression;
    using Xunit;

    public class SingleValuePropertyAccessNodeTests
    {
        public class WhenConstructed
        {
            private readonly SingleValuePropertyAccessNode node;
            private readonly string propertyName = "Name";

            public WhenConstructed()
            {
                this.node = new SingleValuePropertyAccessNode(this.propertyName);
            }

            [Fact]
            public void TheKindIsQueryNodeKindSingleValuePropertyAccess()
            {
                Assert.Equal(QueryNodeKind.SingleValuePropertyAccess, this.node.Kind);
            }

            [Fact]
            public void ThePropertyNamePropertyIsSet()
            {
                Assert.Equal(this.propertyName, this.node.PropertyName);
            }
        }
    }
}