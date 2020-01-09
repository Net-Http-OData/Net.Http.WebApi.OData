namespace Net.Http.OData.Tests.Query.Expressions
{
    using Net.Http.OData.Model;
    using Net.Http.OData.Query;
    using Net.Http.OData.Query.Expressions;
    using Net.Http.OData.Tests;
    using Xunit;

    public class PropertyAccessNodeTests
    {
        public class WhenConstructed
        {
            private readonly PropertyAccessNode node;
            private readonly PropertyPathSegment propertyPathSegment;

            public WhenConstructed()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Customers"].EdmType;

                this.propertyPathSegment = new PropertyPathSegment(model.GetProperty("CompanyName"));
                this.node = new PropertyAccessNode(this.propertyPathSegment);
            }

            [Fact]
            public void TheKindIsQueryNodeKindPropertyAccess()
            {
                Assert.Equal(QueryNodeKind.PropertyAccess, this.node.Kind);
            }

            [Fact]
            public void ThePropertyPathIsSet()
            {
                Assert.Equal(this.propertyPathSegment, this.node.PropertyPath);
            }
        }
    }
}