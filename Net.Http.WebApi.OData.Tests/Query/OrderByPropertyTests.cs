namespace Net.Http.WebApi.Tests.OData.Query
{
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Xunit;

    public class OrderByPropertyTests
    {
        public class WhenConstructedWithAnIncorrectlyCasedValue
        {
            [Fact]
            public void AnODataExceptionShouldBeThrown()
            {
                var exception = Assert.Throws<ODataException>(() => new OrderByProperty("Name ASC"));
                Assert.Equal(Messages.OrderByPropertyRawValueInvalid, exception.Message);
            }
        }

        public class WhenConstructedWithAnInvalidValue
        {
            [Fact]
            public void AnODataExceptionShouldBeThrown()
            {
                var exception = Assert.Throws<ODataException>(() => new OrderByProperty("Name wibble"));
                Assert.Equal(Messages.OrderByPropertyRawValueInvalid, exception.Message);
            }
        }

        public class WhenConstructedWithAsc
        {
            private readonly OrderByProperty property;
            private readonly string rawValue;

            public WhenConstructedWithAsc()
            {
                this.rawValue = "Name asc";
                this.property = new OrderByProperty(this.rawValue);
            }

            [Fact]
            public void TheDirectionShouldBeSetToAscending()
            {
                Assert.Equal(OrderByDirection.Ascending, this.property.Direction);
            }

            [Fact]
            public void ThePropertyNameShouldBeSetToTheNameOfThePropertyPassedToTheConstructor()
            {
                Assert.Equal("Name", this.property.Name);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.property.RawValue);
            }
        }

        public class WhenConstructedWithDesc
        {
            private readonly OrderByProperty property;
            private readonly string rawValue;

            public WhenConstructedWithDesc()
            {
                this.rawValue = "Name desc";
                this.property = new OrderByProperty(this.rawValue);
            }

            [Fact]
            public void TheDirectionShouldBeSetToDescending()
            {
                Assert.Equal(OrderByDirection.Descending, this.property.Direction);
            }

            [Fact]
            public void ThePropertyNameShouldBeSetToTheNameOfThePropertyPassedToTheConstructor()
            {
                Assert.Equal("Name", this.property.Name);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.property.RawValue);
            }
        }

        public class WhenConstructedWithoutADirection
        {
            private readonly OrderByProperty property;
            private readonly string rawValue;

            public WhenConstructedWithoutADirection()
            {
                this.rawValue = "Name";
                this.property = new OrderByProperty(this.rawValue);
            }

            [Fact]
            public void TheDirectionShouldDefaultToAscending()
            {
                Assert.Equal(OrderByDirection.Ascending, this.property.Direction);
            }

            [Fact]
            public void ThePropertyNameShouldBeSetToTheNameOfThePropertyPassedToTheConstructor()
            {
                Assert.Equal("Name", this.property.Name);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.property.RawValue);
            }
        }
    }
}