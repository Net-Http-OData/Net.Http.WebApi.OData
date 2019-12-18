namespace Net.Http.WebApi.OData.Tests.Model
{
    using System;
    using Net.Http.WebApi.OData.Model;
    using Xunit;

    public class EdmTypeCacheTests
    {
        [Fact]
        public void PrimitivesAreRegisteredByDefault()
        {
            Assert.Equal(EdmType.GetEdmType(typeof(byte[])), EdmPrimitiveType.Binary);

            Assert.Equal(EdmType.GetEdmType(typeof(bool)), EdmPrimitiveType.Boolean);
            Assert.Equal(EdmType.GetEdmType(typeof(bool?)), EdmPrimitiveType.Boolean);

            Assert.Equal(EdmType.GetEdmType(typeof(byte)), EdmPrimitiveType.Byte);
            Assert.Equal(EdmType.GetEdmType(typeof(byte?)), EdmPrimitiveType.Byte);

            Assert.Equal(EdmType.GetEdmType(typeof(DateTime)), EdmPrimitiveType.Date);
            Assert.Equal(EdmType.GetEdmType(typeof(DateTime?)), EdmPrimitiveType.Date);

            Assert.Equal(EdmType.GetEdmType(typeof(DateTimeOffset)), EdmPrimitiveType.DateTimeOffset);
            Assert.Equal(EdmType.GetEdmType(typeof(DateTimeOffset?)), EdmPrimitiveType.DateTimeOffset);

            Assert.Equal(EdmType.GetEdmType(typeof(decimal)), EdmPrimitiveType.Decimal);
            Assert.Equal(EdmType.GetEdmType(typeof(decimal?)), EdmPrimitiveType.Decimal);

            Assert.Equal(EdmType.GetEdmType(typeof(double)), EdmPrimitiveType.Double);
            Assert.Equal(EdmType.GetEdmType(typeof(double?)), EdmPrimitiveType.Double);

            Assert.Equal(EdmType.GetEdmType(typeof(TimeSpan)), EdmPrimitiveType.Duration);
            Assert.Equal(EdmType.GetEdmType(typeof(TimeSpan?)), EdmPrimitiveType.Duration);

            Assert.Equal(EdmType.GetEdmType(typeof(Guid)), EdmPrimitiveType.Guid);
            Assert.Equal(EdmType.GetEdmType(typeof(Guid?)), EdmPrimitiveType.Guid);

            Assert.Equal(EdmType.GetEdmType(typeof(short)), EdmPrimitiveType.Int16);
            Assert.Equal(EdmType.GetEdmType(typeof(short?)), EdmPrimitiveType.Int16);

            Assert.Equal(EdmType.GetEdmType(typeof(int)), EdmPrimitiveType.Int32);
            Assert.Equal(EdmType.GetEdmType(typeof(int?)), EdmPrimitiveType.Int32);

            Assert.Equal(EdmType.GetEdmType(typeof(long)), EdmPrimitiveType.Int64);
            Assert.Equal(EdmType.GetEdmType(typeof(long?)), EdmPrimitiveType.Int64);

            Assert.Equal(EdmType.GetEdmType(typeof(sbyte)), EdmPrimitiveType.SByte);
            Assert.Equal(EdmType.GetEdmType(typeof(sbyte?)), EdmPrimitiveType.SByte);

            Assert.Equal(EdmType.GetEdmType(typeof(float)), EdmPrimitiveType.Single);
            Assert.Equal(EdmType.GetEdmType(typeof(float?)), EdmPrimitiveType.Single);

            Assert.Equal(EdmType.GetEdmType(typeof(char)), EdmPrimitiveType.String);
            Assert.Equal(EdmType.GetEdmType(typeof(char?)), EdmPrimitiveType.String);
            Assert.Equal(EdmType.GetEdmType(typeof(string)), EdmPrimitiveType.String);

            ////Assert.Equal(EdmType.GetEdmType(typeof(TimeSpan)), EdmPrimitiveType.TimeOfDay);
            ////Assert.Equal(EdmType.GetEdmType(typeof(TimeSpan?)), EdmPrimitiveType.TimeOfDay);
        }
    }
}