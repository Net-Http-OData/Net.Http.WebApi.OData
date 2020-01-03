﻿namespace Net.Http.OData.Tests.Query.Parsers
{
    using System.Net;
    using Net.Http.OData;
    using Net.Http.OData.Model;
    using Net.Http.OData.Query.Parsers;
    using Net.Http.OData.Tests;
    using Xunit;

    public partial class FilterExpressionParserTests
    {
        public class InvalidSyntax
        {
            public InvalidSyntax()
            {
                TestHelper.EnsureEDM();
            }

            [Fact]
            public void ParseFunctionEqMissingExpression()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterExpressionParser.Parse("ceiling(Freight) eq", EntityDataModel.Current.EntitySets["Orders"].EdmType));

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("Unable to parse the specified $filter system query option, the binary operator Equal has no right node", exception.Message);
            }

            [Fact]
            public void ParseFunctionExtraBeginParenthesisEqExpression()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterExpressionParser.Parse("(ceiling(Freight) eq 32", EntityDataModel.Current.EntitySets["Orders"].EdmType)); ;

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("Unable to parse the specified $filter system query option, an extra opening or missing closing parenthesis may be present", exception.Message);
            }

            [Fact]
            public void ParseFunctionExtraEndParenthesisEqExpression()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterExpressionParser.Parse("ceiling(Freight) eq 32)", EntityDataModel.Current.EntitySets["Orders"].EdmType)); ;

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("Unable to parse the specified $filter system query option, closing parenthesis not expected", exception.Message);
            }

            [Fact]
            public void ParseFunctionMissingParameterExpression()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterExpressionParser.Parse("ceiling() eq 32", EntityDataModel.Current.EntitySets["Orders"].EdmType)); ;

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("Unable to parse the specified $filter system query option, the function ceiling has no parameters", exception.Message);
            }

            [Fact]
            public void ParseFunctionMissingParenthesisEqExpression()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterExpressionParser.Parse("ceiling(Freight eq 32", EntityDataModel.Current.EntitySets["Orders"].EdmType)); ;

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("Unable to parse the specified $filter system query option, an extra opening or missing closing parenthesis may be present", exception.Message);
            }

            [Fact]
            public void ParseFunctionMissingSecondParameterExpression()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterExpressionParser.Parse("cast(Colour,) eq 20", EntityDataModel.Current.EntitySets["Products"].EdmType)); ;

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("Unable to parse the specified $filter system query option, the function cast has a missing parameter or extra comma", exception.Message);
            }

            [Fact]
            public void ParseNotMissingExpression()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterExpressionParser.Parse("not", EntityDataModel.Current.EntitySets["Products"].EdmType)); ;

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("Unable to parse the specified $filter system query option, an incomplete filter has been specified", exception.Message);
            }

            [Fact]
            public void ParsePropertyEqMissingExpression()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterExpressionParser.Parse("Deleted eq", EntityDataModel.Current.EntitySets["Products"].EdmType)); ;

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("Unable to parse the specified $filter system query option, the binary operator Equal has no right node", exception.Message);
            }

            [Fact]
            public void ParsePropertyEqValueAndMissingExpression()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterExpressionParser.Parse("Deleted eq true and", EntityDataModel.Current.EntitySets["Products"].EdmType)); ;

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("Unable to parse the specified $filter system query option, an incomplete filter has been specified", exception.Message);
            }

            [Fact]
            public void ParsePropertyEqValueOrMissingExpression()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterExpressionParser.Parse("Deleted eq true or", EntityDataModel.Current.EntitySets["Products"].EdmType)); ;

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("Unable to parse the specified $filter system query option, an incomplete filter has been specified", exception.Message);
            }

            [Fact]
            public void ParseSingleOpeningParenthesis()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterExpressionParser.Parse("(", EntityDataModel.Current.EntitySets["Orders"].EdmType)); ;

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("Unable to parse the specified $filter system query option, an incomplete filter has been specified", exception.Message);
            }
        }
    }
}