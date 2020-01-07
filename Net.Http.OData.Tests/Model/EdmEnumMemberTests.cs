using Net.Http.OData.Model;
using Xunit;

namespace Net.Http.OData.Tests.Model
{
    public class EdmEnumMemberTests
    {
        [Fact]
        public void Constructor_SetsProperties()
        {
            var edmEnumMember = new EdmEnumMember("Read", 1);

            Assert.Equal("Read", edmEnumMember.Name);
            Assert.Equal(1, edmEnumMember.Value);
        }
    }
}