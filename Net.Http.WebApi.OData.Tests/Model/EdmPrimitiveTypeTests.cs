namespace Net.Http.WebApi.OData.Tests.Model
{
    using System;
    using System.IO;
    using Net.Http.WebApi.OData.Model;
    using Xunit;

    public class EdmPrimitiveTypeTests
    {
        [Fact]
        public void Binary()
        {
            Assert.Equal(typeof(byte[]), EdmPrimitiveType.Binary.ClrType);
            Assert.Equal("Edm.Binary", EdmPrimitiveType.Binary.FullName);
            Assert.Equal("Binary", EdmPrimitiveType.Binary.Name);
            Assert.Same(EdmPrimitiveType.Binary, EdmPrimitiveType.Binary);
            Assert.Equal(EdmPrimitiveType.Binary.ToString(), EdmPrimitiveType.Binary.FullName);
        }

        [Fact]
        public void Boolean()
        {
            Assert.Equal(typeof(bool), EdmPrimitiveType.Boolean.ClrType);
            Assert.Equal("Edm.Boolean", EdmPrimitiveType.Boolean.FullName);
            Assert.Equal("Boolean", EdmPrimitiveType.Boolean.Name);
            Assert.Same(EdmPrimitiveType.Boolean, EdmPrimitiveType.Boolean);
            Assert.Equal(EdmPrimitiveType.Boolean.ToString(), EdmPrimitiveType.Boolean.FullName);
        }

        [Fact]
        public void Byte()
        {
            Assert.Equal(typeof(byte), EdmPrimitiveType.Byte.ClrType);
            Assert.Equal("Edm.Byte", EdmPrimitiveType.Byte.FullName);
            Assert.Equal("Byte", EdmPrimitiveType.Byte.Name);
            Assert.Same(EdmPrimitiveType.Byte, EdmPrimitiveType.Byte);
            Assert.Equal(EdmPrimitiveType.Byte.ToString(), EdmPrimitiveType.Byte.FullName);
        }

        [Fact]
        public void Date()
        {
            Assert.Equal(typeof(DateTime), EdmPrimitiveType.Date.ClrType);
            Assert.Equal("Edm.Date", EdmPrimitiveType.Date.FullName);
            Assert.Equal("Date", EdmPrimitiveType.Date.Name);
            Assert.Same(EdmPrimitiveType.Date, EdmPrimitiveType.Date);
            Assert.Equal(EdmPrimitiveType.Date.ToString(), EdmPrimitiveType.Date.FullName);
        }

        [Fact]
        public void DateTimeOffset()
        {
            Assert.Equal(typeof(DateTimeOffset), EdmPrimitiveType.DateTimeOffset.ClrType);
            Assert.Equal("Edm.DateTimeOffset", EdmPrimitiveType.DateTimeOffset.FullName);
            Assert.Equal("DateTimeOffset", EdmPrimitiveType.DateTimeOffset.Name);
            Assert.Same(EdmPrimitiveType.DateTimeOffset, EdmPrimitiveType.DateTimeOffset);
            Assert.Equal(EdmPrimitiveType.DateTimeOffset.ToString(), EdmPrimitiveType.DateTimeOffset.FullName);
        }

        [Fact]
        public void Decimal()
        {
            Assert.Equal(typeof(decimal), EdmPrimitiveType.Decimal.ClrType);
            Assert.Equal("Edm.Decimal", EdmPrimitiveType.Decimal.FullName);
            Assert.Equal("Decimal", EdmPrimitiveType.Decimal.Name);
            Assert.Same(EdmPrimitiveType.Decimal, EdmPrimitiveType.Decimal);
            Assert.Equal(EdmPrimitiveType.Decimal.ToString(), EdmPrimitiveType.Decimal.FullName);
        }

        [Fact]
        public void Double()
        {
            Assert.Equal(typeof(double), EdmPrimitiveType.Double.ClrType);
            Assert.Equal("Edm.Double", EdmPrimitiveType.Double.FullName);
            Assert.Equal("Double", EdmPrimitiveType.Double.Name);
            Assert.Same(EdmPrimitiveType.Double, EdmPrimitiveType.Double);
            Assert.Equal(EdmPrimitiveType.Double.ToString(), EdmPrimitiveType.Double.FullName);
        }

        [Fact]
        public void Duration()
        {
            Assert.Equal(typeof(TimeSpan), EdmPrimitiveType.Duration.ClrType);
            Assert.Equal("Edm.Duration", EdmPrimitiveType.Duration.FullName);
            Assert.Equal("Duration", EdmPrimitiveType.Duration.Name);
            Assert.Same(EdmPrimitiveType.Duration, EdmPrimitiveType.Duration);
            Assert.Equal(EdmPrimitiveType.Duration.ToString(), EdmPrimitiveType.Duration.FullName);
        }

        [Fact]
        public void Guid()
        {
            Assert.Equal(typeof(Guid), EdmPrimitiveType.Guid.ClrType);
            Assert.Equal("Edm.Guid", EdmPrimitiveType.Guid.FullName);
            Assert.Equal("Guid", EdmPrimitiveType.Guid.Name);
            Assert.Same(EdmPrimitiveType.Guid, EdmPrimitiveType.Guid);
            Assert.Equal(EdmPrimitiveType.Guid.ToString(), EdmPrimitiveType.Guid.FullName);
        }

        [Fact]
        public void Int16()
        {
            Assert.Equal(typeof(short), EdmPrimitiveType.Int16.ClrType);
            Assert.Equal("Edm.Int16", EdmPrimitiveType.Int16.FullName);
            Assert.Equal("Int16", EdmPrimitiveType.Int16.Name);
            Assert.Same(EdmPrimitiveType.Int16, EdmPrimitiveType.Int16);
            Assert.Equal(EdmPrimitiveType.Int16.ToString(), EdmPrimitiveType.Int16.FullName);
        }

        [Fact]
        public void Int32()
        {
            Assert.Equal(typeof(int), EdmPrimitiveType.Int32.ClrType);
            Assert.Equal("Edm.Int32", EdmPrimitiveType.Int32.FullName);
            Assert.Equal("Int32", EdmPrimitiveType.Int32.Name);
            Assert.Same(EdmPrimitiveType.Int32, EdmPrimitiveType.Int32);
            Assert.Equal(EdmPrimitiveType.Int32.ToString(), EdmPrimitiveType.Int32.FullName);
        }

        [Fact]
        public void Int64()
        {
            Assert.Equal(typeof(long), EdmPrimitiveType.Int64.ClrType);
            Assert.Equal("Edm.Int64", EdmPrimitiveType.Int64.FullName);
            Assert.Equal("Int64", EdmPrimitiveType.Int64.Name);
            Assert.Same(EdmPrimitiveType.Int64, EdmPrimitiveType.Int64);
            Assert.Equal(EdmPrimitiveType.Int64.ToString(), EdmPrimitiveType.Int64.FullName);
        }

        [Fact]
        public void SByte()
        {
            Assert.Equal(typeof(sbyte), EdmPrimitiveType.SByte.ClrType);
            Assert.Equal("Edm.SByte", EdmPrimitiveType.SByte.FullName);
            Assert.Equal("SByte", EdmPrimitiveType.SByte.Name);
            Assert.Same(EdmPrimitiveType.SByte, EdmPrimitiveType.SByte);
            Assert.Equal(EdmPrimitiveType.SByte.ToString(), EdmPrimitiveType.SByte.FullName);
        }

        [Fact]
        public void Single()
        {
            Assert.Equal(typeof(float), EdmPrimitiveType.Single.ClrType);
            Assert.Equal("Edm.Single", EdmPrimitiveType.Single.FullName);
            Assert.Equal("Single", EdmPrimitiveType.Single.Name);
            Assert.Same(EdmPrimitiveType.Single, EdmPrimitiveType.Single);
            Assert.Equal(EdmPrimitiveType.Single.ToString(), EdmPrimitiveType.Single.FullName);
        }

        [Fact]
        public void Stream()
        {
            Assert.Equal(typeof(Stream), EdmPrimitiveType.Stream.ClrType);
            Assert.Equal("Edm.Stream", EdmPrimitiveType.Stream.FullName);
            Assert.Equal("Stream", EdmPrimitiveType.Stream.Name);
            Assert.Same(EdmPrimitiveType.Stream, EdmPrimitiveType.Stream);
            Assert.Equal(EdmPrimitiveType.Stream.ToString(), EdmPrimitiveType.Stream.FullName);
        }

        [Fact]
        public void String()
        {
            Assert.Equal(typeof(string), EdmPrimitiveType.String.ClrType);
            Assert.Equal("Edm.String", EdmPrimitiveType.String.FullName);
            Assert.Equal("String", EdmPrimitiveType.String.Name);
            Assert.Same(EdmPrimitiveType.String, EdmPrimitiveType.String);
            Assert.Equal(EdmPrimitiveType.String.ToString(), EdmPrimitiveType.String.FullName);
        }

        [Fact]
        public void TimeOfDay()
        {
            Assert.Equal(typeof(TimeSpan), EdmPrimitiveType.TimeOfDay.ClrType);
            Assert.Equal("Edm.TimeOfDay", EdmPrimitiveType.TimeOfDay.FullName);
            Assert.Equal("TimeOfDay", EdmPrimitiveType.TimeOfDay.Name);
            Assert.Same(EdmPrimitiveType.TimeOfDay, EdmPrimitiveType.TimeOfDay);
            Assert.Equal(EdmPrimitiveType.TimeOfDay.ToString(), EdmPrimitiveType.TimeOfDay.FullName);
        }
    }
}