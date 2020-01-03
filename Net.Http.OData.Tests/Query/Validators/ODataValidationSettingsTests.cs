namespace Net.Http.OData.Tests.Query.Validators
{
    using Net.Http.OData.Query.Validators;
    using Xunit;

    public class ODataValidationSettingsTests
    {
        public class All
        {
            private readonly ODataValidationSettings all = ODataValidationSettings.All;

            [Fact]
            public void SetsAllowedArithmeticOperatorsToAll()
            {
                Assert.Equal(AllowedArithmeticOperators.All, this.all.AllowedArithmeticOperators);
            }

            [Fact]
            public void SetsAllowedFunctionsTAllFunctions()
            {
                Assert.Equal(AllowedFunctions.AllFunctions, this.all.AllowedFunctions);
            }

            [Fact]
            public void SetsAllowedLogicalOperatorsToAll()
            {
                Assert.Equal(AllowedLogicalOperators.All, this.all.AllowedLogicalOperators);
            }

            [Fact]
            public void SetsAllowedQueryOptionsToAll()
            {
                Assert.Equal(AllowedQueryOptions.All, this.all.AllowedQueryOptions);
            }

            [Fact]
            public void SetsMaxTopToOneHundred()
            {
                Assert.Equal(100, this.all.MaxTop);
            }
        }

        public class None
        {
            private readonly ODataValidationSettings none = ODataValidationSettings.None;

            [Fact]
            public void SetsAllowedArithmeticOperatorsToNone()
            {
                Assert.Equal(AllowedArithmeticOperators.None, this.none.AllowedArithmeticOperators);
            }

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