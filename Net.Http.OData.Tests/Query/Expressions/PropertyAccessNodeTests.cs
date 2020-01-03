namespace Net.Http.OData.Tests.Query.Expressions
{
    using Net.Http.OData.Model;
    using Net.Http.OData.Query.Expressions;
    using Net.Http.OData.Tests;
    using Xunit;

    public class PropertyAccessNodeTests
    {
        public class WhenConstructed
        {
            private readonly PropertyAccessNode node;
            private readonly EdmProperty property;

            public WhenConstructed()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Customers"].EdmType;

                this.property = model.GetProperty("CompanyName");
                this.node = new PropertyAccessNode(this.property);
            }

            [Fact]
            public void TheKindIsQueryNodeKindSingleValuePropertyAccess()
            {
                Assert.Equal(QueryNodeKind.PropertyAccess, this.node.Kind);
            }

            [Fact]
            public void ThePropertyPropertyIsSet()
            {
                Assert.Equal(this.property, this.node.Property);
            }
        }
    }
}