namespace Net.Http.WebApi.OData.Tests
{
    using System;
    using Xunit;

    public class UriExtensionsTests
    {
        [Fact]
        public void GetModelName()
        {
            var requestUri = new Uri("http://services.odata.org/OData/Products");

            Assert.Equal("Products", requestUri.GetModelName());
        }

        [Fact]
        public void GetModelNameWithEntityKey()
        {
            var requestUri = new Uri("http://services.odata.org/OData/Products(1)");

            Assert.Equal("Products", requestUri.GetModelName());
        }

        [Fact]
        public void GetModelNameWithTrailingSlash()
        {
            var requestUri = new Uri("http://services.odata.org/OData/Products/");

            Assert.Equal("Products", requestUri.GetModelName());
        }

        [Fact]
        public void GetODataServiceUri()
        {
            var requestUri = new Uri("http://services.odata.org/OData/Products");

            Assert.Equal("http://services.odata.org/OData/", requestUri.GetODataServiceUri().ToString());
        }

        [Fact]
        public void GetODataServiceUriWithEntityKey()
        {
            var requestUri = new Uri("http://services.odata.org/OData/Products(1)");

            Assert.Equal("http://services.odata.org/OData/", requestUri.GetODataServiceUri().ToString());
        }

        [Fact]
        public void GetODataServiceUriWithTrailingSlash()
        {
            var requestUri = new Uri("http://services.odata.org/OData/Products/");

            Assert.Equal("http://services.odata.org/OData/", requestUri.GetODataServiceUri().ToString());
        }
    }
}