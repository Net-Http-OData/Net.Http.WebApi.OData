namespace Net.Http.OData.Tests.Model
{
    using System;
    using Net.Http.OData.Model;
    using Xunit;

    public class EdmTypeCacheTests
    {
        [Fact]
        public void PrimitivesAreRegisteredByDefault()
        {
            Assert.Equal(EdmType.GetEdmType(typeof(byte[])), EdmPrimitiveType.Binary);

            Assert.Equal(EdmType.GetEdmType(typeof(bool)), EdmPrimitiveType.Boolean);
            Assert.Equal(EdmType.GetEdmType(typeof(bool?)), EdmPrimitiveType.NullableBoolean);

            Assert.Equal(EdmType.GetEdmType(typeof(byte)), EdmPrimitiveType.Byte);
            Assert.Equal(EdmType.GetEdmType(typeof(byte?)), EdmPrimitiveType.NullableByte);

            Assert.Equal(EdmType.GetEdmType(typeof(DateTime)), EdmPrimitiveType.Date);
            Assert.Equal(EdmType.GetEdmType(typeof(DateTime?)), EdmPrimitiveType.NullableDate);

            Assert.Equal(EdmType.GetEdmType(typeof(DateTimeOffset)), EdmPrimitiveType.DateTimeOffset);
            Assert.Equal(EdmType.GetEdmType(typeof(DateTimeOffset?)), EdmPrimitiveType.NullableDateTimeOffset);

            Assert.Equal(EdmType.GetEdmType(typeof(decimal)), EdmPrimitiveType.Decimal);
            Assert.Equal(EdmType.GetEdmType(typeof(decimal?)), EdmPrimitiveType.NullableDecimal);

            Assert.Equal(EdmType.GetEdmType(typeof(double)), EdmPrimitiveType.Double);
            Assert.Equal(EdmType.GetEdmType(typeof(double?)), EdmPrimitiveType.NullableDouble);

            Assert.Equal(EdmType.GetEdmType(typeof(TimeSpan)), EdmPrimitiveType.Duration);
            Assert.Equal(EdmType.GetEdmType(typeof(TimeSpan?)), EdmPrimitiveType.NullableDuration);

            Assert.Equal(EdmType.GetEdmType(typeof(Guid)), EdmPrimitiveType.Guid);
            Assert.Equal(EdmType.GetEdmType(typeof(Guid?)), EdmPrimitiveType.NullableGuid);

            Assert.Equal(EdmType.GetEdmType(typeof(short)), EdmPrimitiveType.Int16);
            Assert.Equal(EdmType.GetEdmType(typeof(short?)), EdmPrimitiveType.NullableInt16);

            Assert.Equal(EdmType.GetEdmType(typeof(int)), EdmPrimitiveType.Int32);
            Assert.Equal(EdmType.GetEdmType(typeof(int?)), EdmPrimitiveType.NullableInt32);

            Assert.Equal(EdmType.GetEdmType(typeof(long)), EdmPrimitiveType.Int64);
            Assert.Equal(EdmType.GetEdmType(typeof(long?)), EdmPrimitiveType.NullableInt64);

            Assert.Equal(EdmType.GetEdmType(typeof(sbyte)), EdmPrimitiveType.SByte);
            Assert.Equal(EdmType.GetEdmType(typeof(sbyte?)), EdmPrimitiveType.NullableSByte);

            Assert.Equal(EdmType.GetEdmType(typeof(float)), EdmPrimitiveType.Single);
            Assert.Equal(EdmType.GetEdmType(typeof(float?)), EdmPrimitiveType.NullableSingle);

            Assert.Equal(EdmType.GetEdmType(typeof(char)), EdmPrimitiveType.String);
            Assert.Equal(EdmType.GetEdmType(typeof(char?)), EdmPrimitiveType.String);
            Assert.Equal(EdmType.GetEdmType(typeof(string)), EdmPrimitiveType.String);

            ////Assert.Equal(EdmType.GetEdmType(typeof(TimeSpan)), EdmPrimitiveType.TimeOfDay);
            ////Assert.Equal(EdmType.GetEdmType(typeof(TimeSpan?)), EdmPrimitiveType.NullableTimeOfDay);
        }
    }
}