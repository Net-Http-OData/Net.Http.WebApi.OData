namespace Net.Http.WebApi.OData.Tests.Query.Validators
{
    using System.Net.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Validators;
    using Xunit;

    public class SkipQueryOptionValidatorTests
    {
        public class WhenValidatingAndTheValueIsAboveZero
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$skip=10"));

            [Fact]
            public void NoExceptionIsThrown()
            {
                Assert.DoesNotThrow(() => SkipQueryOptionValidator.Validate(this.queryOptions));
            }
        }

        public class WhenValidatingAndTheValueIsBelowZero
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$skip=-10"));

            [Fact]
            public void AnExceptionIsThrown()
            {
                Assert.Throws<ODataException>(() => SkipQueryOptionValidator.Validate(this.queryOptions));
            }
        }
    }
}