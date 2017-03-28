namespace Net.Http.WebApi.OData.Tests.Query
{
    using Net.Http.WebApi.OData.Query;
    using WebApi.OData.Model;
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

                var model = EntityDataModel.Current.EntitySets["Products"];

                this.rawValue = "$orderby=Name";
                this.option = new OrderByQueryOption(this.rawValue, model);
            }

            [Fact]
            public void ThePropertiesShouldContainTheCorrectItems()
            {
                Assert.Equal(1, this.option.Properties.Count);

                Assert.Equal("Name", this.option.Properties[0].Property.Name);
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

                var model = EntityDataModel.Current.EntitySets["Products"];

                this.rawValue = "$orderby=Name,Price desc,Rating asc";
                this.option = new OrderByQueryOption(this.rawValue, model);
            }

            [Fact]
            public void ThePropertiesShouldContainTheCorrectItems()
            {
                Assert.Equal(3, this.option.Properties.Count);

                Assert.Equal("Name", this.option.Properties[0].Property.Name);
                Assert.Equal(OrderByDirection.Ascending, this.option.Properties[0].Direction);

                Assert.Equal("Price", this.option.Properties[1].Property.Name);
                Assert.Equal(OrderByDirection.Descending, this.option.Properties[1].Direction);

                Assert.Equal("Rating", this.option.Properties[2].Property.Name);
                Assert.Equal(OrderByDirection.Ascending, this.option.Properties[2].Direction);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }
    }
}