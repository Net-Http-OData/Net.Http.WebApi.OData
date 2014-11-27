// -----------------------------------------------------------------------
// <copyright file="BinaryOperatorKindParser.cs" company="Project Contributors">
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
    internal static class BinaryOperatorKindParser
    {
        internal static BinaryOperatorKind ToBinaryOperatorKind(string operatorType)
        {
            switch (operatorType)
            {
                case "and":
                    return BinaryOperatorKind.And;

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

                case "ne":
                    return BinaryOperatorKind.NotEqual;

                case "or":
                    return BinaryOperatorKind.Or;

                case "add":
                    return BinaryOperatorKind.Add;

                case "sub":
                    return BinaryOperatorKind.Subtract;

                case "mul":
                    return BinaryOperatorKind.Multiply;

                case "div":
                    return BinaryOperatorKind.Divide;

                case "mod":
                    return BinaryOperatorKind.Modulo;

                default:
                    throw new ODataException("The operator type '" + operatorType + "' is not currently supported.");
            }
        }
    }
}