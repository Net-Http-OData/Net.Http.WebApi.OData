﻿namespace Net.Http.WebApi.Tests.OData.Query
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Net.Http.WebApi.OData.Query;
    using WebApi.OData;
    using WebApi.OData.Query.Expressions;
    using Xunit;

    public class ODataQueryOptionsTests
    {
        /// <summary>
        /// https://github.com/TrevorPilley/Net.Http.WebApi.OData/issues/85 - Not parsing ampersand in query
        /// </summary>
        [Fact]
        public void Parse_WithAmpersandInQuery()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "http://localhost/Business?$filter=LegacyId+eq+2139+and+Name+eq+'Pool+Farm+%26+Primrose+Hill+Nursery'&$top=1");

            var option = new ODataQueryOptions(httpRequestMessage);
            Assert.NotNull(option);
            Assert.NotNull(option.Filter);

            Assert.NotNull(option.Filter.Expression);
            Assert.IsType<BinaryOperatorNode>(option.Filter.Expression);

            var node = (BinaryOperatorNode)option.Filter.Expression;

            Assert.IsType<BinaryOperatorNode>(node.Left);
            var nodeLeft = (BinaryOperatorNode)node.Left;
            Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
            Assert.Equal("LegacyId", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
            Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
            Assert.IsType<ConstantNode>(nodeLeft.Right);
            Assert.Equal(2139, ((ConstantNode)nodeLeft.Right).Value);

            Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

            Assert.IsType<BinaryOperatorNode>(node.Right);
            var nodeRight = (BinaryOperatorNode)node.Right;
            Assert.IsType<PropertyAccessNode>(nodeRight.Left);
            Assert.Equal("Name", ((PropertyAccessNode)nodeRight.Left).Property.Name);
            Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
            Assert.IsType<ConstantNode>(nodeRight.Right);
            Assert.Equal("Pool Farm & Primrose Hill Nursery", ((ConstantNode)nodeRight.Right).Value);
        }

        public class WhenConstructedWithAllQueryOptions
        {
            private readonly HttpRequestMessage httpRequestMessage;
            private readonly ODataQueryOptions option;

            public WhenConstructedWithAllQueryOptions()
            {
                this.httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://localhost/api?$count=true&$expand=Orders&$filter=Name eq 'Fred'&$format=json&$orderby=Name&$search=blue OR green&$select=Name,Id&$skip=10&$skiptoken=5&$top=25");

                this.option = new ODataQueryOptions(this.httpRequestMessage);
            }

            [Fact]
            public void TheCountOptionShouldBeSet()
            {
                Assert.True(this.option.Count);
            }

            [Fact]
            public void TheExpandOptionShouldBeSet()
            {
                Assert.NotNull(this.option.Expand);
            }

            [Fact]
            public void TheFilterOptionShouldBeSet()
            {
                Assert.NotNull(this.option.Filter);
            }

            [Fact]
            public void TheFormatOptionShouldBeSet()
            {
                Assert.NotNull(this.option.Format);
            }

            [Fact]
            public void TheIsolationLevelIsNone()
            {
                Assert.Equal(ODataIsolationLevel.None, this.option.IsolationLevel);
            }

            [Fact]
            public void TheOrderByPropertyShouldBeSet()
            {
                Assert.NotNull(this.option.OrderBy);
            }

            [Fact]
            public void TheRawValuesPropertyShouldBeSet()
            {
                Assert.NotNull(this.option.RawValues);
            }

            [Fact]
            public void TheRequestPropertyShouldReturnTheRequestMessage()
            {
                Assert.Equal(this.httpRequestMessage, this.option.Request);
            }

            [Fact]
            public void TheSearchPropertyShouldBeSet()
            {
                Assert.NotNull(this.option.Search);
            }

            [Fact]
            public void TheSelectPropertyShouldBeSet()
            {
                Assert.NotNull(this.option.Select);
            }

            [Fact]
            public void TheSkipPropertyShouldBeSet()
            {
                Assert.NotNull(this.option.Skip);
            }

            [Fact]
            public void TheSkipTokenPropertyShouldBeSet()
            {
                Assert.NotNull(this.option.SkipToken);
            }

            [Fact]
            public void TheTopPropertyShouldBeSet()
            {
                Assert.NotNull(this.option.Top);
            }
        }

        public class WhenConstructedWithNoQueryOptions
        {
            private readonly HttpRequestMessage httpRequestMessage;
            private readonly ODataQueryOptions option;

            public WhenConstructedWithNoQueryOptions()
            {
                this.httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://localhost/api");

                this.option = new ODataQueryOptions(this.httpRequestMessage);
            }

            [Fact]
            public void TheCountOptionShouldNotBeSet()
            {
                Assert.False(this.option.Count);
            }

            [Fact]
            public void TheExpandOptionShouldNotBeSet()
            {
                Assert.Null(this.option.Expand);
            }

            [Fact]
            public void TheFilterOptionShouldNotBeSet()
            {
                Assert.Null(this.option.Filter);
            }

            [Fact]
            public void TheFormatOptionShouldNotBeSet()
            {
                Assert.Null(this.option.Format);
            }

            [Fact]
            public void TheIsolationLevelIsNone()
            {
                Assert.Equal(ODataIsolationLevel.None, this.option.IsolationLevel);
            }

            [Fact]
            public void TheOrderByPropertyShouldBeNotSet()
            {
                Assert.Null(this.option.OrderBy);
            }

            [Fact]
            public void TheRawValuesPropertyShouldBeSet()
            {
                Assert.NotNull(this.option.RawValues);
            }

            [Fact]
            public void TheRequestPropertyShouldReturnTheRequestMessage()
            {
                Assert.Equal(this.httpRequestMessage, this.option.Request);
            }

            [Fact]
            public void TheSearchPropertyShouldNotBeSet()
            {
                Assert.Null(this.option.Search);
            }

            [Fact]
            public void TheSelectPropertyShouldBeNotSet()
            {
                Assert.Null(this.option.Select);
            }

            [Fact]
            public void TheSkipPropertyShouldBeNotSet()
            {
                Assert.Null(this.option.Skip);
            }

            [Fact]
            public void TheSkipTokenPropertyShouldBeNotSet()
            {
                Assert.Null(this.option.SkipToken);
            }

            [Fact]
            public void TheTopPropertyShouldBeNotSet()
            {
                Assert.Null(this.option.Top);
            }
        }

        public class WhenConstructedWithODataIsolationHeaderContainingSnapshot
        {
            private readonly ODataQueryOptions option;

            public WhenConstructedWithODataIsolationHeaderContainingSnapshot()
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api");
                httpRequestMessage.Headers.Add(HeaderNames.ODataIsolation, "Snapshot");

                this.option = new ODataQueryOptions(httpRequestMessage);
            }

            [Fact]
            public void TheIsolationLevelIsSet()
            {
                Assert.Equal(ODataIsolationLevel.Snapshot, this.option.IsolationLevel);
            }
        }

        public class WhenConstructedWithODataIsolationHeaderNotContainingSnapshot
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrown()
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api");
                httpRequestMessage.Headers.Add(HeaderNames.ODataIsolation, "ReadCommitted");

                var exception = Assert.Throws<HttpResponseException>(() => new ODataQueryOptions(httpRequestMessage));

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedIsolationLevel, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }

        public class WhenConstructedWithODataVersionHeaderContaining1_0
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrown()
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api");
                httpRequestMessage.Headers.Add(HeaderNames.ODataVersion, "1.0");

                var exception = Assert.Throws<HttpResponseException>(() => new ODataQueryOptions(httpRequestMessage));

                Assert.Equal(HttpStatusCode.NotAcceptable, exception.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedODataVersion, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }

        public class WhenConstructedWithODataVersionHeaderContaining2_0
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrown()
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api");
                httpRequestMessage.Headers.Add(HeaderNames.ODataVersion, "2.0");

                var exception = Assert.Throws<HttpResponseException>(() => new ODataQueryOptions(httpRequestMessage));

                Assert.Equal(HttpStatusCode.NotAcceptable, exception.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedODataVersion, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }

        public class WhenConstructedWithODataVersionHeaderContaining3_0
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrown()
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api");
                httpRequestMessage.Headers.Add(HeaderNames.ODataVersion, "3.0");

                var exception = Assert.Throws<HttpResponseException>(() => new ODataQueryOptions(httpRequestMessage));

                Assert.Equal(HttpStatusCode.NotAcceptable, exception.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedODataVersion, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }

        public class WhenConstructedWithODataVersionHeaderContaining4_0
        {
            [Fact]
            public void AnExceptionIsNotThrown()
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api");
                httpRequestMessage.Headers.Add(HeaderNames.ODataVersion, "4.0");

                Assert.DoesNotThrow(() => new ODataQueryOptions(httpRequestMessage));
            }
        }

        /// <summary>
        /// Issue #58 - Plus character in uri should be treated as a space
        /// </summary>
        public class WhenConstructedWithPlusSignsInsteadOfSpacesInTheUrl
        {
            private readonly HttpRequestMessage httpRequestMessage;
            private readonly ODataQueryOptions option;

            public WhenConstructedWithPlusSignsInsteadOfSpacesInTheUrl()
            {
                this.httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://localhost/api?$filter=Name+eq+'John'&$orderby=Name+asc");

                this.option = new ODataQueryOptions(this.httpRequestMessage);
            }

            [Fact]
            public void TheFilterOptionExpressionShouldBeCorrect()
            {
                Assert.IsType<BinaryOperatorNode>(this.option.Filter.Expression);

                var node = (BinaryOperatorNode)this.option.Filter.Expression;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);
                Assert.IsType<ConstantNode>(node.Right);
            }

            [Fact]
            public void TheFilterOptionShouldBeSet()
            {
                Assert.NotNull(this.option.Filter);
            }

            [Fact]
            public void TheFilterOptionShouldHaveTheUnescapedRawValue()
            {
                Assert.Equal("$filter=Name eq 'John'", this.option.Filter.RawValue);
            }

            [Fact]
            public void TheOrderByOptionShouldBeCorrect()
            {
                Assert.Equal(1, this.option.OrderBy.Properties.Count);
                Assert.Equal(OrderByDirection.Ascending, this.option.OrderBy.Properties[0].Direction);
                Assert.Equal("Name", this.option.OrderBy.Properties[0].Property.Name);
                Assert.Equal("Name asc", this.option.OrderBy.Properties[0].RawValue);
            }

            [Fact]
            public void TheOrderByOptionShouldBeSet()
            {
                Assert.NotNull(this.option.OrderBy);
            }

            [Fact]
            public void TheOrderByOptionShouldHaveTheUnescapedRawValue()
            {
                Assert.Equal("$orderby=Name asc", this.option.OrderBy.RawValue);
            }
        }

        /// <summary>
        /// Issue #78 - Cannot send + in the request
        /// </summary>
        public class WhenConstructedWithUrlEncodedPlusSignsAndPlusSignsInsteadOfSpacesInTheUrl
        {
            private readonly HttpRequestMessage httpRequestMessage;
            private readonly ODataQueryOptions option;

            public WhenConstructedWithUrlEncodedPlusSignsAndPlusSignsInsteadOfSpacesInTheUrl()
            {
                this.httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://localhost/api?$filter=Name+eq+'John'+and+Data+eq+'TG9yZW0gaXBzdW0gZG9s%2Bb3Igc2l0IGF%3D'");

                this.option = new ODataQueryOptions(this.httpRequestMessage);
            }

            [Fact]
            public void TheFilterOptionShouldBeSet()
            {
                Assert.NotNull(this.option.Filter);
            }

            [Fact]
            public void TheFilterOptionShouldHaveTheUnescapedRawValue()
            {
                Assert.Equal("$filter=Name eq 'John' and Data eq 'TG9yZW0gaXBzdW0gZG9s+b3Igc2l0IGF='", this.option.Filter.RawValue);
            }
        }
    }
}