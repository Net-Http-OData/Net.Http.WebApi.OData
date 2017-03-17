namespace Net.Http.WebApi.OData.Tests.Model
{
    using System;
    using OData.Model;
    using Xunit;

    public class EdmTypeCacheTests
    {
        [Fact]
        public void FromClrType_ThrowsArgumentException_IfTypeNotRegistered()
        {
            var exception = Assert.Throws<ArgumentException>(() => EdmTypeCache.FromClrType(typeof(ICloneable)));

            Assert.Equal("There is no matching EdmType for the CLR type 'System.ICloneable'", exception.Message);
        }

        [Fact]
        public void PrimitivesAreRegisteredByDefault()
        {
            Assert.Equal(EdmTypeCache.FromClrType(typeof(byte[])), EdmPrimitiveType.Binary);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(bool)), EdmPrimitiveType.Boolean);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(bool?)), EdmPrimitiveType.Boolean);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(byte)), EdmPrimitiveType.Byte);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(byte?)), EdmPrimitiveType.Byte);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(DateTime)), EdmPrimitiveType.DateTime);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(DateTime?)), EdmPrimitiveType.DateTime);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(DateTimeOffset)), EdmPrimitiveType.DateTimeOffset);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(DateTimeOffset?)), EdmPrimitiveType.DateTimeOffset);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(decimal)), EdmPrimitiveType.Decimal);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(decimal?)), EdmPrimitiveType.Decimal);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(double)), EdmPrimitiveType.Double);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(double?)), EdmPrimitiveType.Double);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(Guid)), EdmPrimitiveType.Guid);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(Guid?)), EdmPrimitiveType.Guid);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(short)), EdmPrimitiveType.Int16);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(short?)), EdmPrimitiveType.Int16);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(int)), EdmPrimitiveType.Int32);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(int?)), EdmPrimitiveType.Int32);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(long)), EdmPrimitiveType.Int64);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(long?)), EdmPrimitiveType.Int64);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(sbyte)), EdmPrimitiveType.SByte);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(sbyte?)), EdmPrimitiveType.SByte);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(float)), EdmPrimitiveType.Single);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(float?)), EdmPrimitiveType.Single);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(char)), EdmPrimitiveType.String);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(char?)), EdmPrimitiveType.String);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(string)), EdmPrimitiveType.String);

            Assert.Equal(EdmTypeCache.FromClrType(typeof(TimeSpan)), EdmPrimitiveType.Time);
            Assert.Equal(EdmTypeCache.FromClrType(typeof(TimeSpan?)), EdmPrimitiveType.Time);
        }
    }
}