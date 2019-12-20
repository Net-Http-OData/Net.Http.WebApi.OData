namespace Net.Http.WebApi.OData.Tests.Model
{
    using System;
    using Net.Http.WebApi.OData.Model;
    using NorthwindModel;
    using Xunit;

    public class EdmPropertyTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            var type = typeof(Customer);
            var edmComplexType = new EdmComplexType(type, new EdmProperty[0]);

            var edmProperty = new EdmProperty(type.GetProperty("CompanyName"), EdmPrimitiveType.String, edmComplexType);

            Assert.Same(edmComplexType, edmProperty.DeclaringType);
            Assert.Equal("CompanyName", edmProperty.Name);
            Assert.Same(EdmPrimitiveType.String, edmProperty.PropertyType);
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullDeclaringType()
        {
            Assert.Throws<ArgumentNullException>(() => new EdmProperty(typeof(Customer).GetProperty("CompanyName"), EdmPrimitiveType.String, null));
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullPropertyInfo()
        {
            var type = typeof(Customer);
            var edmComplexType = new EdmComplexType(type, new EdmProperty[0]);

            Assert.Throws<ArgumentNullException>(() => new EdmProperty(null, EdmPrimitiveType.String, edmComplexType));
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullPropertyType()
        {
            var type = typeof(Customer);
            var edmComplexType = new EdmComplexType(type, new EdmProperty[0]);

            Assert.Throws<ArgumentNullException>(() => new EdmProperty(type.GetProperty("CompanyName"), null, edmComplexType));
        }

        [Fact]
        public void IsNullable_ReturnsFalse_ForClass_WithRequiredAttribute()
        {
            var type = typeof(Employee);
            EdmTypeCache.Map.TryGetValue(typeof(string), out EdmType edmType);
            var edmProperty = new EdmProperty(type.GetProperty("Forename"), edmType, new EdmComplexType(type, new EdmProperty[0]));

            Assert.False(edmProperty.IsNullable);
        }

        [Fact]
        public void IsNullable_ReturnsFalse_ForStruct()
        {
            var type = typeof(Customer);
            EdmTypeCache.Map.TryGetValue(typeof(int), out EdmType edmType);
            var edmProperty = new EdmProperty(type.GetProperty("LegacyId"), edmType, new EdmComplexType(type, new EdmProperty[0]));

            Assert.False(edmProperty.IsNullable);
        }

        [Fact]
        public void IsNullable_ReturnsTrue_ForClass()
        {
            var type = typeof(Customer);
            EdmTypeCache.Map.TryGetValue(typeof(string), out EdmType edmType);
            var edmProperty = new EdmProperty(type.GetProperty("CompanyName"), edmType, new EdmComplexType(type, new EdmProperty[0]));

            Assert.True(edmProperty.IsNullable);
        }

        [Fact]
        public void IsNullable_ReturnsTrue_ForCollection()
        {
            var type = typeof(Order);
            EdmTypeCache.Map.TryGetValue(typeof(OrderDetail), out EdmType edmType);
            var edmProperty = new EdmProperty(type.GetProperty("OrderDetails"), new EdmCollectionType(type, edmType), new EdmComplexType(typeof(Customer), new EdmProperty[0]));

            Assert.True(edmProperty.IsNullable);
        }

        [Fact]
        public void IsNullable_ReturnsTrue_ForNullableStruct()
        {
            var type = typeof(Employee);
            EdmTypeCache.Map.TryGetValue(typeof(int?), out EdmType edmType);
            var edmProperty = new EdmProperty(type.GetProperty("LeavingDate"), edmType, new EdmComplexType(type, new EdmProperty[0]));

            Assert.True(edmProperty.IsNullable);
        }

        [Fact]
        public void ToString_ReturnsName()
        {
            var type = typeof(Customer);
            var edmComplexType = new EdmComplexType(type, new EdmProperty[0]);

            var edmProperty = new EdmProperty(type.GetProperty("CompanyName"), EdmPrimitiveType.String, edmComplexType);

            Assert.Equal(edmProperty.ToString(), edmProperty.Name);
        }
    }
}