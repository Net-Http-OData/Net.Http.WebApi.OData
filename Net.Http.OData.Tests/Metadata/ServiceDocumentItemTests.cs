namespace Net.Http.OData.Tests.Metadata
{
    using System;
    using Net.Http.OData.Metadata;
    using Xunit;

    public class ServiceDocumentItemTests
    {
        [Fact]
        public void EntitySet()
        {
            var uri = new Uri("Products", UriKind.Relative);
            var serviceDocumentItem = ServiceDocumentItem.EntitySet("Products", uri);

            Assert.Equal("EntitySet", serviceDocumentItem.Kind);
            Assert.Equal("Products", serviceDocumentItem.Name);
            Assert.Equal(uri, serviceDocumentItem.Url);
        }
    }
}