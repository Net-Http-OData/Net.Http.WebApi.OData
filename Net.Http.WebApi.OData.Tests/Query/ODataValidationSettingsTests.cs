namespace Net.Http.WebApi.Tests.OData.Query.Validators
{
    using Net.Http.WebApi.OData.Query;
    using Xunit;

    public class ODataValidationSettingsTests
    {
        public class None
        {
            private readonly ODataValidationSettings none = ODataValidationSettings.None;

            [Fact]
            public void SetsAllowedFunctionsToNone()
            {
                Assert.Equal(AllowedFunctions.None, this.none.AllowedFunctions);
            }

            [Fact]
            public void SetsAllowedLogicalOperatorsToNone()
            {
                Assert.Equal(AllowedLogicalOperators.None, this.none.AllowedLogicalOperators);
            }

            [Fact]
            public void SetsAllowedQueryOptionsToNone()
            {
                Assert.Equal(AllowedQueryOptions.None, this.none.AllowedQueryOptions);
            }

            [Fact]
            public void SetsMaxTopToZero()
            {
                Assert.Equal(0, this.none.MaxTop);
            }
        }
    }
}