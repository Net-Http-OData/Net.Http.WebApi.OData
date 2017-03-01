namespace Net.Http.WebApi.Tests.OData.Query.Parsers
{
    using Net.Http.WebApi.OData.Query.Expressions;
    using Net.Http.WebApi.OData.Query.Parsers;
    using Xunit;

    public partial class FilterExpressionParserTests
    {
        public class ComplexExpressionTests
        {
            /// <summary>
            /// https://github.com/TrevorPilley/Net.Http.WebApi.OData/issues/55 - Grouped parsing error
            /// </summary>
            [Fact]
            public void Parse_GroupedA_AndGroupedBandC_AndGroupedD()
            {
                var queryNode = FilterExpressionParser.Parse("(CustomerId eq 9) and (Created ge 2015-03-01T00:00:00 and Created le 2015-03-31T23:59:59) and (Status eq 1)");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                //                             == Expected Tree Structure ==
                //                         ----------------- and -------------------
                //                        |                                         |
                //              -------- and --------                          --- eq ---
                //             |                     |                        |          |
                //         --- eq ---           --- and ---
                //        |          |         |           |
                //                        --- ge ---   --- le ---
                //                       |          |  |          |

                var node = (BinaryOperatorNode)queryNode;

                // node.Left = (CustomerId eq 9) and (Created ge 2015-03-01T00:00:00 and Created le 2015-03-31T23:59:59)
                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;

                // node.Left.Left = (CustomerId eq 9)
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;

                Assert.IsType<PropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("CustomerId", ((PropertyAccessNode)nodeLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeftLeft.Right);
                Assert.Equal("9", ((ConstantNode)nodeLeftLeft.Right).LiteralText);

                // node.Left.Right = (Created ge 2015-03-01T00:00:00 and Created le 2015-03-31T23:59:59)
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Right);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;

                Assert.IsType<BinaryOperatorNode>(nodeLeftRight.Left);
                var nodeLeftRightLeft = (BinaryOperatorNode)nodeLeftRight.Left;

                Assert.IsType<PropertyAccessNode>(nodeLeftRightLeft.Left);
                Assert.Equal("Created", ((PropertyAccessNode)nodeLeftRightLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.GreaterThanOrEqual, nodeLeftRightLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeftRightLeft.Right);
                Assert.Equal("2015-03-01T00:00:00", ((ConstantNode)nodeLeftRightLeft.Right).LiteralText);

                Assert.Equal(BinaryOperatorKind.And, nodeLeftRight.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(nodeLeftRight.Right);
                var nodeLeftRightRight = (BinaryOperatorNode)nodeLeftRight.Right;

                Assert.IsType<PropertyAccessNode>(nodeLeftRightRight.Left);
                Assert.Equal("Created", ((PropertyAccessNode)nodeLeftRightRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.LessThanOrEqual, nodeLeftRightRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeftRightRight.Right);
                Assert.Equal("2015-03-31T23:59:59", ((ConstantNode)nodeLeftRightRight.Right).LiteralText);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                // node.Right = (Status eq 1)
                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;

                Assert.IsType<PropertyAccessNode>(nodeRight.Left);
                Assert.Equal("Status", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal("1", ((ConstantNode)nodeRight.Right).LiteralText);
            }

            [Fact]
            public void Parse_GroupedAandBandC_And_GroupedDorEorF()
            {
                var queryNode = FilterExpressionParser.Parse("(Date ge 2015-02-06T00:00:00 and Date le 2015-02-06T23:59:59 and CustomerId eq 122134) and (Level eq 'WARN' or Level eq 'ERROR' or Level eq 'FATAL')");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                //                             == Expected Tree Structure ==
                //                         ----------------- and -------------------
                //                        |                                         |
                //              -------- and --------                      -------- or ---------
                //             |                     |                    |                     |
                //       ---- and ----           --- eq ---          ---- or ----           --- eq ---
                //      |             |         |          |        |            |         |          |
                //  --- ge ---    --- le ---                    --- eq ---     --- eq ---
                // |          |  |          |                  |          |   |          |

                var node = (BinaryOperatorNode)queryNode;

                // node.Left = (Date ge 2015-02-06T00:00:00 and Date le 2015-02-06T23:59:59 and CustomerId eq 122134)
                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;

                // node.Left.Left = Date ge 2015-02-06T00:00:00 and Date le 2015-02-06T23:59:59
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;

                // node.Left.Left.Left = Date ge 2015-02-06T00:00:00
                Assert.IsType<BinaryOperatorNode>(nodeLeftLeft.Left);
                var nodeLeftLeftLeft = (BinaryOperatorNode)nodeLeftLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeftLeft.Left);
                Assert.Equal("Date", ((PropertyAccessNode)nodeLeftLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.GreaterThanOrEqual, nodeLeftLeftLeft.OperatorKind);
                Assert.Equal("2015-02-06T00:00:00", ((ConstantNode)nodeLeftLeftLeft.Right).LiteralText);

                Assert.Equal(BinaryOperatorKind.And, nodeLeftLeft.OperatorKind);

                // node.Right.Left.Right = Date le 2015-02-06T23:59:59
                Assert.IsType<BinaryOperatorNode>(nodeLeftLeft.Right);
                var nodeLeftLeftRight = (BinaryOperatorNode)nodeLeftLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeftRight.Left);
                Assert.Equal("Date", ((PropertyAccessNode)nodeLeftLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.LessThanOrEqual, nodeLeftLeftRight.OperatorKind);
                Assert.Equal("2015-02-06T23:59:59", ((ConstantNode)nodeLeftLeftRight.Right).LiteralText);

                Assert.Equal(BinaryOperatorKind.And, nodeLeft.OperatorKind);

                // node.Right.Right = CustomerId eq 122134
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Right);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("CustomerId", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.Equal("122134", ((ConstantNode)nodeLeftRight.Right).LiteralText);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                // node.Right = (Level eq 'WARN' or Level eq 'ERROR' or Level eq 'FATAL')
                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;

                // node.Right.Left = Level eq 'WARN' or Level eq 'ERROR'
                Assert.IsType<BinaryOperatorNode>(nodeRight.Left);
                var nodeRightLeft = (BinaryOperatorNode)nodeRight.Left;

                // node.Right.Left.Left = Level eq 'WARN'
                Assert.IsType<BinaryOperatorNode>(nodeRightLeft.Left);
                var nodeRightLeftLeft = (BinaryOperatorNode)nodeRightLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeRightLeftLeft.Left);
                Assert.Equal("Level", ((PropertyAccessNode)nodeRightLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeftLeft.OperatorKind);
                Assert.Equal("'WARN'", ((ConstantNode)nodeRightLeftLeft.Right).LiteralText);
                Assert.Equal("WARN", ((ConstantNode)nodeRightLeftLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, nodeRightLeft.OperatorKind);

                // node.Right.Left.Right = Level eq 'ERROR'
                Assert.IsType<BinaryOperatorNode>(nodeRightLeft.Right);
                var nodeRightLeftRight = (BinaryOperatorNode)nodeRightLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeRightLeftRight.Left);
                Assert.Equal("Level", ((PropertyAccessNode)nodeRightLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeftRight.OperatorKind);
                Assert.Equal("'ERROR'", ((ConstantNode)nodeRightLeftRight.Right).LiteralText);
                Assert.Equal("ERROR", ((ConstantNode)nodeRightLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, nodeRight.OperatorKind);

                // node.Right.Right = Level eq 'FATAL'
                Assert.IsType<BinaryOperatorNode>(nodeRight.Right);
                var nodeRightRight = (BinaryOperatorNode)nodeRight.Right;
                Assert.IsType<PropertyAccessNode>(nodeRightRight.Left);
                Assert.Equal("Level", ((PropertyAccessNode)nodeRightRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRight.OperatorKind);
                Assert.Equal("'FATAL'", ((ConstantNode)nodeRightRight.Right).LiteralText);
            }

            /// <summary>
            /// https://github.com/TrevorPilley/Net.Http.WebApi.OData/issues/40 - Grouping doesn't support (x and y) and (a or b or c)
            /// </summary>
            [Fact]
            public void Parse_GroupedXandY_And_GroupedAorBorC()
            {
                var queryNode = FilterExpressionParser.Parse("(Date ge 2015-02-06T00:00:00 and Date le 2015-02-06T23:59:59) and (Level eq 'WARN' or Level eq 'ERROR' or Level eq 'FATAL')");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                //                  == Expected Tree Structure ==
                //              --------------- and -----------------
                //             |                                     |
                //       ---- and ----                     --------- or ---------
                //      |             |                   |                      |
                // --- ge ---    --- le ---          ---- or ----           --- eq ---
                // |         |   |        |         |            |         |          |
                //                              --- eq ---   --- eq ---
                //                             |         |  |          |

                var node = (BinaryOperatorNode)queryNode;

                // node.Left = (Date ge 2015-02-06T00:00:00 and Date le 2015-02-06T23:59:59)
                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;

                // node.Left.Left = Date ge 2015-02-06T00:00:00
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("Date", ((PropertyAccessNode)nodeLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.GreaterThanOrEqual, nodeLeftLeft.OperatorKind);
                Assert.Equal("2015-02-06T00:00:00", ((ConstantNode)nodeLeftLeft.Right).LiteralText);

                Assert.Equal(BinaryOperatorKind.And, nodeLeft.OperatorKind);

                // node.Left.Right = Date le 2015-02-06T23:59:59
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Right);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Date", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.LessThanOrEqual, nodeLeftRight.OperatorKind);
                Assert.Equal("2015-02-06T23:59:59", ((ConstantNode)nodeLeftRight.Right).LiteralText);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                // node.Right = (Level eq 'WARN' or Level eq 'ERROR' or Level eq 'FATAL')
                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;

                // node.Right.Left = Level eq 'WARN' or Level eq 'ERROR'
                Assert.IsType<BinaryOperatorNode>(nodeRight.Left);
                var nodeRightLeft = (BinaryOperatorNode)nodeRight.Left;

                // node.Right.Left.Left = Level eq 'WARN'
                Assert.IsType<BinaryOperatorNode>(nodeRightLeft.Left);
                var nodeRightLeftLeft = (BinaryOperatorNode)nodeRightLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeRightLeftLeft.Left);
                Assert.Equal("Level", ((PropertyAccessNode)nodeRightLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeftLeft.OperatorKind);
                Assert.Equal("'WARN'", ((ConstantNode)nodeRightLeftLeft.Right).LiteralText);
                Assert.Equal("WARN", ((ConstantNode)nodeRightLeftLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, nodeRightLeft.OperatorKind);

                // node.Right.Left.Right = Level eq 'ERROR'
                Assert.IsType<BinaryOperatorNode>(nodeRightLeft.Right);
                var nodeRightLeftRight = (BinaryOperatorNode)nodeRightLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeRightLeftRight.Left);
                Assert.Equal("Level", ((PropertyAccessNode)nodeRightLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeftRight.OperatorKind);
                Assert.Equal("'ERROR'", ((ConstantNode)nodeRightLeftRight.Right).LiteralText);
                Assert.Equal("ERROR", ((ConstantNode)nodeRightLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, nodeRight.OperatorKind);

                // node.Right.Right = Level eq 'FATAL'
                Assert.IsType<BinaryOperatorNode>(nodeRight.Right);
                var nodeRightRight = (BinaryOperatorNode)nodeRight.Right;
                Assert.IsType<PropertyAccessNode>(nodeRightRight.Left);
                Assert.Equal("Level", ((PropertyAccessNode)nodeRightRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRight.OperatorKind);
                Assert.Equal("'FATAL'", ((ConstantNode)nodeRightRight.Right).LiteralText);
            }

            [Fact]
            public void Parse_XsubY_gt_Z()
            {
                var queryNode = FilterExpressionParser.Parse("(Price sub 5) gt 10");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                //    == Expected Tree Structure ==
                //          ------ gt ------
                //         |                |
                //    --- sub ---           10
                //   |           |
                // Price         5

                var node = (BinaryOperatorNode)queryNode;

                // node.Left = (Price sub 5)
                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal(BinaryOperatorKind.Subtract, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("5", ((ConstantNode)nodeLeft.Right).LiteralText);

                Assert.Equal(BinaryOperatorKind.GreaterThan, node.OperatorKind);

                // node.Right = 10
                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("10", ((ConstantNode)node.Right).LiteralText);
            }

            [Fact]
            public void ParseFunctionCallAndGroupedPropertyEqValueOrPropertyEqValue()
            {
                var queryNode = FilterExpressionParser.Parse("endswith(CompanyName, 'Futterkiste') and (Surname eq 'Smith' or Surname eq 'Smythe')");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("endswith", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeLeft.Parameters[1]);
                Assert.Equal("'Futterkiste'", ((ConstantNode)nodeLeft.Parameters[1]).LiteralText);
                Assert.Equal("Futterkiste", ((ConstantNode)nodeLeft.Parameters[1]).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<BinaryOperatorNode>(nodeRight.Left);
                var nodeRightLeft = (BinaryOperatorNode)nodeRight.Left;
                Assert.IsType<PropertyAccessNode>(nodeRightLeft.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRightLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightLeft.Right);
                Assert.Equal("'Smith'", ((ConstantNode)nodeRightLeft.Right).LiteralText);
                Assert.Equal(BinaryOperatorKind.Or, nodeRight.OperatorKind);
                var nodeRightRight = (BinaryOperatorNode)nodeRight.Right;
                Assert.IsType<PropertyAccessNode>(nodeRightRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRightRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightRight.Right);
                Assert.Equal("'Smythe'", ((ConstantNode)nodeRightRight.Right).LiteralText);
            }

            [Fact]
            public void ParseFunctionCallAndPropertyEqValue()
            {
                var queryNode = FilterExpressionParser.Parse("endswith(CompanyName, 'Futterkiste') and Surname eq 'Smith'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("endswith", nodeLeft.Name);
                Assert.Equal(2, nodeLeft.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeft.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeft.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeLeft.Parameters[1]);
                Assert.Equal("'Futterkiste'", ((ConstantNode)nodeLeft.Parameters[1]).LiteralText);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<PropertyAccessNode>(nodeRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal("'Smith'", ((ConstantNode)nodeRight.Right).LiteralText);
            }

            [Fact]
            public void ParseGroupedPropertyEqValueAndPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("(Forename eq 'John' and Surname eq 'Smith')");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("John", ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<PropertyAccessNode>(nodeRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParseGroupedPropertyEqValueAndPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("(Forename eq 'John' and Surname eq 'Smith') or Age eq 35");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.And, nodeLeft.OperatorKind);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.Equal("Smith", ((ConstantNode)nodeLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                var nodeRightLeft = (PropertyAccessNode)nodeRight.Left;
                Assert.Equal("Age", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                var nodeRightRight = (ConstantNode)nodeRight.Right;
                Assert.Equal(35, ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParseGroupedPropertyEqValueOrPropertyEqValueAndFunctionCall()
            {
                var queryNode = FilterExpressionParser.Parse("(Surname eq 'Smith' or Surname eq 'Smythe') and endswith(CompanyName, 'Futterkiste')");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeftLeft.Right);
                Assert.Equal("'Smith'", ((ConstantNode)nodeLeftLeft.Right).LiteralText);
                Assert.Equal("Smith", ((ConstantNode)nodeLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeLeft.OperatorKind);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeftRight.Right);
                Assert.Equal("'Smythe'", ((ConstantNode)nodeLeftRight.Right).LiteralText);
                Assert.Equal("Smythe", ((ConstantNode)nodeLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                Assert.IsType<FunctionCallNode>(node.Right);
                var nodeRight = (FunctionCallNode)node.Right;
                Assert.Equal("endswith", nodeRight.Name);
                Assert.Equal(2, nodeRight.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeRight.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeRight.Parameters[0]).Property.Name);
                Assert.IsType<ConstantNode>(nodeRight.Parameters[1]);
                Assert.Equal("'Futterkiste'", ((ConstantNode)nodeRight.Parameters[1]).LiteralText);
            }

            [Fact]
            public void ParseGroupedPropertyEqValueorPropertyEqValueAndGroupedPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("(Forename eq 'John' or Forename eq 'Joe') and (Surname eq 'Smith' or Surname eq 'Bloggs')");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeftLeft.Right);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeLeft.OperatorKind);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeftRight.Right);
                Assert.Equal("Joe", ((ConstantNode)nodeLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<BinaryOperatorNode>(nodeRight.Left);
                var nodeRightLeft = (BinaryOperatorNode)nodeRight.Left;
                Assert.IsType<PropertyAccessNode>(nodeRightLeft.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRightLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightLeft.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeRightLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeRight.OperatorKind);
                var nodeRightRight = (BinaryOperatorNode)nodeRight.Right;
                Assert.IsType<PropertyAccessNode>(nodeRightRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRightRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightRight.Right);
                Assert.Equal("Bloggs", ((ConstantNode)nodeRightRight.Right).Value);
            }

            [Fact]
            public void ParseNestedFunctionCallEqFunctionCallExpression()
            {
                var queryNode = FilterExpressionParser.Parse("length(trim(CompanyName)) eq length(CompanyName)");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("length", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<FunctionCallNode>(nodeLeft.Parameters[0]);
                var nodeLeftArg0 = (FunctionCallNode)nodeLeft.Parameters[0];
                Assert.Equal("trim", nodeLeftArg0.Name);
                Assert.Equal(1, nodeLeftArg0.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeftArg0.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeftArg0.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<FunctionCallNode>(node.Right);
                var nodeRight = (FunctionCallNode)node.Right;
                Assert.Equal("length", nodeRight.Name);
                Assert.Equal(1, nodeRight.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeRight.Parameters[0]);
                var nodeRightArg0 = (PropertyAccessNode)nodeRight.Parameters[0];
                Assert.Equal("CompanyName", nodeRightArg0.Property.Name);
            }

            [Fact]
            public void ParseNestedFunctionCallEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("length(trim(CompanyName)) eq 50");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<FunctionCallNode>(node.Left);
                var nodeLeft = (FunctionCallNode)node.Left;
                Assert.Equal("length", nodeLeft.Name);
                Assert.Equal(1, nodeLeft.Parameters.Count);
                Assert.IsType<FunctionCallNode>(nodeLeft.Parameters[0]);
                var nodeLeftArg0 = (FunctionCallNode)nodeLeft.Parameters[0];
                Assert.Equal("trim", nodeLeftArg0.Name);
                Assert.Equal(1, nodeLeftArg0.Parameters.Count);
                Assert.IsType<PropertyAccessNode>(nodeLeftArg0.Parameters[0]);
                Assert.Equal("CompanyName", ((PropertyAccessNode)nodeLeftArg0.Parameters[0]).Property.Name);

                Assert.Equal(BinaryOperatorKind.Equal, node.OperatorKind);

                Assert.IsType<ConstantNode>(node.Right);
                Assert.Equal("50", ((ConstantNode)node.Right).LiteralText);
                Assert.IsType<int>(((ConstantNode)node.Right).Value);
                Assert.Equal(50, ((ConstantNode)node.Right).Value);
            }

            /// <summary>
            /// https://github.com/TrevorPilley/Net.Http.WebApi.OData/issues/49 - InvalidOperationException: Stack Empty thrown by FilterExpressionParser with nested grouping.
            /// </summary>
            [Fact]
            public void ParseNestedGrouping()
            {
                var queryNode = FilterExpressionParser.Parse("(Created ge 2015-03-01T00:00:00 and Created le 2015-03-31T23:59:59) and ((Enabled eq true and contains(CompanyName,'Alfreds') eq true or (Enabled eq true and contains(CompanyName,'Futterkiste') eq true)");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                //                  == Expected Tree Structure ==
                //              ---------------- and ------------------
                //             |                                       |
                //       ---- and ----                     ----------- or ------------
                //      |             |                   |                           |
                // --- ge ---    --- le ---         ---- and ----                --- and ---
                // |         |   |        |         |            |              |           |
                //                              --- eq ---   --- eq ---    --- eq ---   --- eq ---
                //                             |         |  |          |  |         |  |          |

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.Equal(BinaryOperatorKind.GreaterThanOrEqual, nodeLeftLeft.OperatorKind);
                Assert.Equal(BinaryOperatorKind.And, nodeLeft.OperatorKind);
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Right);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.Equal(BinaryOperatorKind.LessThanOrEqual, nodeLeftRight.OperatorKind);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<BinaryOperatorNode>(nodeRight.Left);
                var nodeRightLeft = (BinaryOperatorNode)nodeRight.Left;
                Assert.IsType<BinaryOperatorNode>(nodeRightLeft.Left);
                var nodeRightLeftLeft = (BinaryOperatorNode)nodeRightLeft.Left;
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeftLeft.OperatorKind);
                Assert.Equal(BinaryOperatorKind.And, nodeRightLeft.OperatorKind);
                Assert.IsType<BinaryOperatorNode>(nodeRightLeft.Right);
                var nodeRightLeftRight = (BinaryOperatorNode)nodeRightLeft.Right;
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeftRight.OperatorKind);
                Assert.Equal(BinaryOperatorKind.Or, nodeRight.OperatorKind);
                Assert.IsType<BinaryOperatorNode>(nodeRight.Right);
                var nodeRightRight = (BinaryOperatorNode)nodeRight.Right;
                Assert.IsType<BinaryOperatorNode>(nodeRightRight.Left);
                var nodeRightRightLeft = (BinaryOperatorNode)nodeRightLeft.Left;
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRightLeft.OperatorKind);
                Assert.Equal(BinaryOperatorKind.And, nodeRightRight.OperatorKind);
                Assert.IsType<BinaryOperatorNode>(nodeRightRight.Right);
                var nodeRightRightRight = (BinaryOperatorNode)nodeRightLeft.Right;
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRightRight.OperatorKind);
            }

            /// <summary>
            /// https://github.com/TrevorPilley/Net.Http.WebApi.OData/issues/36#issuecomment-70567443.
            /// </summary>
            [Fact]
            public void ParseOuterGroupedExample1()
            {
                var queryNode = FilterExpressionParser.Parse("(((FirstName eq 'andrew') and (LastName eq 'davis')) or (FirstName eq 'system'))");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("FirstName", ((PropertyAccessNode)nodeLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.Equal("andrew", ((ConstantNode)nodeLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.And, nodeLeft.OperatorKind);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("LastName", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.Equal("davis", ((ConstantNode)nodeLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                var nodeRightLeft = (PropertyAccessNode)nodeRight.Left;
                Assert.Equal("FirstName", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                var nodeRightRight = (ConstantNode)nodeRight.Right;
                Assert.Equal("system", ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParseOuterGroupedPropertyEqValueAndGroupedPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("(Forename eq 'John' and (Surname eq 'Smith' or Surname eq 'Smythe'))");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("John", ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<BinaryOperatorNode>(nodeRight.Left);
                var nodeRightLeft = (BinaryOperatorNode)nodeRight.Left;
                Assert.IsType<PropertyAccessNode>(nodeRightLeft.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRightLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightLeft.Right);
                Assert.Equal("'Smith'", ((ConstantNode)nodeRightLeft.Right).LiteralText);
                Assert.Equal(BinaryOperatorKind.Or, nodeRight.OperatorKind);
                var nodeRightRight = (BinaryOperatorNode)nodeRight.Right;
                Assert.IsType<PropertyAccessNode>(nodeRightRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRightRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightRight.Right);
                Assert.Equal("'Smythe'", ((ConstantNode)nodeRightRight.Right).LiteralText);
            }

            [Fact]
            public void ParseOuterGroupedPropertyEqValueAndPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("((Forename eq 'John' and Surname eq 'Smith') or Age eq 35)");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.And, nodeLeft.OperatorKind);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.Equal("Smith", ((ConstantNode)nodeLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                var nodeRightLeft = (PropertyAccessNode)nodeRight.Left;
                Assert.Equal("Age", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                var nodeRightRight = (ConstantNode)nodeRight.Right;
                Assert.Equal(35, ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParseOuterGroupedPropertyEqValueorPropertyEqValueAndGroupedPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("((Forename eq 'John' or Forename eq 'Joe') and (Surname eq 'Smith' or Surname eq 'Bloggs'))");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeftLeft.Right);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeLeft.OperatorKind);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeftRight.Right);
                Assert.Equal("Joe", ((ConstantNode)nodeLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<BinaryOperatorNode>(nodeRight.Left);
                var nodeRightLeft = (BinaryOperatorNode)nodeRight.Left;
                Assert.IsType<PropertyAccessNode>(nodeRightLeft.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRightLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightLeft.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeRightLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeRight.OperatorKind);
                var nodeRightRight = (BinaryOperatorNode)nodeRight.Right;
                Assert.IsType<PropertyAccessNode>(nodeRightRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRightRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightRight.Right);
                Assert.Equal("Bloggs", ((ConstantNode)nodeRightRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueAndGroupedPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and (Surname eq 'Smith' or Surname eq 'Smythe')");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("John", ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<BinaryOperatorNode>(nodeRight.Left);
                var nodeRightLeft = (BinaryOperatorNode)nodeRight.Left;
                Assert.IsType<PropertyAccessNode>(nodeRightLeft.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRightLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightLeft.Right);
                Assert.Equal("'Smith'", ((ConstantNode)nodeRightLeft.Right).LiteralText);
                Assert.Equal("Smith", ((ConstantNode)nodeRightLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeRight.OperatorKind);
                var nodeRightRight = (BinaryOperatorNode)nodeRight.Right;
                Assert.IsType<PropertyAccessNode>(nodeRightRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRightRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRightRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRightRight.Right);
                Assert.Equal("'Smythe'", ((ConstantNode)nodeRightRight.Right).LiteralText);
                Assert.Equal("Smythe", ((ConstantNode)nodeRightRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueAndPropertyEqValueAndPropertyEqValueAndPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and Surname eq 'Smith' and Age eq 35 and Title eq 'Mr'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeftLeft.Left);
                var nodeLeftLeftLeft = (BinaryOperatorNode)nodeLeftLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeftLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeftLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeftLeft.OperatorKind);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.And, nodeLeftLeft.OperatorKind);
                var nodeLeftLeftRight = (BinaryOperatorNode)nodeLeftLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeftRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeLeftLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeftRight.OperatorKind);
                Assert.Equal("Smith", ((ConstantNode)nodeLeftLeftRight.Right).Value);
                Assert.Equal(BinaryOperatorKind.And, nodeLeft.OperatorKind);
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Right);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Age", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<PropertyAccessNode>(nodeRight.Left);
                Assert.Equal("Title", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal("Mr", ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueAndPropertyEqValueAndPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and Surname eq 'Smith' and Age eq 35");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.And, nodeLeft.OperatorKind);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.Equal("Smith", ((ConstantNode)nodeLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                var nodeRightLeft = (PropertyAccessNode)nodeRight.Left;
                Assert.Equal("Age", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                var nodeRightRight = (ConstantNode)nodeRight.Right;
                Assert.Equal(35, ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueAndPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and Surname eq 'Smith'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("John", ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<PropertyAccessNode>(nodeRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueAndPropertyEqValueOrPropertyEqValueUnGroupedExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' and Surname eq 'Smith' or Age eq 35");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.And, nodeLeft.OperatorKind);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.Equal("Smith", ((ConstantNode)nodeLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                var nodeRightLeft = (PropertyAccessNode)nodeRight.Left;
                Assert.Equal("Age", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                var nodeRightRight = (ConstantNode)nodeRight.Right;
                Assert.Equal(35, ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueAndYearFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Surname eq 'Smith' and year(BirthDate) eq 1971");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.And, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<FunctionCallNode>(nodeRight.Left);
                Assert.Equal("year", ((FunctionCallNode)nodeRight.Left).Name);
                var rightNodeLeft = (FunctionCallNode)nodeRight.Left;
                Assert.IsType<PropertyAccessNode>(rightNodeLeft.Parameters[0]);
                Assert.Equal("BirthDate", ((PropertyAccessNode)rightNodeLeft.Parameters[0]).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal(1971, ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' or Surname eq 'Smith'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("John", ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<PropertyAccessNode>(nodeRight.Left);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueOrPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' or Surname eq 'Smith' or Age eq 35");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;

                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeft.OperatorKind);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeLeft.OperatorKind);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftRight.OperatorKind);
                Assert.Equal("Smith", ((ConstantNode)nodeLeftRight.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                var nodeRightLeft = (PropertyAccessNode)nodeRight.Left;
                Assert.Equal("Age", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                var nodeRightRight = (ConstantNode)nodeRight.Right;
                Assert.Equal(35, ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueOrPropertyEqValueOrPropertyEqValueOrPropertyEqValueExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Forename eq 'John' or Surname eq 'Smith' or Age eq 35 or Title eq 'Mr'");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Left);
                var nodeLeftLeft = (BinaryOperatorNode)nodeLeft.Left;
                Assert.IsType<BinaryOperatorNode>(nodeLeftLeft.Left);
                var nodeLeftLeftLeft = (BinaryOperatorNode)nodeLeftLeft.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeftLeft.Left);
                Assert.Equal("Forename", ((PropertyAccessNode)nodeLeftLeftLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeftLeft.OperatorKind);
                Assert.Equal("John", ((ConstantNode)nodeLeftLeftLeft.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeLeftLeft.OperatorKind);
                var nodeLeftLeftRight = (BinaryOperatorNode)nodeLeftLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftLeftRight.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeLeftLeftRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeftLeftRight.OperatorKind);
                Assert.Equal("Smith", ((ConstantNode)nodeLeftLeftRight.Right).Value);
                Assert.Equal(BinaryOperatorKind.Or, nodeLeft.OperatorKind);
                Assert.IsType<BinaryOperatorNode>(nodeLeft.Right);
                var nodeLeftRight = (BinaryOperatorNode)nodeLeft.Right;
                Assert.IsType<PropertyAccessNode>(nodeLeftRight.Left);
                Assert.Equal("Age", ((PropertyAccessNode)nodeLeftRight.Left).Property.Name);

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<PropertyAccessNode>(nodeRight.Left);
                Assert.Equal("Title", ((PropertyAccessNode)nodeRight.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal("Mr", ((ConstantNode)nodeRight.Right).Value);
            }

            [Fact]
            public void ParsePropertyEqValueOrYearFunctionExpression()
            {
                var queryNode = FilterExpressionParser.Parse("Surname eq 'Smith' or year(BirthDate) eq 1971");

                Assert.NotNull(queryNode);
                Assert.IsType<BinaryOperatorNode>(queryNode);

                var node = (BinaryOperatorNode)queryNode;

                Assert.IsType<BinaryOperatorNode>(node.Left);
                var nodeLeft = (BinaryOperatorNode)node.Left;
                Assert.IsType<PropertyAccessNode>(nodeLeft.Left);
                Assert.Equal("Surname", ((PropertyAccessNode)nodeLeft.Left).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeLeft.OperatorKind);
                Assert.IsType<ConstantNode>(nodeLeft.Right);
                Assert.Equal("Smith", ((ConstantNode)nodeLeft.Right).Value);

                Assert.Equal(BinaryOperatorKind.Or, node.OperatorKind);

                Assert.IsType<BinaryOperatorNode>(node.Right);
                var nodeRight = (BinaryOperatorNode)node.Right;
                Assert.IsType<FunctionCallNode>(nodeRight.Left);
                Assert.Equal("year", ((FunctionCallNode)nodeRight.Left).Name);
                var rightNodeLeft = (FunctionCallNode)nodeRight.Left;
                Assert.IsType<PropertyAccessNode>(rightNodeLeft.Parameters[0]);
                Assert.Equal("BirthDate", ((PropertyAccessNode)rightNodeLeft.Parameters[0]).Property.Name);
                Assert.Equal(BinaryOperatorKind.Equal, nodeRight.OperatorKind);
                Assert.IsType<ConstantNode>(nodeRight.Right);
                Assert.Equal(1971, ((ConstantNode)nodeRight.Right).Value);
            }
        }
    }
}