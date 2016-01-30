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

                            if (stack.Count > 0)
                            {
                                var lastNode = stack.Pop();

                                if (stack.Count > 0)
                                {
                                    stack.Peek().Parameters.Add(lastNode);
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
                            }

                            break;

                        case TokenType.FunctionName:
                            node = new SingleValueFunctionCallNode(token.Value);
                            break;

                        case TokenType.LogicalOperator:
                            binaryNode = new BinaryOperatorNode(node, BinaryOperatorKindParser.ToBinaryOperatorKind(token.Value), null);
                            break;

                        case TokenType.PropertyName:
                            if (stack.Count > 0)
                            {
                                stack.Peek().Parameters.Add(new SingleValuePropertyAccessNode(token.Value));
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
                        case TokenType.Int32:
                        case TokenType.Int64:
                        case TokenType.DateTime:
                        case TokenType.DateTimeOffset:
                        case TokenType.Guid:
                        case TokenType.String:
                        case TokenType.Null:
                        case TokenType.Time:
                        case TokenType.True:
                            if (stack.Count > 0)
                            {
                                stack.Peek().Parameters.Add(ConstantNodeParser.ParseConstantNode(token));
                            }
                            else
                            {
                                binaryNode.Right = ConstantNodeParser.ParseConstantNode(token);
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
                        case TokenType.Int32:
                        case TokenType.Int64:
                        case TokenType.DateTime:
                        case TokenType.DateTimeOffset:
                        case TokenType.Guid:
                        case TokenType.String:
                        case TokenType.Null:
                        case TokenType.Time:
                        case TokenType.True:
                            rightNode = ConstantNodeParser.ParseConstantNode(token);
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