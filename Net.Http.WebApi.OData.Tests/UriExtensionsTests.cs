namespace Net.Http.WebApi.OData.Tests
{
    using System;
    using Xunit;

    public class UriExtensionsTests
    {
        [Fact]
        public void GetModelName()
        {
            var requestUri = new Uri("http://services.odata.org/OData/OData.svc/Products");

            Assert.Equal("Products", requestUri.GetModelName());
        }

        [Fact]
        public void GetModelNameWithTrailingSlash()
        {
            var requestUri = new Uri("http://services.odata.org/OData/OData.svc/Products/");

            Assert.Equal("Products", requestUri.GetModelName());
        }
    }
}