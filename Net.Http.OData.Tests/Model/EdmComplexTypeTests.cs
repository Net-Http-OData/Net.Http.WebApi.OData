﻿namespace Net.Http.OData.Tests.Model
{
    using System;
    using System.Net;
    using Net.Http.OData.Model;
    using NorthwindModel;
    using Xunit;

    public class EdmComplexTypeTests
    {
        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullProperties()
        {
            var type = typeof(Customer);

            Assert.Throws<ArgumentNullException>(() => new EdmComplexType(type, null));
        }

        [Fact]
        public void Constructor_Type_Properties_SetsProperties()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType = new EdmComplexType(type, properties);

            Assert.Null(edmComplexType.BaseType);
            Assert.Same(type, edmComplexType.ClrType);
            Assert.Equal(type.FullName, edmComplexType.FullName);
            Assert.Equal(type.Name, edmComplexType.Name);
            Assert.Same(properties, edmComplexType.Properties);
        }

        [Fact]
        public void Constructor_Type_Type_Properties_SetsProperties()
        {
            var type = typeof(Manager);
            var baseType = new EdmComplexType(typeof(Employee), new EdmProperty[0]);
            var properties = new EdmProperty[0];

            var edmComplexType = new EdmComplexType(type, baseType, properties);

            Assert.Same(baseType, edmComplexType.BaseType);
            Assert.Same(type, edmComplexType.ClrType);
            Assert.Equal(type.FullName, edmComplexType.FullName);
            Assert.Equal(type.Name, edmComplexType.Name);
            Assert.Same(properties, edmComplexType.Properties);
        }

        [Fact]
        public void Equality_False_IfOtherNotEdmType()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType1 = new EdmComplexType(type, properties);

            Assert.False(edmComplexType1.Equals("Customer"));
        }

        [Fact]
        public void Equality_False_IfOtherNull()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType1 = new EdmComplexType(type, properties);

            Assert.False(edmComplexType1.Equals(null));
        }

        [Fact]
        public void Equality_True_IfReferenceIsSame()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType1 = new EdmComplexType(type, properties);
            var edmComplexType2 = edmComplexType1;

            Assert.True(edmComplexType1.Equals(edmComplexType2));
        }

        [Fact]
        public void Equality_True_IfTypeAreSame()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType1 = new EdmComplexType(type, properties);
            var edmComplexType2 = new EdmComplexType(type, properties);

            Assert.True(edmComplexType1.Equals(edmComplexType2));
        }

        [Fact]
        public void GetProperty_ReturnsProperty()
        {
            TestHelper.EnsureEDM();

            var edmComplexType = EntityDataModel.Current.EntitySets["Customers"].EdmType;

            var edmProperty = edmComplexType.GetProperty("CompanyName");

            Assert.NotNull(edmProperty);
            Assert.Equal("CompanyName", edmProperty.Name);
        }

        [Fact]
        public void GetProperty_ThrowsODataExceptionIfPropertyNameNotFound()
        {
            TestHelper.EnsureEDM();

            var edmComplexType = EntityDataModel.Current.EntitySets["Customers"].EdmType;

            var exception = Assert.Throws<ODataException>(() => edmComplexType.GetProperty("Name"));

            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
            Assert.Equal("The type 'NorthwindModel.Customer' does not contain a property named 'Name'", exception.Message);
        }

        [Fact]
        public void ToString_ReturnsFullName()
        {
            var type = typeof(Customer);
            var properties = new EdmProperty[0];

            var edmComplexType = new EdmComplexType(type, properties);

            Assert.Equal(edmComplexType.ToString(), edmComplexType.FullName);
        }
    }
}