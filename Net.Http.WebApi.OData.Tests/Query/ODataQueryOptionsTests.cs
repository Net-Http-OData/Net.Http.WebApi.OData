namespace Net.Http.WebApi.Tests.OData.Query
{
    using System.Net.Http;
    using Net.Http.WebApi.OData.Query;
    using WebApi.OData.Query.Expressions;
    using Xunit;

    public class ODataQueryOptionsTests
    {
        public class WhenCreatedWithAllQueryOptions
        {
            private readonly HttpRequestMessage httpRequestMessage;
            private readonly ODataQueryOptions option;

            public WhenCreatedWithAllQueryOptions()
            {
                this.httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://localhost/api?$filter=Name eq 'John'&$format=json&$inlinecount=allpages&$orderby=Name&$select=Name,Id&$skip=10&$top=25&$expand=Orders");

                this.option = new ODataQueryOptions(this.httpRequestMessage);
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
            public void TheInlineCountOptionShouldBeSet()
            {
                Assert.NotNull(this.option.InlineCount);
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
            public void TheTopPropertyShouldBeSet()
            {
                Assert.NotNull(this.option.Top);
            }
        }

        public class WhenCreatedWithNoQueryOptions
        {
            private readonly HttpRequestMessage httpRequestMessage;
            private readonly ODataQueryOptions option;

            public WhenCreatedWithNoQueryOptions()
            {
                this.httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://localhost/api");

                this.option = new ODataQueryOptions(this.httpRequestMessage);
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
            public void TheInlineCountOptionShouldNotBeSet()
            {
                Assert.Null(this.option.InlineCount);
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
            public void TheTopPropertyShouldBeNotSet()
            {
                Assert.Null(this.option.Top);
            }
        }
        
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
            Assert.IsType<SingleValuePropertyAccessNode>(nodeLeft.Left);
            Assert.Equal("LegacyId", ((SingleValuePropertyAccessNode)nodeLeft.Left).PropertyName);
            Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
            Assert.IsType<ConstantNode>(nodeLeft.Right);
            Assert.Equal(2139, ((ConstantNode)nodeLeft.Right).Value);

            Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

            Assert.IsType<BinaryOperatorNode>(node.Right);
            var nodeRight = (BinaryOperatorNode)node.Right;
            Assert.IsType<SingleValuePropertyAccessNode>(nodeRight.Left);
            Assert.Equal("Name", ((SingleValuePropertyAccessNode)nodeRight.Left).PropertyName);
            Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
            Assert.IsType<ConstantNode>(nodeRight.Right);
            Assert.Equal("Pool Farm & Primrose Hill Nursery", ((ConstantNode)nodeRight.Right).Value);
        }

        /// <summary>
        /// Issue #58 - Plus character in uri should be treated as a space
        /// </summary>
        public class WhenCreatedWithPlusSignsInsteadOfSpacesInTheUrl
        {
            private readonly HttpRequestMessage httpRequestMessage;
            private readonly ODataQueryOptions option;

            public WhenCreatedWithPlusSignsInsteadOfSpacesInTheUrl()
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

                Assert.IsType<SingleValuePropertyAccessNode>(node.Left);
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
                Assert.Equal("Name", this.option.OrderBy.Properties[0].Name);
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
        public class WhenCreatedWithUrlEncodedPlusSignsAndPlusSignsInsteadOfSpacesInTheUrl
        {
            private readonly HttpRequestMessage httpRequestMessage;
            private readonly ODataQueryOptions option;

            public WhenCreatedWithUrlEncodedPlusSignsAndPlusSignsInsteadOfSpacesInTheUrl()
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