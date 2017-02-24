namespace Net.Http.WebApi.Tests.OData.Query.Validators
{
    using System.Net.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Validators;
    using Xunit;

    public class CountQueryOptionValidatorTests
    {
        public class WhenTheCountQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            public WhenTheCountQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions()
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$count=true");

                this.queryOptions = new ODataQueryOptions(requestMessage);
            }

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => CountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.UnsupportedQueryOption.FormatWith("$count"), exception.Message);
            }
        }

        public class WhenTheCountQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Count
            };

            public WhenTheCountQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions()
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$count=true");

                this.queryOptions = new ODataQueryOptions(requestMessage);
            }

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => CountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }
    }
}