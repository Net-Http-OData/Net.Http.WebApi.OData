namespace Net.Http.WebApi.Tests.OData.Query.Validation
{
    using System.Net.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Validation;
    using Xunit;

    public class ODataQueryOptionsValidatorTests
    {
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
                AllowedFunctions = AllowedFunctions.Ceiling
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
                AllowedFunctions = AllowedFunctions.Day
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
                AllowedFunctions = AllowedFunctions.EndsWith
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
                AllowedFunctions = AllowedFunctions.Floor
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
                AllowedFunctions = AllowedFunctions.Hour
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
                AllowedFunctions = AllowedFunctions.IndexOf
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
                AllowedFunctions = AllowedFunctions.Length
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
                AllowedFunctions = AllowedFunctions.Minute
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
                AllowedFunctions = AllowedFunctions.Month
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
                AllowedFunctions = AllowedFunctions.Replace
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
                AllowedFunctions = AllowedFunctions.Round
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
                AllowedFunctions = AllowedFunctions.Second
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
                AllowedFunctions = AllowedFunctions.StartsWith
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
                AllowedFunctions = AllowedFunctions.Substring
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
                AllowedFunctions = AllowedFunctions.SubstringOf
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
                AllowedFunctions = AllowedFunctions.ToLower
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
                AllowedFunctions = AllowedFunctions.ToUpper
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
                AllowedFunctions = AllowedFunctions.Trim
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
                AllowedFunctions = AllowedFunctions.Year
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
                AllowedQueryOptions = AllowedQueryOptions.Filter
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