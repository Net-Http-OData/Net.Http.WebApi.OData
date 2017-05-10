namespace Net.Http.WebApi.OData.Tests.Model
{
    using System;
    using NorthwindModel;
    using OData.Model;
    using Xunit;

    public class EdmComplexTypeTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType = new EdmComplexType(type, "Name", properties);

            Assert.Same(type, edmComplexType.ClrType);
            Assert.Equal(type.FullName, edmComplexType.FullName);
            Assert.Equal(type.Name, edmComplexType.Name);
            Assert.Same(properties, properties);
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullProperties()
        {
            var type = typeof(Customer);

            Assert.Throws<ArgumentNullException>(() => new EdmComplexType(type, "Name", null));
        }

        [Fact]
        public void Equality_False_IfOtherNotEdmType()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType1 = new EdmComplexType(type, "Name", properties);

            Assert.False(edmComplexType1.Equals("Customer"));
        }

        [Fact]
        public void Equality_False_IfOtherNull()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType1 = new EdmComplexType(type, "Name", properties);

            Assert.False(edmComplexType1.Equals(null));
        }

        [Fact]
        public void Equality_True_IfReferenceIsSame()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType1 = new EdmComplexType(type, "Name", properties);
            var edmComplexType2 = edmComplexType1;

            Assert.True(edmComplexType1.Equals(edmComplexType2));
        }

        [Fact]
        public void Equality_True_IfTypeAreSame()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType1 = new EdmComplexType(type, "Name", properties);
            var edmComplexType2 = new EdmComplexType(type, "Name", properties);

            Assert.True(edmComplexType1.Equals(edmComplexType2));
        }

        [Fact]
        public void GetProperty_ReturnsProperty()
        {
            TestHelper.EnsureEDM();

            var edmComplexType = EntityDataModel.Current.EntitySets["Customers"];

            var edmProperty = edmComplexType.GetProperty("CompanyName");

            Assert.NotNull(edmProperty);
            Assert.Equal("CompanyName", edmProperty.Name);
        }

        [Fact]
        public void GetProperty_ThrowsArgumentExceptionIfPropertyNameNotFound()
        {
            TestHelper.EnsureEDM();

            var edmComplexType = EntityDataModel.Current.EntitySets["Customers"];

            var exception = Assert.Throws<ArgumentException>(() => edmComplexType.GetProperty("Name"));

            Assert.Equal("The type 'NorthwindModel.Customer' does not contain a property named 'Name'", exception.Message);
        }

        [Fact]
        public void ToString_ReturnsFullName()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType = new EdmComplexType(type, "Name", properties);

            Assert.Equal(edmComplexType.ToString(), edmComplexType.FullName);
        }
    }
}