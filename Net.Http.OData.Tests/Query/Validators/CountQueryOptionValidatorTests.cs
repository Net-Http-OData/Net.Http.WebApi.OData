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

    public class CountQueryOptionValidatorTests
    {
        public class WhenTheCountQueryOptionIsSetAndItIsNotAValidValue
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Count
            };

            public WhenTheCountQueryOptionIsSetAndItIsNotAValidValue()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$count=x"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithBadRequest()
            {
                var exception = Assert.Throws<ODataException>(
                    () => CountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("The supplied value for OData query $count is invalid, valid options are 'true' and 'false'", exception.Message);
            }
        }

        public class WhenTheCountQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            public WhenTheCountQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$count=true"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => CountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal("The query option $count is not implemented by this service", exception.Message);
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
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$count=true"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                CountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheCountQueryOptionIsSetToFalseAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Count
            };

            public WhenTheCountQueryOptionIsSetToFalseAndItIsSpecifiedInAllowedQueryOptions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$count=false"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                CountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }
    }
}