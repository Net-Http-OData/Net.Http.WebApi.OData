using Net.Http.OData;
using Xunit;

namespace Net.Http.WebApi.OData.Tests
{
    public class ODataMetadataLevelExtensionsTests
    {
        [Fact]
        public void MetadataLevel_Full_Returns_NameValueHeaderValue()
        {
            var headerValue = ODataMetadataLevel.Full.ToNameValueHeaderValue();

            Assert.Equal(ODataMetadataLevelExtensions.HeaderName, headerValue.Name);
            Assert.Equal("full", headerValue.Value);
        }

        [Fact]
        public void MetadataLevel_Minimal_Returns_NameValueHeaderValue()
        {
            var headerValue = ODataMetadataLevel.Minimal.ToNameValueHeaderValue();

            Assert.Equal(ODataMetadataLevelExtensions.HeaderName, headerValue.Name);
            Assert.Equal("minimal", headerValue.Value);
        }

        [Fact]
        public void MetadataLevel_None_Returns_NameValueHeaderValue()
        {
            var headerValue = ODataMetadataLevel.None.ToNameValueHeaderValue();

            Assert.Equal(ODataMetadataLevelExtensions.HeaderName, headerValue.Name);
            Assert.Equal("none", headerValue.Value);
        }
    }
}
