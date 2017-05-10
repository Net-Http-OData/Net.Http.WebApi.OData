namespace Net.Http.WebApi.OData.Tests.Model
{
    using System;
    using OData.Model;
    using Xunit;

    public class EdmPrimitiveTypeTests
    {
        [Fact]
        public void Binary()
        {
            Assert.Equal(typeof(byte[]), EdmPrimitiveType.Binary.ClrType);
            Assert.Equal("Edm.Binary", EdmPrimitiveType.Binary.FullName);
            Assert.Same(EdmPrimitiveType.Binary, EdmPrimitiveType.Binary);
            Assert.Equal(EdmPrimitiveType.Binary.ToString(), EdmPrimitiveType.Binary.FullName);
        }

        [Fact]
        public void Boolean()
        {
            Assert.Equal(typeof(bool), EdmPrimitiveType.Boolean.ClrType);
            Assert.Equal("Edm.Boolean", EdmPrimitiveType.Boolean.FullName);
            Assert.Same(EdmPrimitiveType.Boolean, EdmPrimitiveType.Boolean);
            Assert.Equal(EdmPrimitiveType.Boolean.ToString(), EdmPrimitiveType.Boolean.FullName);
        }

        [Fact]
        public void Byte()
        {
            Assert.Equal(typeof(byte), EdmPrimitiveType.Byte.ClrType);
            Assert.Equal("Edm.Byte", EdmPrimitiveType.Byte.FullName);
            Assert.Same(EdmPrimitiveType.Byte, EdmPrimitiveType.Byte);
            Assert.Equal(EdmPrimitiveType.Byte.ToString(), EdmPrimitiveType.Byte.FullName);
        }

        [Fact]
        public void DateTime()
        {
            Assert.Equal(typeof(DateTime), EdmPrimitiveType.DateTime.ClrType);
            Assert.Equal("Edm.DateTime", EdmPrimitiveType.DateTime.FullName);
            Assert.Same(EdmPrimitiveType.DateTime, EdmPrimitiveType.DateTime);
            Assert.Equal(EdmPrimitiveType.DateTime.ToString(), EdmPrimitiveType.DateTime.FullName);
        }

        [Fact]
        public void DateTimeOffset()
        {
            Assert.Equal(typeof(DateTimeOffset), EdmPrimitiveType.DateTimeOffset.ClrType);
            Assert.Equal("Edm.DateTimeOffset", EdmPrimitiveType.DateTimeOffset.FullName);
            Assert.Same(EdmPrimitiveType.DateTimeOffset, EdmPrimitiveType.DateTimeOffset);
            Assert.Equal(EdmPrimitiveType.DateTimeOffset.ToString(), EdmPrimitiveType.DateTimeOffset.FullName);
        }

        [Fact]
        public void Decimal()
        {
            Assert.Equal(typeof(decimal), EdmPrimitiveType.Decimal.ClrType);
            Assert.Equal("Edm.Decimal", EdmPrimitiveType.Decimal.FullName);
            Assert.Same(EdmPrimitiveType.Decimal, EdmPrimitiveType.Decimal);
            Assert.Equal(EdmPrimitiveType.Decimal.ToString(), EdmPrimitiveType.Decimal.FullName);
        }

        [Fact]
        public void Double()
        {
            Assert.Equal(typeof(double), EdmPrimitiveType.Double.ClrType);
            Assert.Equal("Edm.Double", EdmPrimitiveType.Double.FullName);
            Assert.Same(EdmPrimitiveType.Double, EdmPrimitiveType.Double);
            Assert.Equal(EdmPrimitiveType.Double.ToString(), EdmPrimitiveType.Double.FullName);
        }

        [Fact]
        public void Guid()
        {
            Assert.Equal(typeof(Guid), EdmPrimitiveType.Guid.ClrType);
            Assert.Equal("Edm.Guid", EdmPrimitiveType.Guid.FullName);
            Assert.Same(EdmPrimitiveType.Guid, EdmPrimitiveType.Guid);
            Assert.Equal(EdmPrimitiveType.Guid.ToString(), EdmPrimitiveType.Guid.FullName);
        }

        [Fact]
        public void Int16()
        {
            Assert.Equal(typeof(short), EdmPrimitiveType.Int16.ClrType);
            Assert.Equal("Edm.Int16", EdmPrimitiveType.Int16.FullName);
            Assert.Same(EdmPrimitiveType.Int16, EdmPrimitiveType.Int16);
            Assert.Equal(EdmPrimitiveType.Int16.ToString(), EdmPrimitiveType.Int16.FullName);
        }

        [Fact]
        public void Int32()
        {
            Assert.Equal(typeof(int), EdmPrimitiveType.Int32.ClrType);
            Assert.Equal("Edm.Int32", EdmPrimitiveType.Int32.FullName);
            Assert.Same(EdmPrimitiveType.Int32, EdmPrimitiveType.Int32);
            Assert.Equal(EdmPrimitiveType.Int32.ToString(), EdmPrimitiveType.Int32.FullName);
        }

        [Fact]
        public void Int64()
        {
            Assert.Equal(typeof(long), EdmPrimitiveType.Int64.ClrType);
            Assert.Equal("Edm.Int64", EdmPrimitiveType.Int64.FullName);
            Assert.Same(EdmPrimitiveType.Int64, EdmPrimitiveType.Int64);
            Assert.Equal(EdmPrimitiveType.Int64.ToString(), EdmPrimitiveType.Int64.FullName);
        }

        [Fact]
        public void SByte()
        {
            Assert.Equal(typeof(sbyte), EdmPrimitiveType.SByte.ClrType);
            Assert.Equal("Edm.SByte", EdmPrimitiveType.SByte.FullName);
            Assert.Same(EdmPrimitiveType.SByte, EdmPrimitiveType.SByte);
            Assert.Equal(EdmPrimitiveType.SByte.ToString(), EdmPrimitiveType.SByte.FullName);
        }

        [Fact]
        public void Single()
        {
            Assert.Equal(typeof(float), EdmPrimitiveType.Single.ClrType);
            Assert.Equal("Edm.Single", EdmPrimitiveType.Single.FullName);
            Assert.Same(EdmPrimitiveType.Single, EdmPrimitiveType.Single);
            Assert.Equal(EdmPrimitiveType.Single.ToString(), EdmPrimitiveType.Single.FullName);
        }

        [Fact]
        public void String()
        {
            Assert.Equal(typeof(string), EdmPrimitiveType.String.ClrType);
            Assert.Equal("Edm.String", EdmPrimitiveType.String.FullName);
            Assert.Same(EdmPrimitiveType.String, EdmPrimitiveType.String);
            Assert.Equal(EdmPrimitiveType.String.ToString(), EdmPrimitiveType.String.FullName);
        }

        [Fact]
        public void Time()
        {
            Assert.Equal(typeof(TimeSpan), EdmPrimitiveType.Time.ClrType);
            Assert.Equal("Edm.Time", EdmPrimitiveType.Time.FullName);
            Assert.Same(EdmPrimitiveType.Time, EdmPrimitiveType.Time);
            Assert.Equal(EdmPrimitiveType.Time.ToString(), EdmPrimitiveType.Time.FullName);
        }
    }
}