namespace Net.Http.WebApi.Tests.OData.Query
{
    using System;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Xunit;

    public class InlineCountQueryOptionTests
    {
        public class WhenConstructedWithAnIncorrectlyCasedValue
        {
            [Fact]
            public void AnArgumentOutOfRangeExceptionShouldBeThrown()
            {
                var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new InlineCountQueryOption("$inlinecount=AllPages"));
                Assert.Equal(Messages.InlineCountRawValueInvalid + "\r\nParameter name: rawValue", exception.Message);
            }
        }

        public class WhenConstructedWithAnInvalidValue
        {
            [Fact]
            public void AnArgumentOutOfRangeExceptionShouldBeThrown()
            {
                var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new InlineCountQueryOption("$inlinecount=wibble"));
                Assert.Equal(Messages.InlineCountRawValueInvalid + "\r\nParameter name: rawValue", exception.Message);
            }
        }

        public class WhenConstructedWithInlineCountAllPages
        {
            private readonly InlineCountQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithInlineCountAllPages()
            {
                this.rawValue = "$inlinecount=allpages";
                this.option = new InlineCountQueryOption(this.rawValue);
            }

            [Fact]
            public void TheInlineCountShouldEqualAllPages()
            {
                Assert.Equal(InlineCount.AllPages, this.option.InlineCount);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }

        public class WhenConstructedWithInlineCountNone
        {
            private readonly InlineCountQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithInlineCountNone()
            {
                this.rawValue = "$inlinecount=none";
                this.option = new InlineCountQueryOption(this.rawValue);
            }

            [Fact]
            public void TheInlineCountShouldEqualNone()
            {
                Assert.Equal(InlineCount.None, this.option.InlineCount);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }
    }
}