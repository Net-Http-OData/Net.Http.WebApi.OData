namespace Net.Http.WebApi.Tests.OData.Query.Expressions
{
    using Net.Http.WebApi.OData.Query.Expressions;
    using Xunit;

    public class SingleValueFunctionCallNodeTests
    {
        public class WhenConstructed
        {
            private readonly string functionName = "substringof";
            private readonly SingleValueFunctionCallNode node;

            public WhenConstructed()
            {
                this.node = new SingleValueFunctionCallNode(this.functionName);
            }

            [Fact]
            public void TheArgumentsCollectionIsEmpty()
            {
                Assert.Empty(this.node.Arguments);
            }

            [Fact]
            public void TheArgumentsCollectionIsNotNull()
            {
                Assert.NotNull(this.node.Arguments);
            }

            [Fact]
            public void TheKindIsQueryNodeKindSingleValueFunctionCall()
            {
                Assert.Equal(QueryNodeKind.SingleValueFunctionCall, this.node.Kind);
            }

            [Fact]
            public void ThePropertyNamePropertyIsSet()
            {
                Assert.Equal(this.functionName, this.node.Name);
            }
        }
    }
}