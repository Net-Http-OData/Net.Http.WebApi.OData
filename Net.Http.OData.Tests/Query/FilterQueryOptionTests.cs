﻿namespace Net.Http.OData.Tests.Query
{
    using Net.Http.OData.Model;
    using Net.Http.OData.Query;
    using Net.Http.OData.Tests;
    using Xunit;

    public class FilterQueryOptionTests
    {
        public class WhenConstructedWithAValidValue
        {
            private readonly FilterQueryOption option;
            private readonly string rawValue;

            public WhenConstructedWithAValidValue()
            {
                TestHelper.EnsureEDM();

                var model = EntityDataModel.Current.EntitySets["Customers"].EdmType;

                this.rawValue = "$filter=CompanyName eq 'Alfreds Futterkiste'";
                this.option = new FilterQueryOption(this.rawValue, model);
            }

            [Fact]
            public void TheExpressionShouldBeSet()
            {
                Assert.NotNull(this.option.Expression);
            }

            [Fact]
            public void TheRawValueShouldEqualTheValuePassedToTheConstructor()
            {
                Assert.Equal(this.rawValue, this.option.RawValue);
            }
        }
    }
}