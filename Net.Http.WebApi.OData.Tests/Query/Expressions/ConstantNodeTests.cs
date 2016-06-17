namespace Net.Http.WebApi.Tests.OData.Query.Expressions
{
    using System;
    using Net.Http.WebApi.OData.Query.Expressions;
    using Xunit;

    public class ConstantNodeTests
    {
        [Fact]
        public void FalseValueIsSingleton()
        {
            var node1 = ConstantNode.False;
            var node2 = ConstantNode.False;

            Assert.Same(node1, node2);
        }

        [Fact]
        public void NullValueIsSingleton()
        {
            var node1 = ConstantNode.Null;
            var node2 = ConstantNode.Null;

            Assert.Same(node1, node2);
        }

        [Fact]
        public void TrueValueIsSingleton()
        {
            var node1 = ConstantNode.True;
            var node2 = ConstantNode.True;

            Assert.Same(node1, node2);
        }

        public class BooleanFalseValue
        {
            private readonly ConstantNode node;

            public BooleanFalseValue()
            {
                this.node = ConstantNode.Boolean("false", false);
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.Boolean, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("false", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<bool>(this.node.Value);
                Assert.False((bool)this.node.Value);
            }
        }

        public class BooleanTrueValue
        {
            private readonly ConstantNode node;

            public BooleanTrueValue()
            {
                this.node = ConstantNode.Boolean("true", true);
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.Boolean, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("true", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<bool>(this.node.Value);
                Assert.True((bool)this.node.Value);
            }
        }

        public class DateTimeOffsetValue
        {
            private readonly ConstantNode node;

            public DateTimeOffsetValue()
            {
                this.node = ConstantNode.DateTimeOffset("datetimeoffset'2002-10-10T17:00:00Z'", new DateTimeOffset(2002, 10, 10, 17, 00, 00, TimeSpan.Zero));
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.DateTimeOffset, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("datetimeoffset'2002-10-10T17:00:00Z'", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<DateTimeOffset>(this.node.Value);
                Assert.Equal(new DateTimeOffset(2002, 10, 10, 17, 00, 00, TimeSpan.Zero), this.node.Value);
            }
        }

        public class DateTimeValue
        {
            private readonly ConstantNode node;

            public DateTimeValue()
            {
                this.node = ConstantNode.DateTime("datetime'2000-12-12T12:00'", new DateTime(2000, 12, 12, 12, 0, 0));
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.DateTime, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("datetime'2000-12-12T12:00'", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<DateTime>(this.node.Value);
                Assert.Equal(new DateTime(2000, 12, 12, 12, 0, 0), this.node.Value);
            }
        }

        public class DecimalValue
        {
            private readonly ConstantNode node;

            public DecimalValue()
            {
                this.node = ConstantNode.Decimal("2.345M", 2.345M);
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.Decimal, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("2.345M", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<decimal>(this.node.Value);
                Assert.Equal(2.345M, this.node.Value);
            }
        }

        public class DoubleValue
        {
            private readonly ConstantNode node;

            public DoubleValue()
            {
                this.node = ConstantNode.Double("2.029d", 2.029d);
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.Double, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("2.029d", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<double>(this.node.Value);
                Assert.Equal(2.029d, this.node.Value);
            }
        }

        public class FalseValue
        {
            private readonly ConstantNode node;

            public FalseValue()
            {
                this.node = ConstantNode.False;
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.Boolean, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("false", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<bool>(this.node.Value);
                Assert.False((bool)this.node.Value);
            }
        }

        public class GuidValue
        {
            private readonly ConstantNode node;

            public GuidValue()
            {
                this.node = ConstantNode.Guid("guid'12345678-aaaa-bbbb-cccc-ddddeeeeffff'", new Guid("12345678-aaaa-bbbb-cccc-ddddeeeeffff"));
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.Guid, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("guid'12345678-aaaa-bbbb-cccc-ddddeeeeffff'", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<Guid>(this.node.Value);
                Assert.Equal(new Guid("12345678-aaaa-bbbb-cccc-ddddeeeeffff"), this.node.Value);
            }
        }

        public class Int32Value
        {
            private readonly ConstantNode node;

            public Int32Value()
            {
                this.node = ConstantNode.Int32("32", 32);
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.Int32, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("32", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<int>(this.node.Value);
                Assert.Equal(32, this.node.Value);
            }
        }

        public class Int64Value
        {
            private readonly ConstantNode node;

            public Int64Value()
            {
                this.node = ConstantNode.Int64("64L", 64L);
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.Int64, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("64L", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<long>(this.node.Value);
                Assert.Equal(64L, this.node.Value);
            }
        }

        public class NullValue
        {
            private readonly ConstantNode node;

            public NullValue()
            {
                this.node = ConstantNode.Null;
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.Null, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("null", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.Null(this.node.Value);
            }
        }

        public class SingleValue
        {
            private readonly ConstantNode node;

            public SingleValue()
            {
                this.node = ConstantNode.Single("2.0f", 2.0f);
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.Single, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("2.0f", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<float>(this.node.Value);
                Assert.Equal(2.0f, this.node.Value);
            }
        }

        public class StringValue
        {
            private readonly ConstantNode node;

            public StringValue()
            {
                this.node = ConstantNode.String("'Hello OData'", "Hello OData");
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.String, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("'Hello OData'", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<string>(this.node.Value);
                Assert.Equal("Hello OData", this.node.Value);
            }
        }

        public class TimeValue
        {
            private readonly ConstantNode node;

            public TimeValue()
            {
                this.node = ConstantNode.Time("time'13:20:00'", new TimeSpan(13, 20, 0));
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.Time, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("time'13:20:00'", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<TimeSpan>(this.node.Value);
                Assert.Equal(new TimeSpan(13, 20, 0), this.node.Value);
            }
        }

        public class TrueValue
        {
            private readonly ConstantNode node;

            public TrueValue()
            {
                this.node = ConstantNode.True;
            }

            [Fact]
            public void TheEdmTypeIsSet()
            {
                Assert.Equal(EdmType.Boolean, this.node.EdmType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("true", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<bool>(this.node.Value);
                Assert.True((bool)this.node.Value);
            }
        }
    }
}