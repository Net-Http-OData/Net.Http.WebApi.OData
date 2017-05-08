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
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$inlinecount=x"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithBadRequest()
            {
                var exception = Assert.Throws<HttpResponseException>(
                    () => InlineCountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                Assert.Equal(Messages.InlineCountRawValueInvalid, ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
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
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$inlinecount=allpages"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<HttpResponseException>(
                    () => InlineCountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.Response.StatusCode);
                Assert.Equal(Messages.UnsupportedQueryOption.FormatWith("$inlinecount"), ((HttpError)((ObjectContent<HttpError>)exception.Response.Content).Value).Message);
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
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$inlinecount=allpages"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
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
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$inlinecount=none"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                Assert.DoesNotThrow(() => InlineCountQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }
    }
}