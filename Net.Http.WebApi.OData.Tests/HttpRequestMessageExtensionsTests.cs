using System.Net;
using System.Net.Http;
using Moq;
using Net.Http.OData;
using Net.Http.OData.Model;
using Net.Http.OData.Query;
using Xunit;

namespace Net.Http.WebApi.OData.Tests
{
    public class HttpRequestMessageExtensionsTests
    {
        [Theory]
        [InlineData("/OData")]
        [InlineData("/OData/Products")]
        [Trait("Category", "Unit")]
        public void IsODataMetadataRequest_False(string path)
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage(path);

            Assert.False(httpRequestMessage.IsODataMetadataRequest());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void IsODataMetadataRequest_True()
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/odata/$metadata");

            Assert.True(httpRequestMessage.IsODataMetadataRequest());
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/api")]
        [Trait("Category", "Unit")]
        public void IsODataRequest_False(string path)
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage(path);

            Assert.False(httpRequestMessage.IsODataRequest());
        }

        [Theory]
        [InlineData("/OData")]
        [InlineData("/OData/Products")]
        [Trait("Category", "Unit")]
        public void IsODataRequest_True(string path)
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage(path);

            Assert.True(httpRequestMessage.IsODataRequest());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void NextLink_WithAllQueryOptions()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage(
                "/OData/Products?$count=true&$expand=Category&$filter=Name eq 'Milk'&$format=json&$orderby=Name&$search=blue OR green&$select=Name,Price$top=25");

            ODataQueryOptions queryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                EntityDataModel.Current.EntitySets["Products"],
                Mock.Of<IODataQueryOptionsValidator>());

            Assert.Equal(
                "https://services.odata.org/OData/Products?$skip=75&$count=true&$expand=Category&$filter=Name eq 'Milk'&$format=json&$orderby=Name&$search=blue OR green&$select=Name,Price$top=25",
                httpRequestMessage.NextLink(queryOptions, 50, 25));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void NextLink_WithTopQueryOption()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products?$top=25");

            ODataQueryOptions queryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                EntityDataModel.Current.EntitySets["Products"],
                Mock.Of<IODataQueryOptionsValidator>());

            Assert.Equal(
                "https://services.odata.org/OData/Products?$skip=25&$top=25",
                httpRequestMessage.NextLink(queryOptions, 0, 25));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_ReturnsContext_IfMetadataIsFull()
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData", ODataMetadataLevel.Full);

            string odataContext = httpRequestMessage.ResolveODataContext();

            Assert.Equal("https://services.odata.org/OData/$metadata", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_ReturnsContext_IfMetadataIsMinimal()
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData", ODataMetadataLevel.Minimal);

            string odataContext = httpRequestMessage.ResolveODataContext();

            Assert.Equal("https://services.odata.org/OData/$metadata", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_ReturnsNull_IfMetadataIsNone()
        {
            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData", ODataMetadataLevel.None);

            string odataContext = httpRequestMessage.ResolveODataContext();

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndEntityKey_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products('Milk')", ODataMetadataLevel.Full);

            string odataContext = httpRequestMessage.ResolveODataContext<string>(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products/$entity", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndEntityKey_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products('Milk')", ODataMetadataLevel.Minimal);

            string odataContext = httpRequestMessage.ResolveODataContext<string>(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products/$entity", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndEntityKey_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products('Milk')", ODataMetadataLevel.None);

            string odataContext = httpRequestMessage.ResolveODataContext<string>(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Orders(12345)/Name", ODataMetadataLevel.Full);

            string odataContext = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Equal("https://services.odata.org/OData/$metadata#Orders(12345)/Name", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Orders(12345)/Name", ODataMetadataLevel.Minimal);

            string odataContext = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Equal("https://services.odata.org/OData/$metadata#Orders(12345)/Name", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Orders(12345)/Name", ODataMetadataLevel.None);

            string odataContext = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products('Milk')/Name", ODataMetadataLevel.Full);

            string odataContext = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Equal("https://services.odata.org/OData/$metadata#Products('Milk')/Name", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products('Milk')/Name", ODataMetadataLevel.Minimal);

            string odataContext = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Equal("https://services.odata.org/OData/$metadata#Products('Milk')/Name", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products('Milk')/Name", ODataMetadataLevel.None);

            string odataContext = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products", ODataMetadataLevel.Full);

            string odataContext = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products", ODataMetadataLevel.Minimal);

            string odataContext = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySet_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products", ODataMetadataLevel.None);

            string odataContext = httpRequestMessage.ResolveODataContext(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySetAndSelectExpandQueryOptionAll_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products?$select=*", ODataMetadataLevel.Full);

            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            ODataQueryOptions odataQueryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string odataContext = httpRequestMessage.ResolveODataContext(entitySet, odataQueryOptions.Select);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products(*)", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySetAndSelectExpandQueryOptionAll_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products?$select=*", ODataMetadataLevel.Minimal);

            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            ODataQueryOptions odataQueryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string odataContext = httpRequestMessage.ResolveODataContext(entitySet, odataQueryOptions.Select);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products(*)", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySetAndSelectExpandQueryOptionAll_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products?$select=*", ODataMetadataLevel.None);

            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            ODataQueryOptions odataQueryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string odataContext = httpRequestMessage.ResolveODataContext(entitySet, odataQueryOptions.Select);

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySetAndSelectExpandQueryOptionProperties_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products?$select=Name,Price", ODataMetadataLevel.Full);

            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            ODataQueryOptions odataQueryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string odataContext = httpRequestMessage.ResolveODataContext(entitySet, odataQueryOptions.Select);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products(Name,Price)", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySetAndSelectExpandQueryOptionProperties_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products?$select=Name,Price", ODataMetadataLevel.Minimal);

            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            ODataQueryOptions odataQueryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string odataContext = httpRequestMessage.ResolveODataContext(entitySet, odataQueryOptions.Select);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products(Name,Price)", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataContext_WithEntitySetAndSelectExpandQueryOptionProperties_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products?$select=Name,Price", ODataMetadataLevel.None);

            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            ODataQueryOptions odataQueryOptions = new ODataQueryOptions(
                httpRequestMessage.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string odataContext = httpRequestMessage.ResolveODataContext(entitySet, odataQueryOptions.Select);

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataId_WithIntEntityKey()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Orders");

            string odataContext = httpRequestMessage.ResolveODataId(EntityDataModel.Current.EntitySets["Orders"], 12345);

            Assert.Equal("https://services.odata.org/OData/Orders(12345)", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ResolveODataId_WithStringEntityKey()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products");

            string odataContext = httpRequestMessage.ResolveODataId(EntityDataModel.Current.EntitySets["Products"], "Milk");

            Assert.Equal("https://services.odata.org/OData/Products('Milk')", odataContext);
        }

        public class CreateODataErrorResponse_WithODataException
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public CreateODataErrorResponse_WithODataException()
            {
                HttpRequestMessage httpRequestMessage = TestHelper.CreateODataHttpRequestMessage("/OData/Products?$select=Foo");

                _httpResponseMessage = httpRequestMessage.CreateODataErrorResponse(new ODataException("$search query option not supported.", HttpStatusCode.NotImplemented, "query"));
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
            public void TheStatusCodeIsNotImplemented() => Assert.Equal(HttpStatusCode.NotImplemented, _httpResponseMessage.StatusCode);
        }
    }
}
