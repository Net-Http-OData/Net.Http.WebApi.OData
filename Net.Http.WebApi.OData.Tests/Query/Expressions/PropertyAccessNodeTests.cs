namespace Net.Http.WebApi.Tests.OData.Query.Expressions
{
    using Net.Http.WebApi.OData.Query.Expressions;
    using Xunit;

    public class PropertyAccessNodeTests
    {
        public class WhenConstructed
        {
            private readonly PropertyAccessNode node;
            private readonly string propertyName = "Name";

            public WhenConstructed()
            {
                this.node = new PropertyAccessNode(this.propertyName);
            }

            [Fact]
            public void TheKindIsQueryNodeKindSingleValuePropertyAccess()
            {
                Assert.Equal(QueryNodeKind.PropertyAccess, this.node.Kind);
            }

            [Fact]
            public void ThePropertyNamePropertyIsSet()
            {
                Assert.Equal(this.propertyName, this.node.PropertyName);
            }
        }
    }
}