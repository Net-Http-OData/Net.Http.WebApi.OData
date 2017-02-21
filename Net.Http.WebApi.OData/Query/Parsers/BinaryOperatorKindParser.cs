// -----------------------------------------------------------------------
// <copyright file="BinaryOperatorKindParser.cs" company="Project Contributors">
// Copyright 2012 - 2017 Project Contributors
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
    using Expressions;

    internal static class BinaryOperatorKindParser
    {
        internal static BinaryOperatorKind ToBinaryOperatorKind(this string operatorType)
        {
            switch (operatorType)
            {
                case "add":
                    return BinaryOperatorKind.Add;

                case "and":
                    return BinaryOperatorKind.And;

                case "div":
                    return BinaryOperatorKind.Divide;

                case "eq":
                    return BinaryOperatorKind.Equal;

                case "ge":
                    return BinaryOperatorKind.GreaterThanOrEqual;

                case "gt":
                    return BinaryOperatorKind.GreaterThan;

                case "le":
                    return BinaryOperatorKind.LessThanOrEqual;

                case "lt":
                    return BinaryOperatorKind.LessThan;

                case "mul":
                    return BinaryOperatorKind.Multiply;

                case "mod":
                    return BinaryOperatorKind.Modulo;

                case "ne":
                    return BinaryOperatorKind.NotEqual;

                case "or":
                    return BinaryOperatorKind.Or;

                case "sub":
                    return BinaryOperatorKind.Subtract;

                default:
                    throw new ODataException(Messages.UnknownOperator.FormatWith(operatorType));
            }
        }
    }
}