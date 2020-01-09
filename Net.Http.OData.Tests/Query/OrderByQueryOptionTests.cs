namespace Net.Http.OData.Tests.Query
{
    using Net.Http.OData.Model;
    using Net.Http.OData.Query;
    using Net.Http.OData.Tests;
    using Xunit;

    public class OrderByQueryOptionTests
    {
        public class WhenConstructedWithASingleValue
        {
            private readonly OrderByQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithASingleValue()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Products"].EdmType;

                this.rawValue = "$orderby=Name";
                this.option = new OrderByQueryOption(this.rawValue, model);
            }

            [Fact]
            public void ThePropertiesShouldContainTheCorrectItems()
            {
                Assert.Equal(1, this.option.Properties.Count);

                Assert.Equal("Name", this.option.Properties[0].PropertyPath.Property.Name);
                Assert.Equal(OrderByDirection.Ascending, this.option.Properties[0].Direction);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }

        public class WhenConstructedWithMultipleValues
        {
            private readonly OrderByQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithMultipleValues()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Products"].EdmType;

                this.rawValue = "$orderby=Category/Name,Name,Price desc,Rating asc";
                this.option = new OrderByQueryOption(this.rawValue, model);
            }

            [Fact]
            public void ThePropertiesShouldContainTheCorrectItems()
            {
                Assert.Equal(4, this.option.Properties.Count);

                Assert.Equal("Category", this.option.Properties[0].PropertyPath.Property.Name);
                Assert.Equal("Name", this.option.Properties[0].PropertyPath.Next.Property.Name);
                Assert.Equal(OrderByDirection.Ascending, this.option.Properties[0].Direction);

                Assert.Equal("Name", this.option.Properties[1].PropertyPath.Property.Name);
                Assert.Equal(OrderByDirection.Ascending, this.option.Properties[1].Direction);

                Assert.Equal("Price", this.option.Properties[2].PropertyPath.Property.Name);
                Assert.Equal(OrderByDirection.Descending, this.option.Properties[2].Direction);

                Assert.Equal("Rating", this.option.Properties[3].PropertyPath.Property.Name);
                Assert.Equal(OrderByDirection.Ascending, this.option.Properties[3].Direction);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }
    }
}