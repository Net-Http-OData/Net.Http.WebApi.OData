namespace Net.Http.WebApi.OData.Tests.Query
{
    using Net.Http.WebApi.OData.Query;
    using WebApi.OData.Model;
    using Xunit;

    public class SelectExpandQueryOptionTests
    {
        public class WhenConstructedWithExpandSingleValue
        {
            private readonly SelectExpandQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithExpandSingleValue()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Products"];

                this.rawValue = "$expand=Category";
                this.option = new SelectExpandQueryOption(this.rawValue, model);
            }

            [Fact]
            public void ThePropertiesShouldContainEachSpecifiedValue()
            {
                Assert.Equal(1, this.option.Properties.Count);

                Assert.Equal("Category", this.option.Properties[0].Name);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }

        public class WhenConstructedWithExpandStarValue
        {
            private readonly SelectExpandQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithExpandStarValue()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Products"];

                this.rawValue = "$expand=*";
                this.option = new SelectExpandQueryOption(this.rawValue, model);
            }

            [Fact]
            public void ThePropertiesShouldContainEachExpandableProperty()
            {
                Assert.Equal(1, this.option.Properties.Count);

                Assert.Equal("Category", this.option.Properties[0].Name);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }

        public class WhenConstructedWithSelectMultipleValues
        {
            private readonly SelectExpandQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithSelectMultipleValues()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Products"];

                this.rawValue = "$select=Name,Price,ReleaseDate";
                this.option = new SelectExpandQueryOption(this.rawValue, model);
            }

            [Fact]
            public void ThePropertiesShouldContainEachSpecifiedValue()
            {
                Assert.Equal(3, this.option.Properties.Count);

                Assert.Equal("Name", this.option.Properties[0].Name);
                Assert.Equal("Price", this.option.Properties[1].Name);
                Assert.Equal("ReleaseDate", this.option.Properties[2].Name);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }

        public class WhenConstructedWithSelectSingleValue
        {
            private readonly SelectExpandQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithSelectSingleValue()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Products"];

                this.rawValue = "$select=Name";
                this.option = new SelectExpandQueryOption(this.rawValue, model);
            }

            [Fact]
            public void ThePropertiesShouldContainEachSpecifiedValue()
            {
                Assert.Equal(1, this.option.Properties.Count);

                Assert.Equal("Name", this.option.Properties[0].Name);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }

        public class WhenConstructedWithSelectStarValue
        {
            private readonly EdmComplexType model;
            private readonly SelectExpandQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithSelectStarValue()
            {
                TestHelper.EnsureEDM();

                this.model = EntityDataModel.Current.EntitySets["Products"];
                this.rawValue = "$select=*";
                this.option = new SelectExpandQueryOption(this.rawValue, this.model);
            }

            [Fact]
            public void ThePropertiesShouldContainEachProperty()
            {
                Assert.Same(this.option.Properties, this.model.Properties);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }
    }
}