using System;
using Net.Http.OData.Model;
using Xunit;

namespace Net.Http.OData.Tests.Model
{
    public class EdmCollectionTypeTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            var type = typeof(int);
            var edmType = EdmPrimitiveType.Int32;
            var edmCollectionType = new EdmCollectionType(type, edmType);

            Assert.Same(type, edmCollectionType.ClrType);
            Assert.Same(edmType, edmCollectionType.ContainedType);
            Assert.Equal($"Collection({edmType.FullName})", edmCollectionType.FullName);
            Assert.Equal("Collection", edmCollectionType.Name);
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullContainedType()
        {
            var type = typeof(int);

            Assert.Throws<ArgumentNullException>(() => new EdmCollectionType(type, null));
        }
    }
}