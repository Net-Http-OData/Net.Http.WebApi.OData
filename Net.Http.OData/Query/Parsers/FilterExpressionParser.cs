﻿// -----------------------------------------------------------------------
// <copyright file="FilterExpressionParser.cs" company="Project Contributors">
// Copyright 2012 - 2020 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.OData.Query.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Net.Http.OData.Model;
    using Net.Http.OData.Query.Expressions;

    internal static class FilterExpressionParser
    {
        internal static QueryNode Parse(string filterValue, EdmComplexType model)
        {
            var parserImpl = new FilterExpressionParserImpl(model);
            var queryNode = parserImpl.Parse(new Lexer(filterValue));

            return queryNode;
        }

        private sealed class FilterExpressionParserImpl
        {
            private readonly EdmComplexType model;
            private readonly Stack<QueryNode> nodeStack = new Stack<QueryNode>();
            private readonly Queue<Token> tokens = new Queue<Token>();
            private int groupingDepth;
            private BinaryOperatorKind nextBinaryOperatorKind = BinaryOperatorKind.None;

            internal FilterExpressionParserImpl(EdmComplexType model)
            {
                this.model = model;
            }

            internal QueryNode Parse(Lexer lexer)
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

                if (this.groupingDepth != 0 || this.nodeStack.Count != 1)
                {
                    throw new ODataException(HttpStatusCode.BadRequest, "Unable to parse the specified $filter system query option, an extra opening or missing closing parenthesis may be present");
                }

                var node = this.nodeStack.Pop();

                if (node is BinaryOperatorNode binaryNode && (binaryNode.Left is null || binaryNode.Right is null))
                {
                    throw new ODataException(HttpStatusCode.BadRequest, $"Unable to parse the specified $filter system query option, the binary operator {binaryNode.OperatorKind.ToString()} has no {(binaryNode.Left is null ? "left" : "right")} node");
                }

                return node;
            }

            private QueryNode ParseFunctionCallNode()
            {
                BinaryOperatorNode binaryNode = null;
                FunctionCallNode node = null;

                var stack = new Stack<FunctionCallNode>();

                while (this.tokens.Count > 0)
                {
                    var token = this.tokens.Dequeue();

                    switch (token.TokenType)
                    {
                        case TokenType.OpenParentheses:
                            if (this.tokens.Count > 0 && this.tokens.Peek().TokenType == TokenType.CloseParentheses)
                            {
                                // All OData functions have at least 1 or 2 parameters
                                throw new ODataException(HttpStatusCode.BadRequest, $"Unable to parse the specified $filter system query option, the function {node?.Name} has no parameters");
                            }

                            this.groupingDepth++;
                            stack.Push(node);
                            break;

                        case TokenType.CloseParentheses:
                            if (this.groupingDepth == 0)
                            {
                                throw new ODataException(HttpStatusCode.BadRequest, "Unable to parse the specified $filter system query option, closing parenthesis not expected");
                            }

                            this.groupingDepth--;

                            if (stack.Count > 0)
                            {
                                var lastNode = stack.Pop();

                                if (stack.Count > 0)
                                {
                                    stack.Peek().AddParameter(lastNode);
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
                            node = new FunctionCallNode(token.Value);
                            break;

                        case TokenType.BinaryOperator:
                            binaryNode = new BinaryOperatorNode(node, token.Value.ToBinaryOperatorKind(), null);
                            break;

                        case TokenType.PropertyName:
                            var propertyAccessNode = new PropertyAccessNode(PropertyPathSegment.For(token.Value, this.model));

                            if (stack.Count > 0)
                            {
                                stack.Peek().AddParameter(propertyAccessNode);
                            }
                            else
                            {
                                binaryNode.Right = propertyAccessNode;
                            }

                            break;

                        case TokenType.Date:
                        case TokenType.DateTimeOffset:
                        case TokenType.Decimal:
                        case TokenType.Double:
                        case TokenType.Duration:
                        case TokenType.Enum:
                        case TokenType.False:
                        case TokenType.Guid:
                        case TokenType.Integer:
                        case TokenType.Null:
                        case TokenType.Single:
                        case TokenType.String:
                        case TokenType.TimeOfDay:
                        case TokenType.True:
                            var constantNode = ConstantNodeParser.ParseConstantNode(token);

                            if (stack.Count > 0)
                            {
                                stack.Peek().AddParameter(constantNode);
                            }
                            else
                            {
                                binaryNode.Right = constantNode;
                            }

                            break;

                        case TokenType.Comma:
                            if (this.tokens.Count > 0 && this.tokens.Peek().TokenType == TokenType.CloseParentheses)
                            {
                                // If there is a comma in a function call, there should be another parameter followed by a closing comma
                                throw new ODataException(HttpStatusCode.BadRequest, $"Unable to parse the specified $filter system query option, the function {node?.Name} has a missing parameter or extra comma");
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

            private QueryNode ParsePropertyAccessNode()
            {
                QueryNode result = null;

                QueryNode leftNode = null;
                BinaryOperatorKind operatorKind = BinaryOperatorKind.None;
                QueryNode rightNode = null;

                while (this.tokens.Count > 0)
                {
                    var token = this.tokens.Dequeue();

                    switch (token.TokenType)
                    {
                        case TokenType.BinaryOperator:
                            if (operatorKind != BinaryOperatorKind.None)
                            {
                                result = new BinaryOperatorNode(leftNode, operatorKind, rightNode);
                                leftNode = null;
                                rightNode = null;
                            }

                            operatorKind = token.Value.ToBinaryOperatorKind();
                            break;

                        case TokenType.OpenParentheses:
                            this.groupingDepth++;
                            break;

                        case TokenType.CloseParentheses:
                            this.groupingDepth--;
                            break;

                        case TokenType.FunctionName:
                            if (leftNode is null)
                            {
                                leftNode = new FunctionCallNode(token.Value);
                            }
                            else if (rightNode is null)
                            {
                                rightNode = new FunctionCallNode(token.Value);
                            }

                            break;

                        case TokenType.PropertyName:
                            var propertyAccessNode = new PropertyAccessNode(PropertyPathSegment.For(token.Value, this.model));

                            if (leftNode is null)
                            {
                                leftNode = propertyAccessNode;
                            }
                            else if (rightNode is null)
                            {
                                rightNode = propertyAccessNode;
                            }

                            break;

                        case TokenType.Date:
                        case TokenType.DateTimeOffset:
                        case TokenType.Decimal:
                        case TokenType.Double:
                        case TokenType.Duration:
                        case TokenType.Enum:
                        case TokenType.False:
                        case TokenType.Guid:
                        case TokenType.Integer:
                        case TokenType.Null:
                        case TokenType.Single:
                        case TokenType.String:
                        case TokenType.TimeOfDay:
                        case TokenType.True:
                            rightNode = ConstantNodeParser.ParseConstantNode(token);
                            break;
                    }
                }

                result = result is null
                    ? new BinaryOperatorNode(leftNode, operatorKind, rightNode)
                    : new BinaryOperatorNode(result, operatorKind, leftNode ?? rightNode);

                return result;
            }

            private QueryNode ParseQueryNode()
            {
                if (this.tokens.Count == 0)
                {
                    throw new ODataException(HttpStatusCode.BadRequest, "Unable to parse the specified $filter system query option, an incomplete filter has been specified");
                }

                QueryNode node;

                switch (this.tokens.Peek().TokenType)
                {
                    case TokenType.FunctionName:
                        node = this.ParseFunctionCallNode();
                        break;

                    case TokenType.UnaryOperator:
                        var token = this.tokens.Dequeue();
                        node = this.ParseQueryNode();
                        node = new UnaryOperatorNode(node, token.Value.ToUnaryOperatorKind());
                        break;

                    case TokenType.OpenParentheses:
                        this.groupingDepth++;
                        this.tokens.Dequeue();
                        node = this.ParseQueryNode();
                        break;

                    case TokenType.PropertyName:
                        node = this.ParsePropertyAccessNode();
                        break;

                    default:
                        throw new NotSupportedException(this.tokens.Peek().TokenType.ToString());
                }

                return node;
            }

            private void UpdateExpressionTree()
            {
                var initialGroupingDepth = this.groupingDepth;

                var node = this.ParseQueryNode();

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

                        if (leftNode is BinaryOperatorNode binaryNode && binaryNode.Right is null)
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
                        if (this.groupingDepth == 0 && this.nodeStack.Count > 0)
                        {
                            var binaryParent = (BinaryOperatorNode)this.nodeStack.Pop();
                            binaryParent.Right = binaryNode;

                            binaryNode = binaryParent;
                        }

                        this.nodeStack.Push(new BinaryOperatorNode(binaryNode, this.nextBinaryOperatorKind, null));
                    }
                }
            }
        }
    }
}