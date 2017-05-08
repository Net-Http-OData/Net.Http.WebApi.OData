namespace Net.Http.WebApi.OData.Tests.Query.Validators
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Validators;
    using OData.Model;
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
                var exception = Assert.Throws<HttpResponseException>(
                    () => CountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.CountRawValueInvalid, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
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
                var exception = Assert.Throws<HttpResponseException>(
                    () => CountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedQueryOption.FormatWith("$count"), ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
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
                Assert.DoesNotThrow(() => CountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));
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
                Assert.DoesNotThrow(() => CountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }
    }
}