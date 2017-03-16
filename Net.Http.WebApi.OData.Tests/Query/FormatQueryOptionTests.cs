namespace Net.Http.WebApi.OData.Tests.Query
{
    using Net.Http.WebApi.OData.Query;
    using Xunit;

    public class FormatQueryOptionTests
    {
        public class WhenConstructedWithRawValueAtom
        {
            private readonly FormatQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithRawValueAtom()
            {
                this.rawValue = "format=atom";
                this.option = new FormatQueryOption(this.rawValue);
            }

            [Fact]
            public void TheMediaTypeHeaderValueShouldBeApplicationAtomXml()
            {
                Assert.Equal("application/atom+xml", this.option.MediaTypeHeaderValue.MediaType);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }

        public class WhenConstructedWithRawValueJson
        {
            private readonly FormatQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithRawValueJson()
            {
                this.rawValue = "format=json";
                this.option = new FormatQueryOption(this.rawValue);
            }

            [Fact]
            public void TheMediaTypeHeaderValueShouldBeApplicationJson()
            {
                Assert.Equal("application/json", this.option.MediaTypeHeaderValue.MediaType);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }

        public class WhenConstructedWithRawValueVCard
        {
            private readonly FormatQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithRawValueVCard()
            {
                this.rawValue = "format=text/vcard";
                this.option = new FormatQueryOption(this.rawValue);
            }

            [Fact]
            public void TheMediaTypeHeaderValueShouldBeTextVCard()
            {
                Assert.Equal("text/vcard", this.option.MediaTypeHeaderValue.MediaType);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }

        public class WhenConstructedWithRawValueXml
        {
            private readonly FormatQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithRawValueXml()
            {
                this.rawValue = "format=xml";
                this.option = new FormatQueryOption(this.rawValue);
            }

            [Fact]
            public void TheMediaTypeHeaderValueShouldBeApplicationXml()
            {
                Assert.Equal("application/xml", this.option.MediaTypeHeaderValue.MediaType);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }
    }
}