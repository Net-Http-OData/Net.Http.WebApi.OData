namespace Net.Http.WebApi.OData.Tests.Query
{
    using System;
    using System.Net;
    using System.Web.Http;
    using Net.Http.WebApi.OData.Query;
    using Xunit;

    public class ODataRawQueryOptionsTests
    {
        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullHttpReuestMessage()
        {
            Assert.Throws<ArgumentNullException>(() => new ODataRawQueryOptions(null));
        }

        public class WhenCallingConstructorWithAllQueryOptions
        {
            private readonly ODataRawQueryOptions rawQueryOptions;

            public WhenCallingConstructorWithAllQueryOptions()
            {
                this.rawQueryOptions = new ODataRawQueryOptions(
                    "?$count=true&$expand=*&$filter=Name eq 'Fred'&$format=json&$orderby=Name&$search=blue OR green&$select=Name,Id&$skip=10&$skiptoken=5&$top=25");
            }

            [Fact]
            public void CountShouldBeSet()
            {
                Assert.Equal("$count=true", this.rawQueryOptions.Count);
            }

            [Fact]
            public void ExpandShouldBeSet()
            {
                Assert.Equal("$expand=*", this.rawQueryOptions.Expand);
            }

            [Fact]
            public void FilterShouldBeSet()
            {
                Assert.Equal("$filter=Name eq 'Fred'", this.rawQueryOptions.Filter);
            }

            [Fact]
            public void FormatShouldBeSet()
            {
                Assert.Equal("$format=json", this.rawQueryOptions.Format);
            }

            [Fact]
            public void OrderByShouldBeSet()
            {
                Assert.Equal("$orderby=Name", this.rawQueryOptions.OrderBy);
            }

            [Fact]
            public void SearchShouldBeSet()
            {
                Assert.Equal("$search=blue OR green", this.rawQueryOptions.Search);
            }

            [Fact]
            public void SelectShouldBeSet()
            {
                Assert.Equal("$select=Name,Id", this.rawQueryOptions.Select);
            }

            [Fact]
            public void SkipShouldBeSet()
            {
                Assert.Equal("$skip=10", this.rawQueryOptions.Skip);
            }

            [Fact]
            public void SkipTokenShouldBeSet()
            {
                Assert.Equal("$skiptoken=5", this.rawQueryOptions.SkipToken);
            }

            [Fact]
            public void TopShouldBeSet()
            {
                Assert.Equal("$top=25", this.rawQueryOptions.Top);
            }

            [Fact]
            public void ToStringShouldReturnTheOriginalQuery()
            {
                Assert.Equal(
                    "?$count=true&$expand=*&$filter=Name eq 'Fred'&$format=json&$orderby=Name&$search=blue OR green&$select=Name,Id&$skip=10&$skiptoken=5&$top=25",
                    rawQueryOptions.ToString());
            }
        }

        public class WhenCallingConstructorWithAnUnknownQueryOptionWhichDoesNotStartsWithADollar
        {
            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                new ODataRawQueryOptions("wibble=*");
            }
        }

        public class WhenCallingConstructorWithAnUnknownQueryOptionWhichStartsWithADollar
        {
            [Fact]
            public void AnHttpResponseExceptionShouldBeThrown()
            {
                var exception = Assert.Throws<HttpResponseException>(() => new ODataRawQueryOptions("?$wibble=*"));

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
            }
        }

        public class WhenCallingConstructorWithNoQueryOptions
        {
            private readonly ODataRawQueryOptions rawQueryOptions;

            public WhenCallingConstructorWithNoQueryOptions()
            {
                this.rawQueryOptions = new ODataRawQueryOptions(string.Empty);
            }

            [Fact]
            public void CountShouldBeNull()
            {
                Assert.Null(this.rawQueryOptions.Count);
            }

            [Fact]
            public void ExpandShouldBeNull()
            {
                Assert.Null(this.rawQueryOptions.Expand);
            }

            [Fact]
            public void FilterShouldBeNull()
            {
                Assert.Null(this.rawQueryOptions.Filter);
            }

            [Fact]
            public void FormatShouldBeNull()
            {
                Assert.Null(this.rawQueryOptions.Format);
            }

            [Fact]
            public void OrderByShouldBeNull()
            {
                Assert.Null(this.rawQueryOptions.OrderBy);
            }

            [Fact]
            public void SearchShouldBeNull()
            {
                Assert.Null(this.rawQueryOptions.Search);
            }

            [Fact]
            public void SelectShouldBeNull()
            {
                Assert.Null(this.rawQueryOptions.Select);
            }

            [Fact]
            public void SkipShouldBeNull()
            {
                Assert.Null(this.rawQueryOptions.Skip);
            }

            [Fact]
            public void SkipTokenShouldBeNull()
            {
                Assert.Null(this.rawQueryOptions.SkipToken);
            }

            [Fact]
            public void TopShouldBeNull()
            {
                Assert.Null(this.rawQueryOptions.Top);
            }

            [Fact]
            public void ToStringShouldReturnEmpty()
            {
                Assert.Equal(string.Empty, rawQueryOptions.ToString());
            }
        }
    }
}