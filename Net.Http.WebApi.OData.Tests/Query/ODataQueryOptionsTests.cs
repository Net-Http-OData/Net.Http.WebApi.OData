namespace Net.Http.WebApi.OData.Tests.Query
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Model;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Expressions;
    using Xunit;

    public class ODataQueryOptionsTests
    {
        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullHttpReuestMessage()
        {
            TestHelper.EnsureEDM();

            Assert.Throws<ArgumentNullException>(
                () => new ODataQueryOptions(null, EntityDataModel.Current.EntitySets["Products"]));
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForNullModel()
        {
            TestHelper.EnsureEDM();

            Assert.Throws<ArgumentNullException>(
                () => new ODataQueryOptions(new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products"), null));
        }

        /// <summary>
        /// https://github.com/TrevorPilley/Net.Http.WebApi.OData/issues/85 - Not parsing ampersand in query
        /// </summary>
        [Fact]
        public void Parse_WithAmpersandInQuery()
        {
            TestHelper.EnsureEDM();

            var option = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=LegacyId+eq+2139+and+CompanyName+eq+'Pool+Farm+%26+Primrose+Hill+Nursery'&$top=1"),
                EntityDataModel.Current.EntitySets["Customers"]);

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
            Assert.Equal("CompanyName", ((PropertyAccessNode)nodeRight.Left).Property.Name);
            Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
            Assert.IsType<ConstantNode>(nodeRight.Right);
            Assert.Equal("Pool Farm & Primrose Hill Nursery", ((ConstantNode)nodeRight.Right).Value);
        }

        [Fact]
        public void SkipThrowsODataExceptionForNonNumeric()
        {
            TestHelper.EnsureEDM();

            var option = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$skip=A"),
                EntityDataModel.Current.EntitySets["Customers"]);

            var exception = Assert.Throws<ODataException>(() => option.Skip);

            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
            Assert.Equal("The value for OData query $skip must be a non-negative numeric value", exception.Message);
        }

        [Fact]
        public void TopThrowsODataExceptionForNonNumeric()
        {
            TestHelper.EnsureEDM();

            var option = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$top=A"),
                EntityDataModel.Current.EntitySets["Customers"]);

            var exception = Assert.Throws<ODataException>(() => option.Top);

            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
            Assert.Equal("The value for OData query $top must be a non-negative numeric value", exception.Message);
        }

        public class WhenConstructedWithAllQueryOptions
        {
            private readonly HttpRequestMessage httpRequestMessage;
            private readonly ODataQueryOptions option;

            public WhenConstructedWithAllQueryOptions()
            {
                TestHelper.EnsureEDM();

                this.httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Products?$count=true&$expand=Category&$filter=Name eq 'Milk'&$format=json&$orderby=Name&$search=blue OR green&$select=Name,Price&$skip=10&$skiptoken=5&$top=25");

                this.option = new ODataQueryOptions(this.httpRequestMessage, EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void TheCountOptionShouldBeSet()
            {
                Assert.True(this.option.Count);
            }

            [Fact]
            public void TheEntitySetShouldBeSet()
            {
                Assert.NotNull(this.option.EntitySet);
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
                Assert.Equal("blue OR green", this.option.Search);
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
                TestHelper.EnsureEDM();

                this.httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Products");

                this.option = new ODataQueryOptions(this.httpRequestMessage, EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void TheCountOptionShouldNotBeSet()
            {
                Assert.False(this.option.Count);
            }

            [Fact]
            public void TheEntitySetShouldBeSet()
            {
                Assert.NotNull(this.option.EntitySet);
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

        /// <summary>
        /// Issue #58 - Plus character in uri should be treated as a space
        /// </summary>
        public class WhenConstructedWithPlusSignsInsteadOfSpacesInTheUrl
        {
            private readonly HttpRequestMessage httpRequestMessage;
            private readonly ODataQueryOptions option;

            public WhenConstructedWithPlusSignsInsteadOfSpacesInTheUrl()
            {
                TestHelper.EnsureEDM();

                this.httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Employees?$filter=Forename+eq+'John'&$orderby=Forename+asc");

                this.option = new ODataQueryOptions(this.httpRequestMessage, EntityDataModel.Current.EntitySets["Employees"]);
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
                Assert.Equal("$filter=Forename eq 'John'", this.option.Filter.RawValue);
            }

            [Fact]
            public void TheOrderByOptionShouldBeCorrect()
            {
                Assert.Equal(1, this.option.OrderBy.Properties.Count);
                Assert.Equal(OrderByDirection.Ascending, this.option.OrderBy.Properties[0].Direction);
                Assert.Equal("Forename", this.option.OrderBy.Properties[0].Property.Name);
                Assert.Equal("Forename asc", this.option.OrderBy.Properties[0].RawValue);
            }

            [Fact]
            public void TheOrderByOptionShouldBeSet()
            {
                Assert.NotNull(this.option.OrderBy);
            }

            [Fact]
            public void TheOrderByOptionShouldHaveTheUnescapedRawValue()
            {
                Assert.Equal("$orderby=Forename asc", this.option.OrderBy.RawValue);
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
                TestHelper.EnsureEDM();

                this.httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://services.odata.org/OData/Employees?$filter=Forename+eq+'John'+and+ImageData+eq+'TG9yZW0gaXBzdW0gZG9s%2Bb3Igc2l0IGF%3D'");

                this.option = new ODataQueryOptions(this.httpRequestMessage, EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void TheFilterOptionShouldBeSet()
            {
                Assert.NotNull(this.option.Filter);
            }

            [Fact]
            public void TheFilterOptionShouldHaveTheUnescapedRawValue()
            {
                Assert.Equal("$filter=Forename eq 'John' and ImageData eq 'TG9yZW0gaXBzdW0gZG9s+b3Igc2l0IGF='", this.option.Filter.RawValue);
            }
        }
    }
}