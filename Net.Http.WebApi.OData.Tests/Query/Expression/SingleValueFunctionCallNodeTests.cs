namespace Net.Http.WebApi.Tests.OData.Query.Expression
{
    using Net.Http.WebApi.OData.Query.Expression;
    using Xunit;

    public class SingleValueFunctionCallNodeTests
    {
        public class WhenConstructed
        {
            private readonly QueryNode[] arguments = new[] { new ConstantNode("1", 1), new ConstantNode("true", true) };
            private readonly string functionName = "substringof";
            private readonly SingleValueFunctionCallNode node;

            public WhenConstructed()
            {
                this.node = new SingleValueFunctionCallNode(this.functionName, arguments);
            }

            [Fact]
            public void TheArgumentsPropertyIsSet()
            {
                Assert.Equal(this.arguments, this.node.Arguments);
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