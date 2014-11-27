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
namespace Net.Http.WebApi.OData.Query.Expressions
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    internal static class FilterExpressionParser
    {
        private static readonly string[] DateTimeFormats = new[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm", "s", "o" };
        private static readonly Regex DecimalRegex = new Regex(@"^(-)?\d+.\d+(m|M)$", RegexOptions.Compiled | RegexOptions.Singleline);
        private static readonly Regex DoubleRegex = new Regex(@"^(-)?\d+.\d+(d|D)$", RegexOptions.Compiled | RegexOptions.Singleline);
        private static readonly Regex FunctionCallRegex = new Regex(@"^(?<Function>[a-z/]*)\(((?<Property>[A-Za-z/]*)(, '?(?<Argument>[^'\(\),]*)'?)*\)|('?(?<Argument>[^'\(\)]*)'?)?, (?<Property>[A-Za-z/]*)\)) (?<Operator>eq|ne|gt|ge|lt|le) (?:'?)(?<Value>[^']*)(?:'?)$", RegexOptions.Compiled | RegexOptions.Singleline);
        private static readonly Regex PropertyAccessRegex = new Regex(@"^(?<Property>[A-Za-z/]*)? ?(?<Operator>eq|ne|gt|ge|lt|le|add|sub|mul|div|mod) (?<DataType>datetime|guid)?(?:'?)((?<Value>[^']*)(?:'?)|(?<Value>[^'\s]*)(?:'?) (?<Comparison>.*)?)$", RegexOptions.Compiled | RegexOptions.Singleline);
        private static readonly Regex SingleRegex = new Regex(@"^(-)?\d+.\d+(f|F)$", RegexOptions.Compiled | RegexOptions.Singleline);

        internal static QueryNode Parse(string filterValue)
        {
            QueryNode parent = null;

            int workingEndPosition = filterValue.Length;

            for (int i = filterValue.Length; i > 0; i--)
            {
                if (i < 4)
                {
                    var singleValueNode = ParseSingleValueNode(filterValue.Substring(i - i, workingEndPosition));

                    if (parent == null)
                    {
                        parent = singleValueNode;
                    }
                    else
                    {
                        ((BinaryOperatorNode)parent).Left = singleValueNode;
                    }

                    break;
                }

                if (PreceededByAnd(filterValue, i))
                {
                    var singleValueNode = ParseSingleValueNode(filterValue.Substring(i, workingEndPosition - i));

                    UpdateExpressionTree(ref parent, BinaryOperatorKind.And, singleValueNode);

                    workingEndPosition = i - 5;
                }
                else if (PreceededByOr(filterValue, i))
                {
                    var singleValueNode = ParseSingleValueNode(filterValue.Substring(i, workingEndPosition - i));

                    UpdateExpressionTree(ref parent, BinaryOperatorKind.Or, singleValueNode);

                    workingEndPosition = i - 4;
                }
            }

            return parent;
        }

        private static object GetValue(string dataType, string literalValue)
        {
            switch (dataType)
            {
                case "datetime":
                    return DateTime.ParseExact(literalValue, DateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);

                case "guid":
                    return Guid.ParseExact(literalValue, "D");
            }

            switch (literalValue)
            {
                case "false":
                    return false;

                case "true":
                    return true;

                case "null":
                    return null;

                default:
                    if (DoubleRegex.IsMatch(literalValue))
                    {
                        return double.Parse(literalValue.Remove(literalValue.Length - 1, 1), CultureInfo.InvariantCulture);
                    }

                    if (DecimalRegex.IsMatch(literalValue))
                    {
                        return decimal.Parse(literalValue.Remove(literalValue.Length - 1, 1), CultureInfo.InvariantCulture);
                    }

                    if (SingleRegex.IsMatch(literalValue))
                    {
                        return float.Parse(literalValue.Remove(literalValue.Length - 1, 1), CultureInfo.InvariantCulture);
                    }

                    int integerValue;
                    if (int.TryParse(literalValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out integerValue))
                    {
                        return integerValue;
                    }

                    return literalValue;
            }
        }

        private static SingleValueNode ParseSingleValueFunctionCall(Match functionCallMatch)
        {
            var function = functionCallMatch.Groups["Function"].Value;
            var propertyName = functionCallMatch.Groups["Property"].Value;
            var argumentCaptures = functionCallMatch.Groups["Argument"].Captures;
            var operatorType = functionCallMatch.Groups["Operator"].Value;
            var valueLiteral = functionCallMatch.Groups["Value"].Value;

            var propertyNode = new SingleValuePropertyAccessNode(propertyName);
            var arguments = new QueryNode[argumentCaptures.Count > 0 ? argumentCaptures.Count + 1 : 2];

            if (function == "substringof")
            {
                arguments[0] = new ConstantNode(argumentCaptures[0].Value, GetValue(string.Empty, argumentCaptures[0].Value));
                arguments[1] = propertyNode;
            }
            else
            {
                arguments[0] = propertyNode;

                if (argumentCaptures.Count == 0)
                {
                    arguments[1] = new ConstantNode(string.Empty, string.Empty);
                }

                for (int i = 0; i < argumentCaptures.Count; i++)
                {
                    arguments[i + 1] = new ConstantNode(argumentCaptures[i].Value, GetValue(string.Empty, argumentCaptures[i].Value));
                }
            }

            var functionNode = new SingleValueFunctionCallNode(function, arguments);
            var operatorKind = BinaryOperatorKindParser.ToBinaryOperatorKind(operatorType);
            var valueConstantNode = new ConstantNode(valueLiteral, GetValue(string.Empty, valueLiteral));

            return new BinaryOperatorNode(functionNode, operatorKind, valueConstantNode);
        }

        private static SingleValueNode ParseSingleValueNode(string expression)
        {
            var propertyAccessMatch = PropertyAccessRegex.Match(expression);

            if (propertyAccessMatch.Success)
            {
                return ParseSingleValuePropertyAccess(propertyAccessMatch);
            }

            var functionCallMatch = FunctionCallRegex.Match(expression);

            if (functionCallMatch.Success)
            {
                return ParseSingleValueFunctionCall(functionCallMatch);
            }

            throw new ODataException("The expression '" + expression + "' is not currently supported.");
        }

        private static SingleValueNode ParseSingleValuePropertyAccess(Match propertyAccessMatch)
        {
            SingleValueNode result = null;

            var propertyName = propertyAccessMatch.Groups["Property"].Value;
            var operatorType = propertyAccessMatch.Groups["Operator"].Value;
            var dataType = propertyAccessMatch.Groups["DataType"].Value;
            var value = propertyAccessMatch.Groups["Value"].Value;

            var propertyNode = new SingleValuePropertyAccessNode(propertyName);
            var operatorKind = BinaryOperatorKindParser.ToBinaryOperatorKind(operatorType);
            var valueConstantNode = new ConstantNode(value, GetValue(dataType, value));

            result = new BinaryOperatorNode(propertyNode, operatorKind, valueConstantNode);

            var comparison = propertyAccessMatch.Groups["Comparison"].Value;

            if (!string.IsNullOrEmpty(comparison))
            {
                var comparisonMatch = PropertyAccessRegex.Match(comparison);

                if (comparisonMatch.Success)
                {
                    var rightOperatorType = comparisonMatch.Groups["Operator"].Value;
                    var rightDataType = comparisonMatch.Groups["DataType"].Value;
                    var rightValue = comparisonMatch.Groups["Value"].Value;

                    var rightOperatorKind = BinaryOperatorKindParser.ToBinaryOperatorKind(rightOperatorType);
                    var rightValueConstantNode = new ConstantNode(rightValue, GetValue(rightDataType, rightValue));

                    result = new BinaryOperatorNode(result, rightOperatorKind, rightValueConstantNode);
                }
            }

            return result;
        }

        private static bool PreceededByAnd(string filterValue, int position)
        {
            return filterValue[position - 1] == ' '
                    && filterValue[position - 2] == 'd'
                    && filterValue[position - 3] == 'n'
                    && filterValue[position - 4] == 'a'
                    && filterValue[position - 5] == ' ';
        }

        private static bool PreceededByOr(string filterValue, int position)
        {
            return filterValue[position - 1] == ' '
                    && filterValue[position - 2] == 'r'
                    && filterValue[position - 3] == 'o'
                    && filterValue[position - 4] == ' ';
        }

        private static void UpdateExpressionTree(ref QueryNode parent, BinaryOperatorKind binaryOperatorKind, SingleValueNode singleValueNode)
        {
            var binaryParent = parent as BinaryOperatorNode;

            if (binaryParent != null)
            {
                binaryParent.Left = singleValueNode;

                parent = new BinaryOperatorNode
                {
                    OperatorKind = binaryOperatorKind,
                    Right = binaryParent
                };
            }
            else if (parent == null)
            {
                parent = new BinaryOperatorNode
                {
                    OperatorKind = binaryOperatorKind,
                    Right = singleValueNode
                };
            }
        }
    }
}