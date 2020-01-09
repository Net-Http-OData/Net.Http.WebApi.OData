using Net.Http.OData.Model;
using Net.Http.OData.Query;
using NorthwindModel;
using Xunit;

namespace Net.Http.OData.Tests.Query
{
    public class PropertyPathSegmentTests
    {
        public PropertyPathSegmentTests()
        {
            TestHelper.EnsureEDM();
        }

        [Fact]
        public void For_PropertyPath()
        {
            var propertyPathSegment = PropertyPathSegment.For("Category/Name", EntityDataModel.Current.EntitySets["Products"].EdmType);

            Assert.NotNull(propertyPathSegment.Next);
            Assert.NotNull(propertyPathSegment.Property);
            Assert.Equal("Category", propertyPathSegment.Property.Name);

            propertyPathSegment = propertyPathSegment.Next;

            Assert.Null(propertyPathSegment.Next);
            Assert.NotNull(propertyPathSegment.Property);
            Assert.Equal("Name", propertyPathSegment.Property.Name);
        }

        [Fact]
        public void For_SingleProperty()
        {
            var propertyPathSegment = PropertyPathSegment.For("Name", EntityDataModel.Current.EntitySets["Products"].EdmType);

            Assert.Null(propertyPathSegment.Next);
            Assert.NotNull(propertyPathSegment.Property);
            Assert.Equal("Name", propertyPathSegment.Property.Name);
        }

        [Fact]
        public void WhenConstructed_WithEdmProperty()
        {
            var type = typeof(Customer);
            var edmComplexType = new EdmComplexType(type, new EdmProperty[0]);

            var edmProperty = new EdmProperty(type.GetProperty("CompanyName"), EdmPrimitiveType.String, edmComplexType, (_) => true);

            var propertyPathSegment = new PropertyPathSegment(edmProperty);

            Assert.Null(propertyPathSegment.Next);
            Assert.Equal(edmProperty, propertyPathSegment.Property);
        }
    }
}