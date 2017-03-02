namespace Net.Http.WebApi.Tests.OData.Query.Expressions
{
    using System;
    using Net.Http.WebApi.OData.Query.Expressions;
    using WebApi.OData.Model;
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
        public void Int32ZeroValueIsSingleton()
        {
            var node1 = ConstantNode.Int32Zero;
            var node2 = ConstantNode.Int32Zero;

            Assert.Same(node1, node2);
        }

        [Fact]
        public void Int64ZeroValueIsSingleton()
        {
            var node1 = ConstantNode.Int64Zero;
            var node2 = ConstantNode.Int64Zero;

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

        public class DateTimeOffsetValue
        {
            private readonly ConstantNode node;

            public DateTimeOffsetValue()
            {
                this.node = ConstantNode.DateTimeOffset("2002-10-15T17:34:23Z", new DateTimeOffset(2002, 10, 15, 17, 34, 23, TimeSpan.Zero));
            }

            [Fact]
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.DateTimeOffset, this.node.EdmPrimitiveType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("2002-10-15T17:34:23Z", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<DateTimeOffset>(this.node.Value);
                Assert.Equal(new DateTimeOffset(2002, 10, 15, 17, 34, 23, TimeSpan.Zero), this.node.Value);
            }
        }

        public class DateValue
        {
            private readonly ConstantNode node;

            public DateValue()
            {
                this.node = ConstantNode.Date("2000-12-18", new DateTime(2000, 12, 18));
            }

            [Fact]
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.Date, this.node.EdmPrimitiveType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("2000-12-18", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.IsType<DateTime>(this.node.Value);
                Assert.Equal(new DateTime(2000, 12, 18), this.node.Value);
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
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.Decimal, this.node.EdmPrimitiveType);
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
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.Double, this.node.EdmPrimitiveType);
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
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.Boolean, this.node.EdmPrimitiveType);
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
                this.node = ConstantNode.Guid("12345678-aaaa-bbbb-cccc-ddddeeeeffff", new Guid("12345678-aaaa-bbbb-cccc-ddddeeeeffff"));
            }

            [Fact]
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.Guid, this.node.EdmPrimitiveType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("12345678-aaaa-bbbb-cccc-ddddeeeeffff", this.node.LiteralText);
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
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.Int32, this.node.EdmPrimitiveType);
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

        public class Int32ZeroValue
        {
            private readonly ConstantNode node;

            public Int32ZeroValue()
            {
                this.node = ConstantNode.Int32Zero;
            }

            [Fact]
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.Int32, this.node.EdmPrimitiveType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("0", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.Equal(0, this.node.Value);
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
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.Int64, this.node.EdmPrimitiveType);
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

        public class Int64ZeroValue
        {
            private readonly ConstantNode node;

            public Int64ZeroValue()
            {
                this.node = ConstantNode.Int64Zero;
            }

            [Fact]
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.Int64, this.node.EdmPrimitiveType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("0L", this.node.LiteralText);
            }

            [Fact]
            public void TheValuePropertyIsSet()
            {
                Assert.Equal(0L, this.node.Value);
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
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.Null, this.node.EdmPrimitiveType);
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
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.Single, this.node.EdmPrimitiveType);
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
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.String, this.node.EdmPrimitiveType);
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

        public class TimeOfDayValue
        {
            private readonly ConstantNode node;

            public TimeOfDayValue()
            {
                this.node = ConstantNode.Time("13:20:00", new TimeSpan(13, 20, 0));
            }

            [Fact]
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.TimeOfDay, this.node.EdmPrimitiveType);
            }

            [Fact]
            public void TheKindIsQueryNodeKindConstant()
            {
                Assert.Equal(QueryNodeKind.Constant, this.node.Kind);
            }

            [Fact]
            public void TheLiteralTextPropertyIsSet()
            {
                Assert.Equal("13:20:00", this.node.LiteralText);
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
            public void TheEdmPrimitiveTypeIsSet()
            {
                Assert.Equal(EdmPrimitiveType.Boolean, this.node.EdmPrimitiveType);
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