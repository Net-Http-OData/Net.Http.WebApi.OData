namespace Net.Http.WebApi.Tests.OData.Query.Validators
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Validators;
    using Xunit;

    public class InlineCountQueryOptionValidatorTests
    {
        public class WhenTheInlineCountQueryOptionIsSetAndItIsNotAValidValue
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.InlineCount
            };

            public WhenTheInlineCountQueryOptionIsSetAndItIsNotAValidValue()
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$inlinecount=x");

                this.queryOptions = new ODataQueryOptions(requestMessage);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithBadRequest()
            {
                var exception = Assert.Throws<HttpResponseException>(
                    () => InlineCountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
            }
        }

        public class WhenTheInlineCountQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            public WhenTheInlineCountQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions()
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$inlinecount=allpages");

                this.queryOptions = new ODataQueryOptions(requestMessage);
            }

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => InlineCountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.UnsupportedQueryOption.FormatWith("$inlinecount"), exception.Message);
            }
        }

        public class WhenTheInlineCountQueryOptionIsSetToAllPagesAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.InlineCount
            };

            public WhenTheInlineCountQueryOptionIsSetToAllPagesAndItIsSpecifiedInAllowedQueryOptions()
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$inlinecount=allpages");

                this.queryOptions = new ODataQueryOptions(requestMessage);
            }

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => InlineCountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheInlineCountQueryOptionIsSetToNoneAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.InlineCount
            };

            public WhenTheInlineCountQueryOptionIsSetToNoneAndItIsSpecifiedInAllowedQueryOptions()
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$inlinecount=none");

                this.queryOptions = new ODataQueryOptions(requestMessage);
            }

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => InlineCountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }
    }
}