namespace Net.Http.WebApi.OData.Tests.Query.Parsers
{
    using System;
    using System.Globalization;
    using NorthwindModel;
    using WebApi.OData.Model;
    using WebApi.OData.Query.Expressions;
    using WebApi.OData.Query.Parsers;
    using Xunit;

    public partial class FilterExpressionParserTests
    {
        public class SingleValuePropertyValueTests
        {
            public SingleValuePropertyValueTests()
            {
                TestHelper.EnsureEDM();
            }

            [Fact]
            public void ParsePropertyAddValueEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price add 2.45M eq 5.00M", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Price", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Add, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("2.45M", ((ConstantNode)nodeLeft.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)nodeLeft.Right).Value);
                Assert.Equal(2.45M, ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("5.00M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(5.00M, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyDivValueEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price div 2.55M eq 1M", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Price", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Divide, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("2.55M", ((ConstantNode)nodeLeft.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)nodeLeft.Right).Value);
                Assert.Equal(2.55M, ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(1M, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateTimeOffset_DateHourMinute_ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq 2013-06-18T09:30", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2013-06-18T09:30", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTimeOffset>(((ConstantNode)node.Right).Value);

                // Since the request string doesn't contain a timezone offset, assume local
                Assert.Equal(DateTimeOffset.Parse("2013-06-18T09:30", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateTimeOffset_DateHourMinuteSecond_ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq 2013-06-18T09:30:54", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2013-06-18T09:30:54", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTimeOffset>(((ConstantNode)node.Right).Value);

                // Since the request string doesn't contain a timezone offset, assume local
                Assert.Equal(DateTimeOffset.Parse("2013-06-18T09:30:54", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateTimeOffset_MinusOffset_ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq 2002-10-15T17:34:23-02:00", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2002-10-15T17:34:23-02:00", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTimeOffset>(((ConstantNode)node.Right).Value);
                Assert.Equal(new DateTimeOffset(2002, 10, 15, 17, 34, 23, TimeSpan.FromHours(-2)), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateTimeOffset_MomentJsIsoStringFormat_ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq 2013-02-04T22:44:30.652Z", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2013-02-04T22:44:30.652Z", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTimeOffset>(((ConstantNode)node.Right).Value);
                Assert.Equal(new DateTimeOffset(2013, 2, 4, 22, 44, 30, 652, TimeSpan.Zero), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateTimeOffset_PlusOffset_ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq 2002-10-15T17:34:23+02:00", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2002-10-15T17:34:23+02:00", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTimeOffset>(((ConstantNode)node.Right).Value);
                Assert.Equal(new DateTimeOffset(2002, 10, 15, 17, 34, 23, TimeSpan.FromHours(2)), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateTimeOffset_ToStringSFormat_ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq 2013-06-18T09:30:20", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2013-06-18T09:30:20", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTimeOffset>(((ConstantNode)node.Right).Value);

                // Since the request string doesn't contain a timezone offset, assume local
                Assert.Equal(DateTimeOffset.Parse("2013-06-18T09:30:20", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateTimeOffset_Z_ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq 2002-10-15T17:34:23Z", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2002-10-15T17:34:23Z", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTimeOffset>(((ConstantNode)node.Right).Value);
                Assert.Equal(new DateTimeOffset(2002, 10, 15, 17, 34, 23, TimeSpan.Zero), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateTimeOffset_ZeroOffset_ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq 2017-02-28T16:34:18", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2017-02-28T16:34:18", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTimeOffset>(((ConstantNode)node.Right).Value);

                // Since the request string doesn't contain a timezone offset, assume local
                Assert.Equal(DateTimeOffset.Parse("2017-02-28T16:34:18", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqDateValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq 2013-06-18", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2013-06-18", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<DateTime>(((ConstantNode)node.Right).Value);
                Assert.Equal(new DateTime(2013, 6, 18, 0, 0, 0, DateTimeKind.Local), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqEnumFlagsValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("AccessLevel has NorthwindModel.AccessLevel'Read,Write'", EntityDataModel.Current.EntitySets["Employees"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("AccessLevel", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Has, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("NorthwindModel.AccessLevel'Read,Write'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<AccessLevel>(((ConstantNode)node.Right).Value);
                Assert.Equal(AccessLevel.Read | AccessLevel.Write, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqEnumValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Colour eq NorthwindModel.Colour'Blue'", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Colour", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("NorthwindModel.Colour'Blue'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<Colour>(((ConstantNode)node.Right).Value);
                Assert.Equal(Colour.Blue, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqFalseValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Deleted eq false", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Deleted", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Same(ConstantNode.False, node.Right);
            }

            [Fact]
            public void ParsePropertyEqGuidValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("TransactionId eq 0D01B09B-38CD-4C53-AA04-181371087A00", EntityDataModel.Current.EntitySets["Orders"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("TransactionId", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("0D01B09B-38CD-4C53-AA04-181371087A00", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<Guid>(((ConstantNode)node.Right).Value);
                Assert.Equal(new Guid("0D01B09B-38CD-4C53-AA04-181371087A00"), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqInt32ZeroValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Rating eq 0", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Rating", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Same(ConstantNode.Int32Zero, node.Right);
            }

            [Fact]
            public void ParsePropertyEqInt64ZeroValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Rating eq 0L", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Rating", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Same(ConstantNode.Int64Zero, node.Right);
            }

            [Fact]
            public void ParsePropertyEqNegativeDecimalValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price eq -1234.567M", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Price", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("-1234.567M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(-1234.567M, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNegativeDecimalWithNoDigitBeforeDecimalPointValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price eq -.1M", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Price", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("-.1M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(-.1M, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNegativeDoubleValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Rating eq -1234.567D", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Rating", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("-1234.567D", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<double>(((ConstantNode)node.Right).Value);
                Assert.Equal(-1234.567D, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNegativeDoubleWithExponentValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Rating eq -0.314e1", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Rating", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("-0.314e1", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<double>(((ConstantNode)node.Right).Value);
                Assert.Equal(-0.314e1D, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNegativeDurationValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq duration'-P6DT23H59M59.9999S'", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("duration'-P6DT23H59M59.9999S'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<TimeSpan>(((ConstantNode)node.Right).Value);
                Assert.Equal(TimeSpan.Parse("-6.23:59:59.9999"), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNegativeFloatValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Rating eq -1234.567F", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Rating", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("-1234.567F", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<float>(((ConstantNode)node.Right).Value);
                Assert.Equal(-1234.567F, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNegativeInt32ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Rating eq -1234", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Rating", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("-1234", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(-1234, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNegativeInt64ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Rating eq -1234L", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Rating", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("-1234L", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<long>(((ConstantNode)node.Right).Value);
                Assert.Equal(-1234L, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqNullValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Description eq null", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Description", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Same(node.Right, ConstantNode.Null);
            }

            [Fact]
            public void ParsePropertyEqPositiveDecimalValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price eq 1234.567M", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Price", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1234.567M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(1234.567M, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPositiveDecimalWithNoDigitBeforeDecimalPointValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price eq .1M", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Price", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal(".1M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(.1M, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPositiveDoubleValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price eq 1234.567D", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Price", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1234.567D", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<double>(((ConstantNode)node.Right).Value);
                Assert.Equal(1234.567D, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPositiveDoubleWithExponentValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Rating eq 0.314e1", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Rating", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("0.314e1", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<double>(((ConstantNode)node.Right).Value);
                Assert.Equal(0.314e1D, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPositiveDurationValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq duration'P6DT23H59M59.9999S'", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("duration'P6DT23H59M59.9999S'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<TimeSpan>(((ConstantNode)node.Right).Value);
                Assert.Equal(TimeSpan.Parse("6.23:59:59.9999"), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPositiveFloatValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Rating eq 1234.567F", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Rating", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1234.567F", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<float>(((ConstantNode)node.Right).Value);
                Assert.Equal(1234.567F, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPositiveInt32ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Rating eq 1234", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Rating", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1234", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(1234, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPositiveInt64ValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Rating eq 1234L", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Rating", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("1234L", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<long>(((ConstantNode)node.Right).Value);
                Assert.Equal(1234L, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqPropertyExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Name eq Description", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Name", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<PropertyAccessNode>(node.Right);
                Assert.Equal("Description", ((PropertyAccessNode)node.Right).Property.Name);
            }

            [Fact]
            public void ParsePropertyEqStringFullCharacterSetValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Name eq 'ABCDEFGHIHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~!$&('')*+,;=@'", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Name", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'ABCDEFGHIHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~!$&('')*+,;=@'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("ABCDEFGHIHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~!$&(')*+,;=@", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqStringValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Name eq 'Milk'", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Name", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'Milk'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("Milk", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqStringWithQuoteCharacterValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("CompanyName eq 'O''Neil'", EntityDataModel.Current.EntitySets["Customers"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("CompanyName", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'O''Neil'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("O'Neil", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqTimeOfDayHourMinuteSecondFractionalSecondValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq 13:20:45.352", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("13:20:45.352", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<TimeSpan>(((ConstantNode)node.Right).Value);
                Assert.Equal(new TimeSpan(0, 13, 20, 45, 352), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqTimeOfDayHourMinuteSecondValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq 13:20:45", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("13:20:45", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<TimeSpan>(((ConstantNode)node.Right).Value);
                Assert.Equal(new TimeSpan(13, 20, 45), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqTimeOfDayHourMinuteValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("ReleaseDate eq 13:20", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("ReleaseDate", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("13:20", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<TimeSpan>(((ConstantNode)node.Right).Value);
                Assert.Equal(new TimeSpan(13, 20, 0), ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqTrueValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Deleted eq true", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Deleted", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Same(ConstantNode.True, node.Right);
            }

            [Fact]
            public void ParsePropertyGeThanIntValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price ge 10", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Price", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.GreaterThanOrEqual, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("10", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(10, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyGtThanIntValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price gt 20", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Price", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.GreaterThan, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("20", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(20, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyLeThanIntValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price le 100", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Price", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.LessThanOrEqual, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("100", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(100, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyLtThanIntValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price lt 20", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Price", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.LessThan, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("20", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(20, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyModValueEqPropertyExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price mod 2 eq 0", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Price", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Modulo, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("2", ((ConstantNode)nodeLeft.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)nodeLeft.Right).Value);
                Assert.Equal(2, ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("0", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(0, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyMulValueEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price mul 2.0M eq 5.10M", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Price", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Multiply, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("2.0M", ((ConstantNode)nodeLeft.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)nodeLeft.Right).Value);
                Assert.Equal(2.0M, ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("5.10M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(5.10M, ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertyNeStringValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Name ne 'Milk'", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<PropertyAccessNode>(node.Left);
                Assert.Equal("Name", ((PropertyAccessNode)node.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.NotEqual, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("'Milk'", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<string>(((ConstantNode)node.Right).Value);
                Assert.Equal("Milk", ((ConstantNode)node.Right).Value);
            }

            [Fact]
            public void ParsePropertySubValueEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Price sub 0.55M eq 2.00M", EntityDataModel.Current.EntitySets["Products"]);

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Price", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Subtract, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("0.55M", ((ConstantNode)nodeLeft.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)nodeLeft.Right).Value);
                Assert.Equal(0.55M, ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("2.00M", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<decimal>(((ConstantNode)node.Right).Value);
                Assert.Equal(2.00M, ((ConstantNode)node.Right).Value);
            }
        }
    }
}