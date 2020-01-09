namespace Net.Http.OData.Tests.Query
{
    using System;
    using System.Net;
    using Net.Http.OData;
    using Net.Http.OData.Model;
    using Net.Http.OData.Query;
    using Net.Http.OData.Tests;
    using Xunit;

    public class OrderByPropertyTests
    {
        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullModel()
        {
            TestHelper.EnsureEDM();

            Assert.Throws<ArgumentNullException>(
                () => new OrderByProperty("CompanyName", null));
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullRawValue()
        {
            TestHelper.EnsureEDM();

            Assert.Throws<ArgumentNullException>(
                () => new OrderByProperty(null, EntityDataModel.Current.EntitySets["Customers"].EdmType));
        }

        public class WhenConstructedWithAnIncorrectlyCasedValue
        {
            [Fact]
            public void AnArgumentOutOfRangeExceptionShouldBeThrown()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Customers"].EdmType;

                var exception = Assert.Throws<ODataException>(() => new OrderByProperty("CompanyName ASC", model));

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("The supplied order value for CompanyName is invalid, valid options are 'asc' and 'desc'", exception.Message);
            }
        }

        public class WhenConstructedWithAnInvalidValue
        {
            [Fact]
            public void AnArgumentOutOfRangeExceptionShouldBeThrown()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Customers"].EdmType;

                var exception = Assert.Throws<ODataException>(() => new OrderByProperty("CompanyName wibble", model));

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("The supplied order value for CompanyName is invalid, valid options are 'asc' and 'desc'", exception.Message);
            }
        }

        public class WhenConstructedWithAsc
        {
            private readonly OrderByProperty property;
            private readonly string rawValue;

            public WhenConstructedWithAsc()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Customers"].EdmType;

                this.rawValue = "CompanyName asc";
                this.property = new OrderByProperty(this.rawValue, model);
            }

            [Fact]
            public void TheDirectionShouldBeSetToAscending()
            {
                Assert.Equal(OrderByDirection.Ascending, this.property.Direction);
            }

            [Fact]
            public void ThePropertyNameShouldBeSetToTheNameOfThePropertyPassedToTheConstructor()
            {
                Assert.Equal("CompanyName", this.property.PropertyPath.Property.Name);
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
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Customers"].EdmType;

                this.rawValue = "CompanyName desc";
                this.property = new OrderByProperty(this.rawValue, model);
            }

            [Fact]
            public void TheDirectionShouldBeSetToDescending()
            {
                Assert.Equal(OrderByDirection.Descending, this.property.Direction);
            }

            [Fact]
            public void ThePropertyNameShouldBeSetToTheNameOfThePropertyPassedToTheConstructor()
            {
                Assert.Equal("CompanyName", this.property.PropertyPath.Property.Name);
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
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Customers"].EdmType;

                this.rawValue = "CompanyName";
                this.property = new OrderByProperty(this.rawValue, model);
            }

            [Fact]
            public void TheDirectionShouldDefaultToAscending()
            {
                Assert.Equal(OrderByDirection.Ascending, this.property.Direction);
            }

            [Fact]
            public void ThePropertyNameShouldBeSetToTheNameOfThePropertyPassedToTheConstructor()
            {
                Assert.Equal("CompanyName", this.property.PropertyPath.Property.Name);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.property.RawValue);
            }
        }
    }
}