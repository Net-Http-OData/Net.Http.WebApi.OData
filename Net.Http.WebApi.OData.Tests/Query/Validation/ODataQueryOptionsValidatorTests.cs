namespace Net.Http.WebApi.Tests.OData.Query.Validation
{
    using System.Net.Http;
    using Net.Http.WebApi.OData;
    using Net.Http.WebApi.OData.Query;
    using Net.Http.WebApi.OData.Query.Validation;
    using Xunit;

    public class ODataQueryOptionsValidatorTests
    {
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