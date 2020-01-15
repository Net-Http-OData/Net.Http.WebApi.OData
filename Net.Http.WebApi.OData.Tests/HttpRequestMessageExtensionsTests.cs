using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using Net.Http.OData;
using Net.Http.OData.Model;
using Net.Http.OData.Query;
using Xunit;

namespace Net.Http.WebApi.OData.Tests
{
    public class HttpRequestMessageExtensionsTests
    {
        [Fact]
        public void ResolveODataContextUri_ReturnsNull_IfMetadataIsNone()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri();

            Assert.Null(contextUri);
        }

        [Fact]
        public void ResolveODataContextUri_ReturnsUri_IfMetadataIsFull()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri();

            Assert.Equal("http://services.odata.org/OData/$metadata", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_ReturnsUri_IfMetadataIsMinimal()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri();

            Assert.Equal("http://services.odata.org/OData/$metadata", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndEntityKey_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"], "Milk");

            Assert.Null(contextUri);
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndEntityKey_ReturnsUri_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"], "Milk");

            Assert.Equal("http://services.odata.org/OData/$metadata#Products/$entity", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndEntityKey_ReturnsUri_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"], "Milk");

            Assert.Equal("http://services.odata.org/OData/$metadata#Products/$entity", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Orders(12345)/Name"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Null(contextUri);
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsUri_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Orders(12345)/Name"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Equal("http://services.odata.org/OData/$metadata#Orders(12345)/Name", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndIntEntityKey_AndProperty_ReturnsUri_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Orders(12345)/Name"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Orders"], 12345, "Name");

            Assert.Equal("http://services.odata.org/OData/$metadata#Orders(12345)/Name", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')/Name"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Null(contextUri);
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsUri_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')/Name"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Equal("http://services.odata.org/OData/$metadata#Products('Milk')/Name", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_AndStringEntityKey_AndProperty_ReturnsUri_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products('Milk')/Name"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"], "Milk", "Name");

            Assert.Equal("http://services.odata.org/OData/$metadata#Products('Milk')/Name", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Null(contextUri);
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_ReturnsUri_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySet_ReturnsUri_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(EntityDataModel.Current.EntitySets["Products"]);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySetAndSelectExpandQueryOptionAll_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/?$select=*"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");
            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            var odataQueryOptions = new ODataQueryOptions(httpRequestMessage, entitySet);

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(entitySet, odataQueryOptions.Select);

            Assert.Null(contextUri);
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySetAndSelectExpandQueryOptionAll_ReturnsUri_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/?$select=*"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");
            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            var odataQueryOptions = new ODataQueryOptions(httpRequestMessage, entitySet);

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(entitySet, odataQueryOptions.Select);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products(*)", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySetAndSelectExpandQueryOptionAll_ReturnsUri_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/?$select=*"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");
            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            var odataQueryOptions = new ODataQueryOptions(httpRequestMessage, entitySet);

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(entitySet, odataQueryOptions.Select);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products(*)", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySetAndSelectExpandQueryOptionProperties_ReturnsNull_IfMetadataIsNone()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/?$select=Name,Price"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");
            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            var odataQueryOptions = new ODataQueryOptions(httpRequestMessage, entitySet);

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(entitySet, odataQueryOptions.Select);

            Assert.Null(contextUri);
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySetAndSelectExpandQueryOptionProperties_ReturnsUri_IfMetadataIsFull()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/?$select=Name,Price"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");
            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            var odataQueryOptions = new ODataQueryOptions(httpRequestMessage, entitySet);

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(entitySet, odataQueryOptions.Select);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products(Name,Price)", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataContextUri_WithEntitySetAndSelectExpandQueryOptionProperties_ReturnsUri_IfMetadataIsMinimal()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products/?$select=Name,Price"));
            httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");
            EntitySet entitySet = EntityDataModel.Current.EntitySets["Products"];

            var odataQueryOptions = new ODataQueryOptions(httpRequestMessage, entitySet);

            Uri contextUri = httpRequestMessage.ResolveODataContextUri(entitySet, odataQueryOptions.Select);

            Assert.Equal("http://services.odata.org/OData/$metadata#Products(Name,Price)", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataEntityUri_WithIntEntityKey()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Orders"));

            Uri contextUri = httpRequestMessage.ResolveODataEntityUri(EntityDataModel.Current.EntitySets["Orders"], 12345);

            Assert.Equal("http://services.odata.org/OData/Orders(12345)", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataEntityUri_WithStringEntityKey()
        {
            TestHelper.EnsureEDM();

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri("http://services.odata.org/OData/Products"));

            Uri contextUri = httpRequestMessage.ResolveODataEntityUri(EntityDataModel.Current.EntitySets["Products"], "Milk");

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
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataHeaderValues.ODataVersionString, _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
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
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataHeaderValues.ODataVersionString, _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
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
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataHeaderValues.ODataVersionString, _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
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
            public async Task TheContentIsSet()
            {
                Assert.Equal("MLK", await ((StringContent)_httpResponseMessage.Content).ReadAsStringAsync());
            }

            [Fact]
            public void TheContentIsStringContent()
            {
                Assert.IsType<StringContent>(_httpResponseMessage.Content);
            }

            [Fact]
            public void TheContentTypeIsTextPlain()
            {
                Assert.Equal("text/plain", _httpResponseMessage.Content.Headers.ContentType.MediaType);
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataHeaderValues.ODataVersionString, _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsNotSet()
            {
                NameValueHeaderValue metadataParameter = _httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.Null(metadataParameter);
            }

            [Fact]
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
            public void TheContentIsNull()
            {
                Assert.Null(_httpResponseMessage.Content);
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataHeaderValues.ODataVersionString, _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheStatusCodeIsNoContent()
            {
                Assert.Equal(HttpStatusCode.NoContent, _httpResponseMessage.StatusCode);
            }
        }

        public class CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataFull
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataFull()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$filter=Price eq 21.39M"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                _httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataHeaderValues.ODataVersionString, _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                NameValueHeaderValue metadataParameter = _httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("full", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataMinimal
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataMinimal()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$filter=Price eq 21.39M"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                _httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataHeaderValues.ODataVersionString, _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                NameValueHeaderValue metadataParameter = _httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("minimal", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataNone
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public CreateODataResponse_T_WithAcceptHeaderContainingODataMetadataNone()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$filter=Price eq 21.39M"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                _httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataHeaderValues.ODataVersionString, _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                NameValueHeaderValue metadataParameter = _httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("none", metadataParameter.Value);
            }
        }

        public class CreateODataResponse_T_WithoutMetadataLevelSpecifiedInRequest
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public CreateODataResponse_T_WithoutMetadataLevelSpecifiedInRequest()
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products?$filter=Price eq 21.39M"));
                httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

                _httpResponseMessage = httpRequestMessage.CreateODataResponse(HttpStatusCode.OK, new ODataResponseContent(null, new object[0]));
            }

            [Fact]
            public void TheDataServiceVersionHeaderIsSet()
            {
                Assert.True(_httpResponseMessage.Headers.Contains(ODataHeaderNames.ODataVersion));
                Assert.Equal(ODataHeaderValues.ODataVersionString, _httpResponseMessage.Headers.GetValues(ODataHeaderNames.ODataVersion).Single());
            }

            [Fact]
            public void TheMetadataLevelContentTypeParameterIsSet()
            {
                NameValueHeaderValue metadataParameter = _httpResponseMessage.Content.Headers.ContentType.Parameters.SingleOrDefault(x => x.Name == ODataMetadataLevelExtensions.HeaderName);

                Assert.NotNull(metadataParameter);
                Assert.Equal("minimal", metadataParameter.Value);
            }
        }

        public class ReadODataRequestOptions
        {
            [Fact]
            public void CachesInRequestProperties()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

                ODataRequestOptions requestOptions1 = httpRequestMessage.ReadODataRequestOptions();
                ODataRequestOptions requestOptions2 = httpRequestMessage.ReadODataRequestOptions();

                Assert.Same(requestOptions1, requestOptions2);

                Assert.Same(requestOptions1, httpRequestMessage.Properties[typeof(ODataRequestOptions).FullName]);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContaininAnInvalidODataMetadataValue
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrown()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=all");

                ODataException exception = Assert.Throws<ODataException>(() => httpRequestMessage.ReadODataRequestOptions());

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("If specified, the odata.metadata value in the Accept header must be 'none', 'minimal' or 'full'", exception.Message);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContainingODataMetadataFull
        {
            [Fact]
            public void TheMetadataLevelIsSetToVerbose()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=full");

                ODataRequestOptions requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(ODataMetadataLevel.Full, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContainingODataMetadataMinimal
        {
            [Fact]
            public void TheMetadataLevelIsSetToMinimal()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=minimal");

                ODataRequestOptions requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(ODataMetadataLevel.Minimal, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptions_WithAcceptHeaderContainingODataMetadataNone
        {
            [Fact]
            public void TheMetadataLevelIsSetToNone()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add("Accept", "application/json;odata.metadata=none");

                ODataRequestOptions requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(ODataMetadataLevel.None, requestOptions.MetadataLevel);
            }
        }

        public class ReadODataRequestOptions_WithODataIsolationHeaderContainingSnapshot
        {
            [Fact]
            public void TheIsolationLevelIsSet()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataIsolation, "Snapshot");

                ODataRequestOptions requestOptions = httpRequestMessage.ReadODataRequestOptions();

                Assert.Equal(ODataIsolationLevel.Snapshot, requestOptions.IsolationLevel);
            }
        }

        public class ReadODataRequestOptions_WithODataIsolationHeaderNotContainingSnapshot
        {
            [Fact]
            public void AnHttpResponseExceptionIsThrownWhenReadingTheIsolationLevel()
            {
                TestHelper.EnsureEDM();

                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Get,
                    new Uri("http://services.odata.org/OData/Products"));
                httpRequestMessage.Headers.Add(ODataHeaderNames.ODataIsolation, "ReadCommitted");

                ODataException exception = Assert.Throws<ODataException>(() => httpRequestMessage.ReadODataRequestOptions());

                Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
                Assert.Equal("If specified, the OData-IsolationLevel must be 'Snapshot'", exception.Message);
            }
        }
    }
}
