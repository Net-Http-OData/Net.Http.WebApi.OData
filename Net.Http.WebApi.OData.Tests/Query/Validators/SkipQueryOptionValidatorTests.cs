namespace Net.Http.WebApi.OData.Tests.Query.Validators
{
    using System.Net.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Validators;
    using Xunit;

    public class SkipQueryOptionValidatorTests
    {
        public class WhenTheSkipQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$skip=50"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => SkipQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.UnsupportedQueryOption.FormatWith("$skip"), exception.Message);
            }
        }

        public class WhenTheSkipQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$skip=50"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Skip
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => SkipQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenValidatingAndTheValueIsAboveZero
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$skip=10"));

            [Fact]
            public void NoExceptionIsThrown()
            {
                Assert.DoesNotThrow(() => SkipQueryOptionValidator.Validate(this.queryOptions, ODataValidationSettings.All));
            }
        }

        public class WhenValidatingAndTheValueIsBelowZero
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$skip=-10"));

            [Fact]
            public void AnExceptionIsThrown()
            {
                Assert.Throws<ODataException>(
                    () => SkipQueryOptionValidator.Validate(this.queryOptions, ODataValidationSettings.All));
            }
        }
    }
}