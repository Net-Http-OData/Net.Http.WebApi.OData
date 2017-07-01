namespace Net.Http.WebApi.OData.Tests
{
    using System;
    using Xunit;

    public class UriExtensionsTests
    {
        [Fact]
        public void ResolveEntitySetName()
        {
            var requestUri = new Uri("http://services.odata.org/OData/Products");

            Assert.Equal("Products", requestUri.ResolveEntitySetName());
        }

        [Fact]
        public void ResolveEntitySetName_WithEntityKey()
        {
            var requestUri = new Uri("http://services.odata.org/OData/Products(1)");

            Assert.Equal("Products", requestUri.ResolveEntitySetName());
        }

        [Fact]
        public void ResolveEntitySetName_WithTrailingSlash()
        {
            var requestUri = new Uri("http://services.odata.org/OData/Products/");

            Assert.Equal("Products", requestUri.ResolveEntitySetName());
        }

        [Fact]
        public void ResolveODataContextUri()
        {
            var requestUri = new Uri("http://services.odata.org/OData");
            var contextUri = requestUri.ResolveODataContextUri();

            Assert.Equal("http://services.odata.org/OData/$metadata", contextUri.ToString());
        }

        [Fact]
        public void ResolveODataServiceUri()
        {
            var requestUri = new Uri("http://services.odata.org/OData/Products");

            Assert.Equal("http://services.odata.org/OData/", requestUri.ResolveODataServiceUri().ToString());
        }

        [Fact]
        public void ResolveODataServiceUri_WithEntityKey()
        {
            var requestUri = new Uri("http://services.odata.org/OData/Products(1)");

            Assert.Equal("http://services.odata.org/OData/", requestUri.ResolveODataServiceUri().ToString());
        }

        [Fact]
        public void ResolveODataServiceUri_WithTrailingSlash()
        {
            var requestUri = new Uri("http://services.odata.org/OData/Products/");

            Assert.Equal("http://services.odata.org/OData/", requestUri.ResolveODataServiceUri().ToString());
        }
    }
}