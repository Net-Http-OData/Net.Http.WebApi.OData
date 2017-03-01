namespace Net.Http.WebApi.Tests.OData.Query.Validators
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Validators;
    using Xunit;

    public class TopQueryValidatorTests
    {
        public class WhenTheTopQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$top=50"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<HttpResponseException>(
                    () => TopQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedQueryOption.FormatWith("$top"), ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }

        public class WhenTheTopQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$top=50"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Top,
                MaxTop = 100
            };

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                Assert.DoesNotThrow(() => TopQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenValidatingAndNoMaxTopIsSetButTheValueIsBelowZero
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$top=-1"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Top
            };

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithBadRequest()
            {
                var exception = Assert.Throws<HttpResponseException>(
                    () => TopQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.TopRawValueInvalid, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }

        public class WhenValidatingAndTheQueryOptionDoesNotExceedTheSpecifiedMaxTopValue
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$top=25"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Top,
                MaxTop = 100
            };

            [Fact]
            public void NoExceptionIsThrown()
            {
                Assert.DoesNotThrow(() => TopQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenValidatingAndTheQueryOptionDoesNotSpecifyATopValue
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                MaxTop = 100
            };

            [Fact]
            public void NoExceptionIsThrown()
            {
                Assert.DoesNotThrow(() => TopQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenValidatingAndTheQueryOptionExceedsTheSpecifiedMaxTopValue
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$top=150"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Top,
                MaxTop = 100
            };

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithBadRequest()
            {
                var exception = Assert.Throws<HttpResponseException>(
                    () => TopQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.TopValueExceedsMaxAllowed.FormatWith(validationSettings.MaxTop.ToString()), ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
            }
        }
    }
}