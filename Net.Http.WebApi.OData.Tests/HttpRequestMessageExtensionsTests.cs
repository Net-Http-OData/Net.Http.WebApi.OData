using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using Moq;
using Net.Http.OData;
using Net.Http.OData.Model;
using Net.Http.OData.Query;
using Xunit;

namespace Net.Http.WebApi.OData.Tests
{
    public class HttpRequestMessageExtensionsTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void IsODataRequest_False()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri("http://services.odata.org/"));

            Assert.False(httpRequestMessage.IsODataRequest());
        }

        [Theory]
        [InlineData("http://services.odata.org/OData")]
        [InlineData("http://services.odata.org/OData/Products")]
        [Trait("Category", "Unit")]
        public void IsODataRequest_True(string uri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));

            Assert.True(httpRequestMessage.IsODataRequest());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void NextLink_WithAllQueryOptions()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "http://services.odata.org/OData/Products?$count=true&$expand=Category&$filter=Name eq 'Milk'&$format=json&$orderby=Name&$search=blue OR green&$select=Name,Price$top=25");

            var queryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                EntityDataModel.Current.EntitySets["Products"],
                Mock.Of<IODataQueryOptionsValidator>());

            Assert.Equal(
                "http://services.odata.org/OData/Products?$skip=75&$count=true&$expand=Category&$filter=Name eq 'Milk'&$format=json&$orderby=Name&$search=blue OR green&$select=Name,Price$top=25",
                httpRequestMessage.NextLink(queryOptions, 50, 25));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void NextLink_WithNoQueryOptions()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://services.odata.org/OData/Customers");

            var queryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                EntityDataModel.Current.EntitySets["Customers"],
                Mock.Of<IODataQueryOptionsValidator>());

            Assert.Equal(
                "http://services.odata.org/OData/Customers?$skip=75",
                httpRequestMessage.NextLink(queryOptions, 50, 25));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_ReturnsContext_IfMetadataIsFull()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Full, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext();

            Assert.Equal("http://services.odata.org/OData/$metadata", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_ReturnsContext_IfMetadataIsMinimal()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Minimal, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext();

            Assert.Equal("http://services.odata.org/OData/$metadata", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_ReturnsNull_IfMetadataIsNone()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.None, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext();

            Assert.Null(contextUri);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndEntityKey_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Full, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext<string>(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products/$entity", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndEntityKey_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Minimal, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext<string>(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products/$entity", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndEntityKey_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.None, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext<string>(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Null(contextUri);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Orders(12345)/Name"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Full, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Equal("http://services.odata.org/OData/$metadata#Orders(12345)/Name", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Orders(12345)/Name"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Minimal, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Equal("http://services.odata.org/OData/$metadata#Orders(12345)/Name", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Orders(12345)/Name"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.None, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Null(contextUri);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')/Name"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Full, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Equal("http://services.odata.org/OData/$metadata#Products('Milk')/Name", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')/Name"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Minimal, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Equal("http://services.odata.org/OData/$metadata#Products('Milk')/Name", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')/Name"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.None, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Null(contextUri);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Full, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Minimal, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.None, ODataVersion.OData40));

            string contextUri = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Null(contextUri);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySetAndSelectExpandQueryOptionAll_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/?$select=*"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Full, ODataVersion.OData40));
            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            var odataQueryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string contextUri = httpRequestMessage.ResolveODataContext(entitySet, odataQueryOptions.Select);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products(*)", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySetAndSelectExpandQueryOptionAll_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/?$select=*"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Minimal, ODataVersion.OData40));
            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            var odataQueryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string contextUri = httpRequestMessage.ResolveODataContext(entitySet, odataQueryOptions.Select);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products(*)", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySetAndSelectExpandQueryOptionAll_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/?$select=*"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.None, ODataVersion.OData40));
            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            var odataQueryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string contextUri = httpRequestMessage.ResolveODataContext(entitySet, odataQueryOptions.Select);

            Assert.Null(contextUri);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySetAndSelectExpandQueryOptionProperties_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/?$select=Name,Price"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Full, ODataVersion.OData40));
            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            var odataQueryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string contextUri = httpRequestMessage.ResolveODataContext(entitySet, odataQueryOptions.Select);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products(Name,Price)", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySetAndSelectExpandQueryOptionProperties_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/?$select=Name,Price"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.Minimal, ODataVersion.OData40));
            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            var odataQueryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string contextUri = httpRequestMessage.ResolveODataContext(entitySet, odataQueryOptions.Select);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products(Name,Price)", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySetAndSelectExpandQueryOptionProperties_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/?$select=Name,Price"));
            httpRequestMessage.Properties.Add(typeof(ODataRequestOptions).FullName, new ODataRequestOptions(new Uri("http://services.odata.org/OData/"), ODataIsolationLevel.None, ODataMetadataLevel.None, ODataVersion.OData40));
            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            var odataQueryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string contextUri = httpRequestMessage.ResolveODataContext(entitySet, odataQueryOptions.Select);

            Assert.Null(contextUri);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataId_WithIntEntityKey()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Orders"));

            string contextUri = httpRequestMessage.ResolveODataId(EntityDataModel.Current.EntitySets["Orders"], 12345);

            Assert.Equal("http://services.odata.org/OData/Orders(12345)", contextUri.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataId_WithStringEntityKey()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products"));

            string contextUri = httpRequestMessage.ResolveODataId(EntityDataModel.Current.EntitySets["Products"], "Milk");

            Assert.Equal("http://services.odata.org/OData/Products('Milk')", contextUri.ToString());
        }

        public class CreateODataErrorResponse_WithHttpStatusCode_AndMessage
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public CreateODataErrorResponse_WithHttpStatusCode_AndMessage()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$select=Foo"));
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                _httpResponseMessage = httpRequestMessage.CreateODataErrorResponse(HttpStatusCode.BadRequest, "Path segment not supported: 'Foo'.");
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheContentIsSet()
            {
                Assert.IsType<ObjectContent<ODataErrorContent>>(_httpResponseMessage.Content);
                Assert.IsType<ODataErrorContent>(((ObjectContent<ODataErrorContent>)_httpResponseMessage.Content).Value);

                var errorContent = (ODataErrorContent)((ObjectContent<ODataErrorContent>)_httpResponseMessage.Content).Value;

                Assert.NotNull(errorContent.Error);
                Assert.Equal("400", errorContent.Error.Code);
                Assert.Equal("Path segment not supported: 'Foo'.", errorContent.Error.Message);
                Assert.Null(errorContent.Error.Target);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheStatusCodeIsBadRequest()
            {
                Assert.Equal(HttpStatusCode.BadRequest, _httpResponseMessage.StatusCode);
            }
        }

        public class CreateODataErrorResponse_WithHttpStatusCode_Message_AndTarget
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public CreateODataErrorResponse_WithHttpStatusCode_Message_AndTarget()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$select=Foo"));
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                _httpResponseMessage = httpRequestMessage.CreateODataErrorResponse(HttpStatusCode.BadRequest, "Path segment not supported: 'Foo'.", "query");
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheContentIsSet()
            {
                Assert.IsType<ObjectContent<ODataErrorContent>>(_httpResponseMessage.Content);
                Assert.IsType<ODataErrorContent>(((ObjectContent<ODataErrorContent>)_httpResponseMessage.Content).Value);

                var errorContent = (ODataErrorContent)((ObjectContent<ODataErrorContent>)_httpResponseMessage.Content).Value;

                Assert.NotNull(errorContent.Error);
                Assert.Equal("400", errorContent.Error.Code);
                Assert.Equal("Path segment not supported: 'Foo'.", errorContent.Error.Message);
                Assert.Equal("query", errorContent.Error.Target);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheStatusCodeIsBadRequest()
            {
                Assert.Equal(HttpStatusCode.BadRequest, _httpResponseMessage.StatusCode);
            }
        }

        public class CreateODataErrorResponse_WithODataException
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public CreateODataErrorResponse_WithODataException()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$select=Foo"));
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                _httpResponseMessage = httpRequestMessage.CreateODataErrorResponse(new ODataException(HttpStatusCode.NotImplemented, "$search query option not supported.", "query"));
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheContentIsSet()
            {
                Assert.IsType<ObjectContent<ODataErrorContent>>(_httpResponseMessage.Content);
                Assert.IsType<ODataErrorContent>(((ObjectContent<ODataErrorContent>)_httpResponseMessage.Content).Value);

                var errorContent = (ODataErrorContent)((ObjectContent<ODataErrorContent>)_httpResponseMessage.Content).Value;

                Assert.NotNull(errorContent.Error);
                Assert.Equal("501", errorContent.Error.Code);
                Assert.Equal("$search query option not supported.", errorContent.Error.Message);
                Assert.Equal("query", errorContent.Error.Target);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheStatusCodeIsNotImplemented()
            {
                Assert.Equal(HttpStatusCode.NotImplemented, _httpResponseMessage.StatusCode);
            }
        }

        public class CreateODataResponse_String_WithNonNullValue
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public CreateODataResponse_String_WithNonNullValue()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products('Milk')/Code/$value"));

                _httpResponseMessage = httpRequestMessage.CreateODataResponse("MLK");
            }

            [Fact]
            [Trait("Category", "Unit")]
            public async Task TheContentIsSet()
            {
                Assert.Equal("MLK", await ((StringContent)_httpResponseMessage.Content).ReadAsStringAsync());
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheContentIsStringContent()
            {
                Assert.IsType<StringContent>(_httpResponseMessage.Content);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheContentTypeIsTextPlain()
            {
                Assert.Equal("text/plain", _httpResponseMessage.Content.Headers.ContentType.MediaType);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheMetadataLevelContentTypeParameterIsNotSet()
            {
                NameValueHeaderValue metadataParameter = _httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.Null(metadataParameter);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheStatusCodeIsOk()
            {
                Assert.Equal(HttpStatusCode.OK, _httpResponseMessage.StatusCode);
            }
        }

        public class CreateODataResponse_String_WithNullValue
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public CreateODataResponse_String_WithNullValue()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products('Milk')/Code/$value"));

                _httpResponseMessage = httpRequestMessage.CreateODataResponse(default(string));
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheContentIsNull()
            {
                Assert.Null(_httpResponseMessage.Content);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheStatusCodeIsNoContent()
            {
                Assert.Equal(HttpStatusCode.NoContent, _httpResponseMessage.StatusCode);
            }
        }
    }
}
