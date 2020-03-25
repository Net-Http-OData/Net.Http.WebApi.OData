using System.Net;
using System.Net.Http;
using Moq;
using Net.Http.OData;
using Net.Http.OData.Model;
using Net.Http.OData.Query;
using Xunit;

namespace Net.Http.WebApi.OData.Tests
{
    public class ODataHttpRequestMessageExtensionsTests
    {
        [Theory]
        [InlineData("/OData")]
        [InlineData("/OData/Products")]
        [Trait("Category", "Unit")]
        public void IsODataMetadataRequest_False(string path)
        {
            HttpRequestMessage request = TestHelper.CreateODataHttpRequest(path);

            Assert.False(request.IsODataMetadataRequest());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void IsODataMetadataRequest_True()
        {
            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/odata/$metadata");

            Assert.True(request.IsODataMetadataRequest());
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/api")]
        [Trait("Category", "Unit")]
        public void IsODataRequest_False(string path)
        {
            HttpRequestMessage request = TestHelper.CreateODataHttpRequest(path);

            Assert.False(request.IsODataRequest());
        }

        [Theory]
        [InlineData("/OData")]
        [InlineData("/OData/Products")]
        [Trait("Category", "Unit")]
        public void IsODataRequest_True(string path)
        {
            HttpRequestMessage request = TestHelper.CreateODataHttpRequest(path);

            Assert.True(request.IsODataRequest());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_ReturnsContext_IfMetadataIsFull()
        {
            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData", ODataMetadataLevel.Full);

            string odataContext = request.ODataContext();

            Assert.Equal("https://services.odata.org/OData/$metadata", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_ReturnsContext_IfMetadataIsMinimal()
        {
            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData", ODataMetadataLevel.Minimal);

            string odataContext = request.ODataContext();

            Assert.Equal("https://services.odata.org/OData/$metadata", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_ReturnsNull_IfMetadataIsNone()
        {
            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData", ODataMetadataLevel.None);

            string odataContext = request.ODataContext();

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySet_AndEntityKey_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products('Milk')", ODataMetadataLevel.Full);

            string odataContext = request.ODataContext<string>(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products/$entity", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySet_AndEntityKey_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products('Milk')", ODataMetadataLevel.Minimal);

            string odataContext = request.ODataContext<string>(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products/$entity", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySet_AndEntityKey_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products('Milk')", ODataMetadataLevel.None);

            string odataContext = request.ODataContext<string>(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Orders(12345)/Name", ODataMetadataLevel.Full);

            string odataContext = request.ODataContext(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Equal("https://services.odata.org/OData/$metadata#Orders(12345)/Name", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Orders(12345)/Name", ODataMetadataLevel.Minimal);

            string odataContext = request.ODataContext(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Equal("https://services.odata.org/OData/$metadata#Orders(12345)/Name", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Orders(12345)/Name", ODataMetadataLevel.None);

            string odataContext = request.ODataContext(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products('Milk')/Name", ODataMetadataLevel.Full);

            string odataContext = request.ODataContext(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Equal("https://services.odata.org/OData/$metadata#Products('Milk')/Name", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products('Milk')/Name", ODataMetadataLevel.Minimal);

            string odataContext = request.ODataContext(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Equal("https://services.odata.org/OData/$metadata#Products('Milk')/Name", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products('Milk')/Name", ODataMetadataLevel.None);

            string odataContext = request.ODataContext(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySet_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products", ODataMetadataLevel.Full);

            string odataContext = request.ODataContext(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySet_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products", ODataMetadataLevel.Minimal);

            string odataContext = request.ODataContext(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySet_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products", ODataMetadataLevel.None);

            string odataContext = request.ODataContext(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySetAndSelectExpandQueryOptionAll_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products?$select=*", ODataMetadataLevel.Full);

            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            ODataQueryOptions odataQueryOptions = new ODataQueryOptions(
                request.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string odataContext = request.ODataContext(entitySet, odataQueryOptions.Select);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products(*)", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySetAndSelectExpandQueryOptionAll_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products?$select=*", ODataMetadataLevel.Minimal);

            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            ODataQueryOptions odataQueryOptions = new ODataQueryOptions(
                request.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string odataContext = request.ODataContext(entitySet, odataQueryOptions.Select);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products(*)", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySetAndSelectExpandQueryOptionAll_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products?$select=*", ODataMetadataLevel.None);

            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            ODataQueryOptions odataQueryOptions = new ODataQueryOptions(
                request.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string odataContext = request.ODataContext(entitySet, odataQueryOptions.Select);

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySetAndSelectExpandQueryOptionProperties_ReturnsContext_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products?$select=Name,Price", ODataMetadataLevel.Full);

            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            ODataQueryOptions odataQueryOptions = new ODataQueryOptions(
                request.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string odataContext = request.ODataContext(entitySet, odataQueryOptions.Select);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products(Name,Price)", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySetAndSelectExpandQueryOptionProperties_ReturnsContext_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products?$select=Name,Price", ODataMetadataLevel.Minimal);

            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            ODataQueryOptions odataQueryOptions = new ODataQueryOptions(
                request.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string odataContext = request.ODataContext(entitySet, odataQueryOptions.Select);

            Assert.Equal("https://services.odata.org/OData/$metadata#Products(Name,Price)", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataContext_WithEntitySetAndSelectExpandQueryOptionProperties_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products?$select=Name,Price", ODataMetadataLevel.None);

            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            ODataQueryOptions odataQueryOptions = new ODataQueryOptions(
                request.RequestUri.Query,
                entitySet,
                Mock.Of<IODataQueryOptionsValidator>());

            string odataContext = request.ODataContext(entitySet, odataQueryOptions.Select);

            Assert.Null(odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataId_WithIntEntityKey()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Orders");

            string odataContext = request.ODataId(EntityDataModel.Current.EntitySets["Orders"], 12345);

            Assert.Equal("https://services.odata.org/OData/Orders(12345)", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataId_WithStringEntityKey()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products");

            string odataContext = request.ODataId(EntityDataModel.Current.EntitySets["Products"], "Milk");

            Assert.Equal("https://services.odata.org/OData/Products('Milk')", odataContext);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataNextLink_WithAllQueryOptions()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest(
                "/OData/Products?$count=true&$expand=Category&$filter=Name eq 'Milk'&$format=json&$orderby=Name&$search=blue OR green&$select=Name,Price&$top=25");

            ODataQueryOptions queryOptions = new ODataQueryOptions(
                request.RequestUri.Query,
                EntityDataModel.Current.EntitySets["Products"],
                Mock.Of<IODataQueryOptionsValidator>());

            Assert.Equal(
                "https://services.odata.org/OData/Products?$skip=75&$count=true&$expand=Category&$filter=Name eq 'Milk'&$format=json&$orderby=Name&$search=blue OR green&$select=Name,Price&$top=25",
                request.ODataNextLink(queryOptions, 50, 25));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ODataNextLink_WithTopQueryOption()
        {
            TestHelper.EnsureEDM();

            HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products?$top=25");

            ODataQueryOptions queryOptions = new ODataQueryOptions(
                request.RequestUri.Query,
                EntityDataModel.Current.EntitySets["Products"],
                Mock.Of<IODataQueryOptionsValidator>());

            Assert.Equal(
                "https://services.odata.org/OData/Products?$skip=25&$top=25",
                request.ODataNextLink(queryOptions, 0, 25));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadIsolationLevel_None()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");

            Assert.Equal(ODataIsolationLevel.None, request.ReadIsolationLevel());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadIsolationLevel_Snapshot()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Add(ODataRequestHeaderNames.ODataIsolation, "Snapshot");

            Assert.Equal(ODataIsolationLevel.Snapshot, request.ReadIsolationLevel());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadIsolationLevel_Throws_ODataException_ForInvalidIsolationLevel()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Add(ODataRequestHeaderNames.ODataIsolation, "ReadCommitted");

            ODataException exception = Assert.Throws<ODataException>(() => request.ReadIsolationLevel());

            Assert.Equal($"If specified, the {ODataRequestHeaderNames.ODataIsolation} must be 'Snapshot'.", exception.Message);
            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadMetadataLevel_FavoursFormatOverAccept()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products?$format=json;odata.metadata=none");
            request.Headers.Accept.Clear();
            request.Headers.Add("Accept", "application/json;odata.metadata=full");

            Assert.Equal(ODataMetadataLevel.None, request.ReadMetadataLevel());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadMetadataLevel_FromAccept_Full()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Accept.Clear();
            request.Headers.Add("Accept", "application/json;odata.metadata=full");

            Assert.Equal(ODataMetadataLevel.Full, request.ReadMetadataLevel());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadMetadataLevel_FromAccept_Minimal()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Accept.Clear();
            request.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            Assert.Equal(ODataMetadataLevel.Minimal, request.ReadMetadataLevel());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadMetadataLevel_FromAccept_None()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Accept.Clear();
            request.Headers.Add("Accept", "application/json;odata.metadata=none");

            Assert.Equal(ODataMetadataLevel.None, request.ReadMetadataLevel());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadMetadataLevel_FromAccept_Throws_ODataException_ForInvalidMetadataLevel()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Accept.Clear();
            request.Headers.Add("Accept", "application/json;odata.metadata=all");

            ODataException exception = Assert.Throws<ODataException>(() => request.ReadMetadataLevel());

            Assert.Equal($"If specified, the {ODataMetadataLevelExtensions.HeaderName} value in the Accept header must be 'none', 'minimal' or 'full'.", exception.Message);
            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadMetadataLevel_FromFormat_Full()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products?$format=json;odata.metadata=full");

            Assert.Equal(ODataMetadataLevel.Full, request.ReadMetadataLevel());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadMetadataLevel_FromFormat_Minimal()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products?$format=json;odata.metadata=minimal");

            Assert.Equal(ODataMetadataLevel.Minimal, request.ReadMetadataLevel());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadMetadataLevel_FromFormat_None()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products?$format=json;odata.metadata=none");

            Assert.Equal(ODataMetadataLevel.None, request.ReadMetadataLevel());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadMetadataLevel_FromFormat_Throws_ODataException_ForInvalidMetadataLevel()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products?$format=json;odata.metadata=all");

            ODataException exception = Assert.Throws<ODataException>(() => request.ReadMetadataLevel());

            Assert.Equal($"If specified, the {ODataMetadataLevelExtensions.HeaderName} value in the $format query option must be 'none', 'minimal' or 'full'.", exception.Message);
            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadODataMaxVersion_ReturnsODataVersion_FromODataMaxVersionHeader()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Add(ODataRequestHeaderNames.ODataMaxVersion, "3.0");

            Assert.Equal(ODataVersion.Parse("3.0"), request.ReadODataMaxVersion());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadODataMaxVersion_ReturnsODataVersionMaxVersion_IfODataMaxVersionHeaderNotPresent()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");

            Assert.Equal(ODataVersion.MaxVersion, request.ReadODataMaxVersion());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadODataMaxVersion_Throws_ODataException_ForInvalidVersionFormat()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Add(ODataRequestHeaderNames.ODataMaxVersion, "MAX");

            ODataException exception = Assert.Throws<ODataException>(() => request.ReadODataMaxVersion());

            Assert.Equal($"If specified, the {ODataRequestHeaderNames.ODataMaxVersion} header must be a valid OData version.", exception.Message);
            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadODataVersion_ReturnsODataVersion_FromODataMaxVersionHeader_IfODataVersionHeaderNotPresent()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Add(ODataRequestHeaderNames.ODataMaxVersion, "3.0");

            Assert.Equal(ODataVersion.Parse("3.0"), request.ReadODataVersion());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadODataVersion_ReturnsODataVersion_FromODataVersionHeader()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Add(ODataRequestHeaderNames.ODataVersion, "3.0");

            Assert.Equal(ODataVersion.Parse("3.0"), request.ReadODataVersion());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ReadODataVersion_Throws_ODataException_ForInvalidVersionFormat()
        {
            HttpRequestMessage request = TestHelper.CreateHttpRequest("/OData/Products");
            request.Headers.Add(ODataRequestHeaderNames.ODataMaxVersion, "MIN");

            ODataException exception = Assert.Throws<ODataException>(() => request.ReadODataVersion());

            Assert.Equal($"If specified, the {ODataRequestHeaderNames.ODataMaxVersion} header must be a valid OData version.", exception.Message);
            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        }

        public class CreateODataErrorResponse_WithODataException
        {
            private readonly HttpResponseMessage _response;

            public CreateODataErrorResponse_WithODataException()
            {
                HttpRequestMessage request = TestHelper.CreateODataHttpRequest("/OData/Products?$select=Foo");

                _response = request.CreateODataErrorResponse(new ODataException("$search query option not supported.", HttpStatusCode.NotImplemented, "query"));
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheContentIsSet()
            {
                Assert.IsType<ObjectContent<ODataErrorContent>>(_response.Content);
                Assert.IsType<ODataErrorContent>(((ObjectContent<ODataErrorContent>)_response.Content).Value);

                var errorContent = (ODataErrorContent)((ObjectContent<ODataErrorContent>)_response.Content).Value;

                Assert.NotNull(errorContent.Error);
                Assert.Equal("501", errorContent.Error.Code);
                Assert.Equal("$search query option not supported.", errorContent.Error.Message);
                Assert.Equal("query", errorContent.Error.Target);
            }

            [Fact]
            [Trait("Category", "Unit")]
            public void TheStatusCodeIsNotImplemented() => Assert.Equal(HttpStatusCode.NotImplemented, _response.StatusCode);
        }
    }
}
