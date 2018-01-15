// -----------------------------------------------------------------------
// <copyright file="BinaryOperatorKindParser.cs" company="Project Contributors">
// Copyright 2012 - 2018 Project Contributors
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
    using Expressions;

    internal static class BinaryOperatorKindParser
    {
        private static Dictionary<string, BinaryOperatorKind> operatorTypeMap = new Dictionary<string, BinaryOperatorKind>
        {
            ["add"] = BinaryOperatorKind.Add,
            ["and"] = BinaryOperatorKind.And,
            ["div"] = BinaryOperatorKind.Divide,
            ["eq"] = BinaryOperatorKind.Equal,
            ["ge"] = BinaryOperatorKind.GreaterThanOrEqual,
            ["gt"] = BinaryOperatorKind.GreaterThan,
            ["has"] = BinaryOperatorKind.Has,
            ["le"] = BinaryOperatorKind.LessThanOrEqual,
            ["lt"] = BinaryOperatorKind.LessThan,
            ["mul"] = BinaryOperatorKind.Multiply,
            ["mod"] = BinaryOperatorKind.Modulo,
            ["ne"] = BinaryOperatorKind.NotEqual,
            ["or"] = BinaryOperatorKind.Or,
            ["sub"] = BinaryOperatorKind.Subtract,
        };

        internal static BinaryOperatorKind ToBinaryOperatorKind(this string operatorType)
        {
            BinaryOperatorKind binaryOperatorKind;

            if (operatorTypeMap.TryGetValue(operatorType, out binaryOperatorKind))
            {
                return binaryOperatorKind;
            }

            throw new ArgumentException(Messages.UnknownOperator.FormatWith(operatorType), nameof(operatorType));
        }
    }
}