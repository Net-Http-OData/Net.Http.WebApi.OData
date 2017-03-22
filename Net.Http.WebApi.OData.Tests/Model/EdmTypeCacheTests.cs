namespace Net.Http.WebApi.OData.Tests.Model
{
    using System;
    using OData.Model;
    using Xunit;

    public class EdmTypeCacheTests
    {
        [Fact]
        public void PrimitivesAreRegisteredByDefault()
        {
            Assert.Equal(EdmTypeCache.Map[typeof(byte[])], EdmPrimitiveType.Binary);

            Assert.Equal(EdmTypeCache.Map[typeof(bool)], EdmPrimitiveType.Boolean);
            Assert.Equal(EdmTypeCache.Map[typeof(bool?)], EdmPrimitiveType.Boolean);

            Assert.Equal(EdmTypeCache.Map[typeof(byte)], EdmPrimitiveType.Byte);
            Assert.Equal(EdmTypeCache.Map[typeof(byte?)], EdmPrimitiveType.Byte);

            Assert.Equal(EdmTypeCache.Map[typeof(DateTime)], EdmPrimitiveType.DateTime);
            Assert.Equal(EdmTypeCache.Map[typeof(DateTime?)], EdmPrimitiveType.DateTime);

            Assert.Equal(EdmTypeCache.Map[typeof(DateTimeOffset)], EdmPrimitiveType.DateTimeOffset);
            Assert.Equal(EdmTypeCache.Map[typeof(DateTimeOffset?)], EdmPrimitiveType.DateTimeOffset);

            Assert.Equal(EdmTypeCache.Map[typeof(decimal)], EdmPrimitiveType.Decimal);
            Assert.Equal(EdmTypeCache.Map[typeof(decimal?)], EdmPrimitiveType.Decimal);

            Assert.Equal(EdmTypeCache.Map[typeof(double)], EdmPrimitiveType.Double);
            Assert.Equal(EdmTypeCache.Map[typeof(double?)], EdmPrimitiveType.Double);

            Assert.Equal(EdmTypeCache.Map[typeof(Guid)], EdmPrimitiveType.Guid);
            Assert.Equal(EdmTypeCache.Map[typeof(Guid?)], EdmPrimitiveType.Guid);

            Assert.Equal(EdmTypeCache.Map[typeof(short)], EdmPrimitiveType.Int16);
            Assert.Equal(EdmTypeCache.Map[typeof(short?)], EdmPrimitiveType.Int16);

            Assert.Equal(EdmTypeCache.Map[typeof(int)], EdmPrimitiveType.Int32);
            Assert.Equal(EdmTypeCache.Map[typeof(int?)], EdmPrimitiveType.Int32);

            Assert.Equal(EdmTypeCache.Map[typeof(long)], EdmPrimitiveType.Int64);
            Assert.Equal(EdmTypeCache.Map[typeof(long?)], EdmPrimitiveType.Int64);

            Assert.Equal(EdmTypeCache.Map[typeof(sbyte)], EdmPrimitiveType.SByte);
            Assert.Equal(EdmTypeCache.Map[typeof(sbyte?)], EdmPrimitiveType.SByte);

            Assert.Equal(EdmTypeCache.Map[typeof(float)], EdmPrimitiveType.Single);
            Assert.Equal(EdmTypeCache.Map[typeof(float?)], EdmPrimitiveType.Single);

            Assert.Equal(EdmTypeCache.Map[typeof(char)], EdmPrimitiveType.String);
            Assert.Equal(EdmTypeCache.Map[typeof(char?)], EdmPrimitiveType.String);
            Assert.Equal(EdmTypeCache.Map[typeof(string)], EdmPrimitiveType.String);

            Assert.Equal(EdmTypeCache.Map[typeof(TimeSpan)], EdmPrimitiveType.Time);
            Assert.Equal(EdmTypeCache.Map[typeof(TimeSpan?)], EdmPrimitiveType.Time);
        }
    }
}