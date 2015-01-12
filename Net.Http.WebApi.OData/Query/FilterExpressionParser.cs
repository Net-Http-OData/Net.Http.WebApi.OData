// -----------------------------------------------------------------------
// <copyright file="FilterExpressionParser.cs" company="Project Contributors">
// Copyright 2012-2013 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData.Query
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Net.Http.WebApi.OData.Query.Expressions;

    internal static class FilterExpressionParser
    {
        private static readonly string[] DateTimeFormats = new[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm", "s", "o" };

        internal static QueryNode Parse(string filterValue)
        {
            var parserImpl = new FilterExpressionParserImpl();
            var queryNode = parserImpl.Parse(new Lexer(filterValue));

            return queryNode;
        }

        private class FilterExpressionParserImpl
        {
            private readonly Queue<Token> tokens = new Queue<Token>();
            private SingleValueNode rootNode;

            public FilterExpressionParserImpl()
            {
            }

            public SingleValueNode Parse(Lexer lexer)
            {
                while (lexer.MoveNext())
                {
                    var token = lexer.Current;

                    if (token.TokenType == TokenType.And)
                    {
                        this.UpdateExpressionTree(BinaryOperatorKind.And);
                    }
                    else if (token.TokenType == TokenType.Or)
                    {
                        this.UpdateExpressionTree(BinaryOperatorKind.Or);
                    }
                    else
                    {
                        this.tokens.Enqueue(token);
                    }
                }

                this.UpdateExpressionTree(BinaryOperatorKind.None);

                return this.rootNode;
            }

            private static object GetValue(Token token)
            {
                string literalValue;

                switch (token.TokenType)
                {
                    case TokenType.Decimal:
                        literalValue = token.Value.Remove(token.Value.Length - 1, 1);
                        return decimal.Parse(literalValue, CultureInfo.InvariantCulture);

                    case TokenType.Double:
                        literalValue = token.Value.Remove(token.Value.Length - 1, 1);
                        return double.Parse(literalValue, CultureInfo.InvariantCulture);

                    case TokenType.False:
                        return false;

                    case TokenType.Single:
                        literalValue = token.Value.Remove(token.Value.Length - 1, 1);
                        return float.Parse(literalValue, CultureInfo.InvariantCulture);

                    case TokenType.Integer:
                        return int.Parse(token.Value, CultureInfo.InvariantCulture);

                    case TokenType.Null:
                        return null;

                    case TokenType.DateTime:
                        literalValue = token.Value.Trim('\'');
                        return DateTime.ParseExact(literalValue, DateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                    case TokenType.Guid:
                        literalValue = token.Value.Trim('\'');
                        return Guid.ParseExact(literalValue, "D");

                    case TokenType.String:
                        return token.Value.Trim('\'');

                    case TokenType.True:
                        return true;

                    default:
                        throw new NotSupportedException(token.TokenType.ToString());
                }
            }

            private SingleValueNode ParseSingleValueFunctionCall()
            {
                BinaryOperatorNode binaryNode = null;
                SingleValueFunctionCallNode node = null;

                var stack = new Stack<SingleValueFunctionCallNode>();

                while (this.tokens.Count > 0)
                {
                    var token = this.tokens.Dequeue();

                    switch (token.TokenType)
                    {
                        case TokenType.OpenParentheses:
                            stack.Push(node);
                            break;

                        case TokenType.CloseParentheses:
                            var lastNode = stack.Pop();

                            if (stack.Count > 0)
                            {
                                stack.Peek().Arguments.Add(lastNode);
                            }
                            else
                            {
                                if (binaryNode != null)
                                {
                                    binaryNode.Right = lastNode;
                                }
                                else
                                {
                                    node = lastNode;
                                }
                            }

                            break;

                        case TokenType.FunctionName:
                            node = new SingleValueFunctionCallNode(token.Value, new List<QueryNode>());
                            break;

                        case TokenType.LogicalOperator:
                            binaryNode = new BinaryOperatorNode
                            {
                                Left = node,
                                OperatorKind = BinaryOperatorKindParser.ToBinaryOperatorKind(token.Value)
                            };
                            break;

                        case TokenType.PropertyName:
                            if (stack.Count > 0)
                            {
                                stack.Peek().Arguments.Add(new SingleValuePropertyAccessNode(token.Value));
                            }
                            else
                            {
                                binaryNode.Right = new SingleValuePropertyAccessNode(token.Value);
                            }

                            break;

                        case TokenType.Decimal:
                        case TokenType.Double:
                        case TokenType.False:
                        case TokenType.Single:
                        case TokenType.Integer:
                        case TokenType.DateTime:
                        case TokenType.Guid:
                        case TokenType.String:
                        case TokenType.Null:
                        case TokenType.True:
                            if (stack.Count > 0)
                            {
                                node.Arguments.Add(new ConstantNode(token.Value.Trim('\''), GetValue(token)));
                            }
                            else
                            {
                                binaryNode.Right = new ConstantNode(token.Value.Trim('\''), GetValue(token));
                            }

                            break;
                    }
                }

                if (binaryNode != null)
                {
                    return binaryNode;
                }

                return node;
            }

            private SingleValueNode ParseSingleValueNode()
            {
                SingleValueNode node = null;

                switch (this.tokens.Peek().TokenType)
                {
                    case TokenType.FunctionName:
                        node = this.ParseSingleValueFunctionCall();
                        break;

                    case TokenType.PropertyName:
                        node = this.ParseSingleValuePropertyAccess();
                        break;

                    default:
                        throw new NotSupportedException(this.tokens.Peek().TokenType.ToString());
                }

                return node;
            }

            private SingleValueNode ParseSingleValuePropertyAccess()
            {
                SingleValueNode result = null;

                SingleValueNode leftNode = null;
                BinaryOperatorKind operatorKind = BinaryOperatorKind.None;
                SingleValueNode rightNode = null;

                while (this.tokens.Count > 0)
                {
                    var token = this.tokens.Dequeue();

                    switch (token.TokenType)
                    {
                        case TokenType.ArithmeticOperator:
                        case TokenType.LogicalOperator:
                            if (operatorKind != BinaryOperatorKind.None)
                            {
                                result = new BinaryOperatorNode(leftNode, operatorKind, rightNode);
                                leftNode = null;
                                rightNode = null;
                            }

                            operatorKind = BinaryOperatorKindParser.ToBinaryOperatorKind(token.Value);
                            break;

                        case TokenType.PropertyName:
                            if (leftNode == null)
                            {
                                leftNode = new SingleValuePropertyAccessNode(token.Value);
                            }
                            else if (rightNode == null)
                            {
                                rightNode = new SingleValuePropertyAccessNode(token.Value);
                            }

                            break;

                        case TokenType.Decimal:
                        case TokenType.Double:
                        case TokenType.False:
                        case TokenType.Single:
                        case TokenType.Integer:
                        case TokenType.DateTime:
                        case TokenType.Guid:
                        case TokenType.String:
                        case TokenType.Null:
                        case TokenType.True:
                            rightNode = new ConstantNode(token.Value.Trim('\''), GetValue(token));
                            break;
                    }
                }

                result = result == null
                    ? new BinaryOperatorNode(leftNode, operatorKind, rightNode)
                    : new BinaryOperatorNode(result, operatorKind, leftNode ?? rightNode);

                return result;
            }

            private void UpdateExpressionTree(BinaryOperatorKind binaryOperatorKind)
            {
                SingleValueNode node = this.ParseSingleValueNode();

                if (this.rootNode == null)
                {
                    if (binaryOperatorKind == BinaryOperatorKind.None)
                    {
                        this.rootNode = node;
                    }
                    else
                    {
                        this.rootNode = new BinaryOperatorNode
                        {
                            Left = node,
                            OperatorKind = binaryOperatorKind
                        };
                    }
                }
                else
                {
                    var binaryParent = (BinaryOperatorNode)this.rootNode;

                    if (binaryParent.Right == null)
                    {
                        binaryParent.Right = node;
                    }

                    if (binaryOperatorKind != BinaryOperatorKind.None)
                    {
                        this.rootNode = new BinaryOperatorNode
                        {
                            Left = binaryParent,
                            OperatorKind = binaryOperatorKind,
                        };
                    }
                }
            }
        }
    }
}