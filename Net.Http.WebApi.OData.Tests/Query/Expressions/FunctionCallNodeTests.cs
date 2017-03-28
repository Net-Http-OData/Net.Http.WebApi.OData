namespace Net.Http.WebApi.OData.Tests.Query.Expressions
{
    using Net.Http.WebApi.OData.Query.Expressions;
    using Xunit;

    public class FunctionCallNodeTests
    {
        public class WhenAddingAParameter
        {
            private readonly FunctionCallNode node;
            private readonly QueryNode parameter = ConstantNode.String("Hi", "Hi");

            public WhenAddingAParameter()
            {
                this.node = new FunctionCallNode("contains");
                this.node.AddParameter(parameter);
            }

            [Fact]
            public void TheParameterExistsInTheParameterCollection()
            {
                Assert.Contains(parameter, node.Parameters);
            }
        }

        public class WhenConstructed
        {
            private readonly string functionName = "contains";
            private readonly FunctionCallNode node;

            public WhenConstructed()
            {
                this.node = new FunctionCallNode(this.functionName);
            }

            [Fact]
            public void TheKindIsQueryNodeKindSingleValueFunctionCall()
            {
                Assert.Equal(QueryNodeKind.FunctionCall, this.node.Kind);
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