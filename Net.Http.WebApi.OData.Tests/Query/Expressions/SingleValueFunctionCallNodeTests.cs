namespace Net.Http.WebApi.Tests.OData.Query.Expressions
{
    using Net.Http.WebApi.OData.Query.Expressions;
    using Xunit;

    public class SingleValueFunctionCallNodeTests
    {
        public class WhenAddingAParameter
        {
            private readonly SingleValueFunctionCallNode node;
            private readonly QueryNode parameter = ConstantNode.String("Hi", "Hi");

            public WhenAddingAParameter()
            {
                this.node = new SingleValueFunctionCallNode("substringof");
                this.node.Parameters.Add(parameter);
            }

            [Fact]
            public void TheParameterExistsInTheParameterCollection()
            {
                Assert.Contains(parameter, node.Parameters);
            }
        }

        public class WhenConstructed
        {
            private readonly string functionName = "substringof";
            private readonly SingleValueFunctionCallNode node;

            public WhenConstructed()
            {
                this.node = new SingleValueFunctionCallNode(this.functionName);
            }

            [Fact]
            public void TheKindIsQueryNodeKindSingleValueFunctionCall()
            {
                Assert.Equal(QueryNodeKind.SingleValueFunctionCall, this.node.Kind);
            }

            [Fact]
            public void TheParametersCollectionIsEmpty()
            {
                Assert.Empty(this.node.Parameters);
            }

            [Fact]
            public void TheParametersCollectionIsNotNull()
            {
                Assert.NotNull(this.node.Parameters);
            }

            [Fact]
            public void ThePropertyNamePropertyIsSet()
            {
                Assert.Equal(this.functionName, this.node.Name);
            }
        }
    }
}