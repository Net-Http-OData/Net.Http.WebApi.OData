namespace Net.Http.WebApi.OData.Tests.Query
{
    using Net.Http.WebApi.OData.Model;
    using Net.Http.WebApi.OData.Query;
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

                var model = EntityDataModel.Current.EntitySets["Products"].EdmType;

                this.rawValue = "$expand=Category";
                this.option = new SelectExpandQueryOption(this.rawValue, model);
            }

            [Fact]
            public void ThePropertiesShouldContainSpecifiedNavigationProperty()
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

                var model = EntityDataModel.Current.EntitySets["Products"].EdmType;

                this.rawValue = "$expand=*";
                this.option = new SelectExpandQueryOption(this.rawValue, model);
            }

            [Fact]
            public void ThePropertiesShouldContainAllNavigationProperties()
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

                var model = EntityDataModel.Current.EntitySets["Products"].EdmType;

                this.rawValue = "$select=Name,Price,ReleaseDate";
                this.option = new SelectExpandQueryOption(this.rawValue, model);
            }

            [Fact]
            public void ThePropertiesShouldContainSpecifiedProperties()
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

                var model = EntityDataModel.Current.EntitySets["Products"].EdmType;

                this.rawValue = "$select=Name";
                this.option = new SelectExpandQueryOption(this.rawValue, model);
            }

            [Fact]
            public void ThePropertiesShouldContainSpecifiedProperty()
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

                this.model = EntityDataModel.Current.EntitySets["Products"].EdmType;
                this.rawValue = "$select=*";
                this.option = new SelectExpandQueryOption(this.rawValue, this.model);
            }

            [Fact]
            public void ThePropertiesShouldContainAllProperties()
            {
                Assert.Equal(8, this.option.Properties.Count);

                Assert.Equal("Colour", this.option.Properties[0].Name);
                Assert.Equal("Deleted", this.option.Properties[1].Name);
                Assert.Equal("Description", this.option.Properties[2].Name);
                Assert.Equal("Name", this.option.Properties[3].Name);
                Assert.Equal("Price", this.option.Properties[4].Name);
                Assert.Equal("ProductId", this.option.Properties[5].Name);
                Assert.Equal("Rating", this.option.Properties[6].Name);
                Assert.Equal("ReleaseDate", this.option.Properties[7].Name);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }
    }
}