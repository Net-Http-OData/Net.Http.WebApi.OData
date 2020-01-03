namespace Net.Http.OData.Tests.Query.Validators
{
    using System.Net;
    using System.Net.Http;
    using Net.Http.OData;
    using Net.Http.OData.Model;
    using Net.Http.OData.Query;
    using Net.Http.OData.Query.Validators;
    using Net.Http.OData.Tests;
    using Xunit;

    public class TopQueryValidatorTests
    {
        public class WhenTheTopQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            public WhenTheTopQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$top=50"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => TopQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal("The query option $top is not implemented by this service", exception.Message);
            }
        }

        public class WhenTheTopQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Top,
                MaxTop = 100
            };

            public WhenTheTopQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$top=50"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                TopQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenValidatingAndNoMaxTopIsSetButTheValueIsBelowZero
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Top
            };

            public WhenValidatingAndNoMaxTopIsSetButTheValueIsBelowZero()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$top=-1"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithBadRequest()
            {
                var exception = Assert.Throws<ODataException>(
                    () => TopQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("The integer value for $top is invalid, it must be an integer greater than zero and below the max value of 0 allowed by this service", exception.Message);
            }
        }

        public class WhenValidatingAndTheQueryOptionDoesNotExceedTheSpecifiedMaxTopValue
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Top,
                MaxTop = 100
            };

            public WhenValidatingAndTheQueryOptionDoesNotExceedTheSpecifiedMaxTopValue()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$top=25"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void NoExceptionIsThrown()
            {
                TopQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenValidatingAndTheQueryOptionDoesNotSpecifyATopValue
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                MaxTop = 100
            };

            public WhenValidatingAndTheQueryOptionDoesNotSpecifyATopValue()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void NoExceptionIsThrown()
            {
                TopQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenValidatingAndTheQueryOptionExceedsTheSpecifiedMaxTopValue
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Top,
                MaxTop = 100
            };

            public WhenValidatingAndTheQueryOptionExceedsTheSpecifiedMaxTopValue()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$top=150"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithBadRequest()
            {
                var exception = Assert.Throws<ODataException>(
                    () => TopQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("The integer value for $top is invalid, it must be an integer greater than zero and below the max value of 100 allowed by this service", exception.Message);
            }
        }
    }
}