namespace Net.Http.WebApi.Tests.OData
{
    using System.Collections.Generic;
    using Net.Http.WebApi.OData;
    using Xunit;

    public class InlineCountCollectionTests
    {
        public class WhenEnumeratingAnInlineCountCollection
        {
            private readonly InlineCountCollection<int> inlineCountCollection;
            private readonly List<int> list = new List<int> { 1, 2, 3 };

            public WhenEnumeratingAnInlineCountCollection()
            {
                this.inlineCountCollection = new InlineCountCollection<int>(list, 3);
            }

            [Fact]
            public void TheWrappedCollectionIsEnumerated()
            {
                Assert.Equal(list, inlineCountCollection);
            }
        }
    }
}