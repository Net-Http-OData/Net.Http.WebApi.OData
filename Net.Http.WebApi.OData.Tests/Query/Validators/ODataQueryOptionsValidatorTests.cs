﻿namespace Net.Http.WebApi.Tests.OData.Query.Validators
{
    using System.Net.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Validators;
    using Xunit;

    public class ODataQueryOptionsValidatorTests
    {
        public class WhenTheExpandQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$expand=Orders"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.ExpandQueryOptionNotSupported, exception.Message);
            }
        }

        public class WhenTheExpandQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$expand=Orders"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Expand
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheAddOperatorAddItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Price add 100 eq 150"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.None,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.AddOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheAddOperatorAddItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Price add 100 eq 150"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.Add,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheAndOperatorAndItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Forename eq 'John' and Surname eq 'Smith'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.AndOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheAndOperatorAndItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Forename eq 'John' and Surname eq 'Smith'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.And | AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheCastAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=cast(Age, 'Edm.Int64') eq 20"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.CastFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheCastFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=cast(Age, 'Edm.Int64') eq 20"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Cast,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheCeilingFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=ceiling(Freight) eq 32"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.CeilingFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheCeilingFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=ceiling(Freight) eq 32"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Ceiling,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheConcatAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=concat(concat(City, ', '), Country) eq 'Berlin, Germany'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.ConcatFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheConcatFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=concat(concat(City, ', '), Country) eq 'Berlin, Germany'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Concat,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheDayFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=day(BirthDate) eq 8"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.DayFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheDayFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=day(BirthDate) eq 8"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Day,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheDivideOperatorDivideItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Price div 100 eq 150"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.None,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.DivideOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheDivideOperatorDivideItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Price div 100 eq 150"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.Divide,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheEndsWithFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=endswith(Name, 'yes') eq true"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.EndsWithFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheEndsWithFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=endswith(Name, 'yes') eq true"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.EndsWith,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheEqualsOperatorEqualsItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Forename eq 'John'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.EqualsOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheEqualsOperatorEqualsItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Forename eq 'John'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheFloorFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=floor(Freight) eq 32"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.FloorFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheFloorFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=floor(Freight) eq 32"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Floor,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheGreaterThanOperatorGreaterThanItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Age gt 35"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.GreaterThanOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheGreaterThanOperatorGreaterThanItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Age gt 35"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.GreaterThan
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheGreaterThanOrEqualsOperatorGreaterThanOrEqualsItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Age ge 35"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.GreaterThanOrEqualsOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheGreaterThanOrEqualsOperatorGreaterThanOrEqualsItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Age ge 35"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.GreaterThanOrEqual
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheHourFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=hour(BirthDate) eq 8"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.HourFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheHourFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=hour(BirthDate) eq 8"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Hour,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheIndexOfFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=indexof(Name, 'Hayes') eq 1"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.IndexOfFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheIndexOfFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=indexof(Name, 'Hayes') eq 1"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.IndexOf,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheIsOfAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=isof(Age, 'Edm.Int64')"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.IsOfFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheIsOfFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=isof(Age, 'Edm.Int64')"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.IsOf,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheLengthFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=length(CompanyName) eq 19"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.LengthFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheLengthFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=length(CompanyName) eq 19"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Length,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheLessThanOperatorLessThanItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Age lt 35"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.LessThanOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheLessThanOperatorLessThanItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Age lt 35"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.LessThan
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheLessThanOrEqualsOperatorLessThanOrEqualsItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Age le 35"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.LessThanOrEqualsOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheLessThanOrEqualsOperatorLessThanOrEqualsItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Age le 35"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.LessThanOrEqual
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMinuteFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=minute(BirthDate) eq 8"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.MinuteFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMinuteFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=minute(BirthDate) eq 8"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Minute,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheModuloOperatorModuloItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Price mod 100 eq 150"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.None,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.ModuloOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheModuloOperatorModuloItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Price mod 100 eq 150"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.Modulo,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMonthFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=month(BirthDate) eq 5"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.MonthFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMonthFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=month(BirthDate) eq 5"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Month,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMultiplyOperatorMultiplyItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Price mul 100 eq 150"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.None,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.MultiplyOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheMultiplyOperatorMultiplyItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Price mul 100 eq 150"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.Multiply,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheNotEqualsOperatorNotEqualsItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Forename ne 'John'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.NotEqualsOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheNotEqualsOperatorNotEqualsItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Forename ne 'John'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.NotEqual
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheOrOperatorOrItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Forename eq 'John' or Surname eq 'Smith'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.OrOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheOrOperatorOrItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Forename eq 'John' or Surname eq 'Smith'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.Or | AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheReplaceFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=replace(CompanyName, ' ', '') eq 'AlfredsFutterkiste'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.ReplaceFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheReplaceFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=replace(CompanyName, ' ', '') eq 'AlfredsFutterkiste'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Replace,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheRoundFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=round(Freight) eq 32"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.RoundFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheRoundFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=round(Freight) eq 32"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Round,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSecondFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=second(BirthDate) eq 8"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.SecondFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSecondFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=second(BirthDate) eq 8"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Second,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheStartsWithFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=startswith(Name, 'Hay') eq true"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.StartsWithFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheStartsWithFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=startswith(Name, 'Hay') eq true"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.StartsWith,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSubstringFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=substring(CompanyName, 1) eq 'lfreds Futterkiste'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.SubstringFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSubstringFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=substring(CompanyName, 1) eq 'lfreds Futterkiste'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Substring,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSubstringOfFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=substringof('Hayes', Name) eq true"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.SubstringOfFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSubstringOfFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=substringof('Hayes', Name) eq true"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.SubstringOf,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSubtractOperatorSubtractItIsNotSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Price sub 100 eq 150"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.None,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.SubtractOperatorNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheSubtractOperatorSubtractItIsSpecifiedInAllowedLogicalOperators
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Price sub 100 eq 150"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.Subtract,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal,
                AllowedQueryOptions = AllowedQueryOptions.Filter
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheToLowerFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=tolower(CompanyName) eq 'alfreds futterkiste'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.ToLowerFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheToLowerFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=tolower(CompanyName) eq 'alfreds futterkiste'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.ToLower,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheToUpperFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=toupper(CompanyName) eq 'ALFREDS FUTTERKISTE'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.ToUpperFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheToUpperFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=toupper(CompanyName) eq 'ALFREDS FUTTERKISTE'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.ToUpper,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheTrimFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=trim(CompanyName) eq 'Alfreds Futterkiste'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.TrimFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheTrimFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=trim(CompanyName) eq 'Alfreds Futterkiste'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Trim,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionContainsTheYearFunctionAndItIsNotSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=year(BirthDate) eq 1971"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.YearFunctionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionContainsTheYearFunctionAndItIsSpecifiedInAllowedFunctions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=year(BirthDate) eq 1971"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedFunctions = AllowedFunctions.Year,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFilterQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Name eq 'Smith'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.FilterQueryOptionNotSupported, exception.Message);
            }
        }

        public class WhenTheFilterQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$filter=Name eq 'Smith'"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Filter,
                AllowedLogicalOperators = AllowedLogicalOperators.Equal
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheFormatQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$format=xml"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.FormatQueryOptionNotSupported, exception.Message);
            }
        }

        public class WhenTheFormatQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$format=xml"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Format
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheInlineCountQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$inlinecount=allpages"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.InlineCountQueryOptionNotSupported, exception.Message);
            }
        }

        public class WhenTheInlineCountQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$inlinecount=allpages"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.InlineCount
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheOrderByQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$orderby=Name desc"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.OrderByQueryOptionNotSupported, exception.Message);
            }
        }

        public class WhenTheOrderByQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$orderby=Name desc"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.OrderBy
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheSelectQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$select=Name"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.SelectQueryOptionNotSupported, exception.Message);
            }
        }

        public class WhenTheSelectQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$select=Name"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Select
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheSkipQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$skip=50"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.SkipQueryOptionNotSupported, exception.Message);
            }
        }

        public class WhenTheSkipQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$skip=50"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Skip
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }

        public class WhenTheTopQueryOptionIsSetAndItIsNotSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$top=50"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.None
            };

            [Fact]
            public void AnODataExceptionIsThrown()
            {
                var exception = Assert.Throws<ODataException>(
                    () => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));

                Assert.Equal(Messages.TopQueryOptionNotSupported, exception.Message);
            }
        }

        public class WhenTheTopQueryOptionIsSetAndItIsSpecifiedInAllowedQueryOptions
        {
            private readonly ODataQueryOptions queryOptions = new ODataQueryOptions(
                new HttpRequestMessage(HttpMethod.Get, "http://localhost/api?$top=50"));

            private readonly ODataValidationSettings validationSettings = new ODataValidationSettings
            {
                AllowedQueryOptions = AllowedQueryOptions.Top
            };

            [Fact]
            public void AnODataExceptionIsNotThrown()
            {
                Assert.DoesNotThrow(() => ODataQueryOptionsValidator.Validate(this.queryOptions, this.validationSettings));
            }
        }
    }
}