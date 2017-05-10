namespace Net.Http.WebApi.OData.Tests.Model
{
    using System;
    using NorthwindModel;
    using OData.Model;
    using Xunit;

    public class EdmPropertyTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            var type = typeof(Customer);
            var edmComplexType = new EdmComplexType(type, "Name", new EdmProperty[0]);

            var edmProperty = new EdmProperty("CompanyName", EdmPrimitiveType.String, edmComplexType);

            Assert.Same(edmComplexType, edmProperty.DeclaringType);
            Assert.Equal("CompanyName", edmProperty.Name);
            Assert.Same(EdmPrimitiveType.String, edmProperty.PropertyType);
        }

        [Fact]
        public void Constructor_ThrowsArgumentException_ForEmptyName()
        {
            var type = typeof(Customer);
            var edmComplexType = new EdmComplexType(type, "Name", new EdmProperty[0]);

            Assert.Throws<ArgumentException>(() => new EdmProperty("", EdmPrimitiveType.String, edmComplexType));
        }

        [Fact]
        public void Constructor_ThrowsArgumentException_ForNullName()
        {
            var type = typeof(Customer);
            var edmComplexType = new EdmComplexType(type, "Name", new EdmProperty[0]);

            Assert.Throws<ArgumentException>(() => new EdmProperty(null, EdmPrimitiveType.String, edmComplexType));
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullDeclaringType()
        {
            Assert.Throws<ArgumentNullException>(() => new EdmProperty("CompanyName", EdmPrimitiveType.String, null));
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullPropertyType()
        {
            var type = typeof(Customer);
            var edmComplexType = new EdmComplexType(type, "Name", new EdmProperty[0]);

            Assert.Throws<ArgumentNullException>(() => new EdmProperty("CompanyName", null, edmComplexType));
        }

        [Fact]
        public void ToString_ReturnsName()
        {
            var type = typeof(Customer);
            var edmComplexType = new EdmComplexType(type, "Name", new EdmProperty[0]);

            var edmProperty = new EdmProperty("CompanyName", EdmPrimitiveType.String, edmComplexType);

            Assert.Equal(edmProperty.ToString(), edmProperty.Name);
        }
    }
}