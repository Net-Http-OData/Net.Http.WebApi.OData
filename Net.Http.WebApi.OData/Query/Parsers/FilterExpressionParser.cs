// -----------------------------------------------------------------------
// <copyright file="FilterExpressionParser.cs" company="Project Contributors">
// Copyright 2012 - 2015 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData.Query.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Net.Http.WebApi.OData.Query.Expressions;

    internal static class FilterExpressionParser
    {
        internal static QueryNode Parse(string filterValue)
        {
            var parserImpl = new FilterExpressionParserImpl();
            var queryNode = parserImpl.Parse(new Lexer(filterValue));

            return queryNode;
        }

        private sealed class FilterExpressionParserImpl
        {
            private static readonly string[] DateTimeFormats = new[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm", "s", "o" };

            private readonly Stack<SingleValueNode> nodeStack = new Stack<SingleValueNode>();
            private readonly Queue<Token> tokens = new Queue<Token>();
            private int groupingDepth;
            private BinaryOperatorKind nextBinaryOperatorKind = BinaryOperatorKind.None;

            internal FilterExpressionParserImpl()
            {
            }

            internal SingleValueNode Parse(Lexer lexer)
            {
                while (lexer.MoveNext())
                {
                    var token = lexer.Current;

                    switch (token.TokenType)
                    {
                        case TokenType.And:
                            this.nextBinaryOperatorKind = BinaryOperatorKind.And;
                            this.UpdateExpressionTree();
                            break;

                        case TokenType.Or:
                            this.nextBinaryOperatorKind = BinaryOperatorKind.Or;
                            this.UpdateExpressionTree();
                            break;

                        default:
                            this.tokens.Enqueue(token);
                            break;
                    }
                }

                this.nextBinaryOperatorKind = BinaryOperatorKind.None;
                this.UpdateExpressionTree();

                return this.nodeStack.Pop();
            }

            private static ConstantNode ParseConstantNode(Token token)
            {
                switch (token.TokenType)
                {
                    case TokenType.Decimal:
                        var decimalText = token.Value.Remove(token.Value.Length - 1, 1);
                        var decimalValue = decimal.Parse(decimalText, CultureInfo.InvariantCulture);
                        return new ConstantNode(token.Value, decimalValue);

                    case TokenType.Double:
                        var doubleText = token.Value.Remove(token.Value.Length - 1, 1);
                        var doubleValue = double.Parse(doubleText, CultureInfo.InvariantCulture);
                        return new ConstantNode(token.Value, doubleValue);

                    case TokenType.False:
                        return new ConstantNode(token.Value, false);

                    case TokenType.Single:
                        var singleText = token.Value.Remove(token.Value.Length - 1, 1);
                        var singleValue = float.Parse(singleText, CultureInfo.InvariantCulture);
                        return new ConstantNode(token.Value, singleValue);

                    case TokenType.Integer:
                        var integerValue = int.Parse(token.Value, CultureInfo.InvariantCulture);
                        return new ConstantNode(token.Value, integerValue);

                    case TokenType.Null:
                        return new ConstantNode(token.Value, null);

                    case TokenType.DateTime:
                        var dateText = token.Value.Trim('\'');
                        var dateValue = DateTime.ParseExact(dateText, DateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                        return new ConstantNode(dateText, dateValue);

                    case TokenType.Guid:
                        var guidText = token.Value.Trim('\'');
                        var guidValue = Guid.ParseExact(guidText, "D");
                        return new ConstantNode(guidText, guidValue);

                    case TokenType.String:
                        var stringText = token.Value.Trim('\'').Replace("''", "'");
                        return new ConstantNode(stringText, stringText);

                    case TokenType.True:
                        return new ConstantNode(token.Value, true);

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
                            this.groupingDepth++;
                            stack.Push(node);
                            break;

                        case TokenType.CloseParentheses:
                            this.groupingDepth--;
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
                                node.Arguments.Add(ParseConstantNode(token));
                            }
                            else
                            {
                                binaryNode.Right = ParseConstantNode(token);
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

                    case TokenType.UnaryOperator:
                        var token = this.tokens.Dequeue();
                        node = this.ParseSingleValueNode();
                        node = new UnaryOperatorNode(node, UnaryOperatorKindParser.ToUnaryOperatorKind(token.Value));
                        break;

                    case TokenType.OpenParentheses:
                        this.groupingDepth++;
                        this.tokens.Dequeue();
                        node = this.ParseSingleValueNode();
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

                        case TokenType.CloseParentheses:
                            this.groupingDepth--;
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
                            rightNode = ParseConstantNode(token);
                            break;
                    }
                }

                result = result == null
                    ? new BinaryOperatorNode(leftNode, operatorKind, rightNode)
                    : new BinaryOperatorNode(result, operatorKind, leftNode ?? rightNode);

                return result;
            }

            private void UpdateExpressionTree()
            {
                var initialGroupingDepth = this.groupingDepth;

                var node = this.ParseSingleValueNode();

                if (this.groupingDepth == initialGroupingDepth)
                {
                    if (this.nodeStack.Count == 0)
                    {
                        if (this.nextBinaryOperatorKind == BinaryOperatorKind.None)
                        {
                            this.nodeStack.Push(node);
                        }
                        else
                        {
                            this.nodeStack.Push(new BinaryOperatorNode(node, this.nextBinaryOperatorKind, null));
                        }
                    }
                    else
                    {
                        var leftNode = this.nodeStack.Pop();

                        var binaryNode = leftNode as BinaryOperatorNode;

                        if (binaryNode != null && binaryNode.Right == null)
                        {
                            binaryNode.Right = node;

                            if (this.nextBinaryOperatorKind != BinaryOperatorKind.None)
                            {
                                binaryNode = new BinaryOperatorNode(binaryNode, this.nextBinaryOperatorKind, null);
                            }
                        }
                        else
                        {
                            binaryNode = new BinaryOperatorNode(leftNode, this.nextBinaryOperatorKind, node);
                        }

                        this.nodeStack.Push(binaryNode);
                    }
                }
                else if (this.groupingDepth > initialGroupingDepth)
                {
                    this.nodeStack.Push(new BinaryOperatorNode(node, this.nextBinaryOperatorKind, null));
                }
                else if (this.groupingDepth < initialGroupingDepth)
                {
                    var binaryNode = (BinaryOperatorNode)this.nodeStack.Pop();
                    binaryNode.Right = node;

                    if (this.nextBinaryOperatorKind == BinaryOperatorKind.None)
                    {
                        this.nodeStack.Push(binaryNode);

                        while (this.nodeStack.Count > 1)
                        {
                            var rightNode = this.nodeStack.Pop();

                            var binaryParent = (BinaryOperatorNode)this.nodeStack.Pop();
                            binaryParent.Right = rightNode;

                            this.nodeStack.Push(binaryParent);
                        }
                    }
                    else
                    {
                        this.nodeStack.Push(new BinaryOperatorNode(binaryNode, this.nextBinaryOperatorKind, null));
                    }
                }
            }
        }
    }
}