namespace Net.Http.WebApi.OData.Tests.Query.Validators
{
    using System.Net;
    using System.Net.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Validators;
    using OData.Model;
    using Xunit;

    public class FilterQueryOptionValidatorTests
    {
        public class WhenTheFilterQueryOptionContainsTheAddOperatorAddItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.None,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            public WhenTheFilterQueryOptionContainsTheAddOperatorAddItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price add 100 eq 150"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("add"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheAddOperatorAddItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.Add,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            public WhenTheFilterQueryOptionContainsTheAddOperatorAddItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price add 100 eq 150"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheAndOperatorAndItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            public WhenTheFilterQueryOptionContainsTheAndOperatorAndItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=Forename eq 'John' and Surname eq 'Smith'"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("and"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheAndOperatorAndItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.And | AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheAndOperatorAndItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=Forename eq 'John' and Surname eq 'Smith'"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheCastAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheCastAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=cast(Price, 'Edm.Int64') eq 20"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("cast"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheCastFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Cast,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheCastFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=cast(Price, 'Edm.Int64') eq 20"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheCeilingFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheCeilingFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Orders?$filter=ceiling(Freight) eq 32"),
                    EntityDataModel.Current.EntitySets["Orders"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("ceiling"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheCeilingFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Ceiling,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheCeilingFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Orders?$filter=ceiling(Freight) eq 32"),
                    EntityDataModel.Current.EntitySets["Orders"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheConcatAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheConcatAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=concat(concat(City, ', '), Country) eq 'Berlin, Germany'"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("concat"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheConcatFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Concat,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheConcatFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=concat(concat(City, ', '), Country) eq 'Berlin, Germany'"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheContainsFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheContainsFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=contains(CompanyName,'Alfreds')"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("contains"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheContainsFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Contains,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheContainsFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=contains(CompanyName,'Alfreds')"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheDayFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheDayFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=day(BirthDate) eq 8"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("day"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheDayFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Day,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheDayFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=day(BirthDate) eq 8"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheDivideOperatorDivideItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.None,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            public WhenTheFilterQueryOptionContainsTheDivideOperatorDivideItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price div 100 eq 150"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("div"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheDivideOperatorDivideItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.Divide,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            public WhenTheFilterQueryOptionContainsTheDivideOperatorDivideItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price div 100 eq 150"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheEndsWithFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheEndsWithFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=endswith(Surname, 'yes') eq true"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("endswith"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheEndsWithFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.EndsWith,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheEndsWithFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=endswith(Surname, 'yes') eq true"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheEqualsOperatorEqualsItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            public WhenTheFilterQueryOptionContainsTheEqualsOperatorEqualsItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=Forename eq 'John'"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("eq"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheEqualsOperatorEqualsItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheEqualsOperatorEqualsItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=Forename eq 'John'"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheFloorFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheFloorFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Orders?$filter=floor(Freight) eq 32"),
                    EntityDataModel.Current.EntitySets["Orders"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("floor"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheFloorFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Floor,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheFloorFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Orders?$filter=floor(Freight) eq 32"),
                    EntityDataModel.Current.EntitySets["Orders"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheFractionalSecondsAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheFractionalSecondsAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=fractionalseconds(BirthDate) lt 0.1m"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("fractionalseconds"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheFractionalSecondsFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.FractionalSeconds,
                AllowedLogicalOperators = AllowedLogicalOperators.LessThan
            };

            public WhenTheFilterQueryOptionContainsTheFractionalSecondsFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=fractionalseconds(BirthDate) lt 0.1m"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheGreaterThanOperatorGreaterThanItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            public WhenTheFilterQueryOptionContainsTheGreaterThanOperatorGreaterThanItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price gt 35"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("gt"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheGreaterThanOperatorGreaterThanItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.GreaterThan
            };

            public WhenTheFilterQueryOptionContainsTheGreaterThanOperatorGreaterThanItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price gt 35"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheGreaterThanOrEqualsOperatorGreaterThanOrEqualsItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            public WhenTheFilterQueryOptionContainsTheGreaterThanOrEqualsOperatorGreaterThanOrEqualsItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price ge 35"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("ge"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheGreaterThanOrEqualsOperatorGreaterThanOrEqualsItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.GreaterThanOrEqual
            };

            public WhenTheFilterQueryOptionContainsTheGreaterThanOrEqualsOperatorGreaterThanOrEqualsItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price ge 35"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheHasOperatorHasItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            public WhenTheFilterQueryOptionContainsTheHasOperatorHasItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=AccessLevel has NorthwindModel.AccessLevel'Write'"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("has"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheHasOperatorHasItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.Has
            };

            public WhenTheFilterQueryOptionContainsTheHasOperatorHasItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=AccessLevel has NorthwindModel.AccessLevel'Write'"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheHourFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheHourFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=hour(BirthDate) eq 8"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("hour"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheHourFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Hour,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheHourFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=hour(BirthDate) eq 8"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheIndexOfFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheIndexOfFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=indexof(Surname, 'Hayes') eq 1"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("indexof"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheIndexOfFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.IndexOf,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheIndexOfFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=indexof(Surname, 'Hayes') eq 1"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheIsOfAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheIsOfAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=isof(Price, 'Edm.Int64')"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("isof"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheIsOfFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.IsOf,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheIsOfFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=isof(Price, 'Edm.Int64')"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheLengthFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheLengthFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=length(CompanyName) eq 19"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("length"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheLengthFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Length,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheLengthFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=length(CompanyName) eq 19"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheLessThanOperatorLessThanItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            public WhenTheFilterQueryOptionContainsTheLessThanOperatorLessThanItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price lt 35"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("lt"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheLessThanOperatorLessThanItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.LessThan
            };

            public WhenTheFilterQueryOptionContainsTheLessThanOperatorLessThanItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price lt 35"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheLessThanOrEqualOperatorLessThanOrEqualItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            public WhenTheFilterQueryOptionContainsTheLessThanOrEqualOperatorLessThanOrEqualItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price le 35"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("le"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheLessThanOrEqualOperatorLessThanOrEqualItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.LessThanOrEqual
            };

            public WhenTheFilterQueryOptionContainsTheLessThanOrEqualOperatorLessThanOrEqualItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price le 35"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMaxDateTimeAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheMaxDateTimeAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=ReleaseDate eq maxdatetime()"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("maxdatetime"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMaxDateTimeFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.MaxDateTime,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheMaxDateTimeFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=ReleaseDate eq maxdatetime()"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMinDateTimeAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheMinDateTimeAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=ReleaseDate eq mindatetime()"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("mindatetime"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMinDateTimeFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.MinDateTime,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheMinDateTimeFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=ReleaseDate eq mindatetime()"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMinuteFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheMinuteFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=minute(BirthDate) eq 8"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("minute"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMinuteFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Minute,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheMinuteFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=minute(BirthDate) eq 8"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheModuloOperatorModuloItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.None,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            public WhenTheFilterQueryOptionContainsTheModuloOperatorModuloItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price mod 100 eq 150"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("mod"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheModuloOperatorModuloItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.Modulo,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            public WhenTheFilterQueryOptionContainsTheModuloOperatorModuloItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price mod 100 eq 150"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMonthFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheMonthFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=month(BirthDate) eq 5"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("month"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMonthFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Month,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheMonthFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=month(BirthDate) eq 5"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMultiplyOperatorMultiplyItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.None,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            public WhenTheFilterQueryOptionContainsTheMultiplyOperatorMultiplyItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price mul 100 eq 150"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("mul"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMultiplyOperatorMultiplyItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.Multiply,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            public WhenTheFilterQueryOptionContainsTheMultiplyOperatorMultiplyItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price mul 100 eq 150"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheNotEqualsOperatorNotEqualsItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            public WhenTheFilterQueryOptionContainsTheNotEqualsOperatorNotEqualsItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=Forename ne 'John'"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("ne"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheNotEqualsOperatorNotEqualsItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.NotEqual
            };

            public WhenTheFilterQueryOptionContainsTheNotEqualsOperatorNotEqualsItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=Forename ne 'John'"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheNowAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None,
                AllowedLogicalOperators = AllowedLogicalOperators.GreaterThanOrEqual
            };

            public WhenTheFilterQueryOptionContainsTheNowAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=ReleaseDate eq now()"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("now"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheNowFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Now,
                AllowedLogicalOperators = AllowedLogicalOperators.GreaterThanOrEqual
            };

            public WhenTheFilterQueryOptionContainsTheNowFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=ReleaseDate ge now()"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheOrOperatorOrItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            public WhenTheFilterQueryOptionContainsTheOrOperatorOrItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=Forename eq 'John' or Surname eq 'Smith'"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("or"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheOrOperatorOrItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.Or | AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheOrOperatorOrItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=Forename eq 'John' or Surname eq 'Smith'"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheReplaceFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheReplaceFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=replace(CompanyName, ' ', '') eq 'AlfredsFutterkiste'"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("replace"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheReplaceFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Replace,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheReplaceFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=replace(CompanyName, ' ', '') eq 'AlfredsFutterkiste'"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheRoundFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheRoundFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Orders?$filter=round(Freight) eq 32"),
                    EntityDataModel.Current.EntitySets["Orders"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("round"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheRoundFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Round,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheRoundFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Orders?$filter=round(Freight) eq 32"),
                    EntityDataModel.Current.EntitySets["Orders"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSecondFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheSecondFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=second(BirthDate) eq 8"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("second"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSecondFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Second,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheSecondFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=second(BirthDate) eq 8"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheStartsWithFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheStartsWithFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=startswith(Surname, 'Hay') eq true"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("startswith"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheStartsWithFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.StartsWith,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheStartsWithFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=startswith(Surname, 'Hay') eq true"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSubstringFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheSubstringFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=substring(CompanyName, 1) eq 'lfreds Futterkiste'"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("substring"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSubstringFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Substring,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheSubstringFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=substring(CompanyName, 1) eq 'lfreds Futterkiste'"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSubtractOperatorSubtractItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.None,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            public WhenTheFilterQueryOptionContainsTheSubtractOperatorSubtractItIsNotSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price sub 100 eq 150"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedOperator.FormatWith("sub"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSubtractOperatorSubtractItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.Subtract,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            public WhenTheFilterQueryOptionContainsTheSubtractOperatorSubtractItIsSpecifiedInAllowedLogicalOperators()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Products?$filter=Price sub 100 eq 150"),
                    EntityDataModel.Current.EntitySets["Products"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheToLowerFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheToLowerFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=tolower(CompanyName) eq 'alfreds futterkiste'"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("tolower"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheToLowerFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.ToLower,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheToLowerFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=tolower(CompanyName) eq 'alfreds futterkiste'"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheToUpperFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheToUpperFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=toupper(CompanyName) eq 'ALFREDS FUTTERKISTE'"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("toupper"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheToUpperFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.ToUpper,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheToUpperFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=toupper(CompanyName) eq 'ALFREDS FUTTERKISTE'"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheTrimFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheTrimFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=trim(CompanyName) eq 'Alfreds Futterkiste'"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("trim"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheTrimFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Trim,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheTrimFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers?$filter=trim(CompanyName) eq 'Alfreds Futterkiste'"),
                    EntityDataModel.Current.EntitySets["Customers"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheYearFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            public WhenTheFilterQueryOptionContainsTheYearFunctionAndItIsNotSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=year(BirthDate) eq 1971"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedFunction.FormatWith("year"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheYearFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Year,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionContainsTheYearFunctionAndItIsSpecifiedInAllowedFunctions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=year(BirthDate) eq 1971"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }

        public class WhenTheFilterQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            public WhenTheFilterQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=Surname eq 'Smith'"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnHttpResponseExceptionExceptionIsThrownWithNotImplemented()
            {
                var exception = Assert.Throws<ODataException>(
                    () => FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(HttpStatusCode.NotImplemented, exception.StatusCode);
                Assert.Equal(Messages.UnsupportedQueryOption.FormatWith("$filter"), exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions;

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            public WhenTheFilterQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions()
            {
                TestHelper.EnsureEDM();

                this.queryOptions = new ODataQueryOptions(
                    new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Employees?$filter=Surname eq 'Smith'"),
                    EntityDataModel.Current.EntitySets["Employees"]);
            }

            [Fact]
            public void AnExceptionShouldNotBeThrown()
            {
                FilterQueryOptionValidator.Validate(this.queryOptions, this.validationSettings);
            }
        }
    }
}