namespace Net.Http.WebApi.Tests.OData.Query.Validators
{
    using System.Net.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Validators;
    using Xunit;

    public class InlineCountQueryOptionValidatorTests
    {
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

                Assert.Equal(Messages.InlineCountQueryOptionNotSupported, exception.Message);
            }
        }

        public class WhenTheInlineCountQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.InlineCount
            };

            public WhenTheInlineCountQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions()
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
    }
}