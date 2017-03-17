namespace Net.Http.WebApi.OData.Tests.Query
{
    using Net.Http.WebApi.OData.Query;
    using WebApi.OData.Model;
    using Xunit;

    public class SelectExpandQueryOptionTests
    {
        public class WhenConstructedWithAValidValue
        {
            private readonly SelectExpandQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithAValidValue()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.Collections["Products"];

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
    }
}