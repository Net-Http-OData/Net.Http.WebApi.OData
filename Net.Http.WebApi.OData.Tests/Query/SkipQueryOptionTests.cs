namespace Net.Http.WebApi.Tests.OData.Query
{
    using System;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Xunit;

    public class SkipQueryOptionTests
    {
        public class WhenConstructedWithAnInvalidValue
        {
            [Fact]
            public void AnArgumentOutOfRangeExceptionShouldBeThrown()
            {
                var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new SkipQueryOption("$skip=wibble"));

                Assert.Equal(Messages.SkipRawValueInvalid + "\r\nParameter name: rawValue", exception.Message);
            }
        }

        public class WhenConstructedWithAValidValue
        {
            private readonly SkipQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithAValidValue()
            {
                this.rawValue = "$skip=25";
                this.option = new SkipQueryOption(this.rawValue);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }

            [Fact]
            public void TheSkipValueShouldEqualTheValueFromTheRawValue()
            {
                Assert.Equal(25, this.option.Value);
            }
        }
    }
}