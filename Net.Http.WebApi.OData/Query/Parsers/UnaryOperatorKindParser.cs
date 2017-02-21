// -----------------------------------------------------------------------
// <copyright file="UnaryOperatorKindParser.cs" company="Project Contributors">
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

    internal static class UnaryOperatorKindParser
    {
        internal static UnaryOperatorKind ToUnaryOperatorKind(this string operatorType)
        {
            switch (operatorType)
            {
                case "not":
                    return UnaryOperatorKind.Not;

                default:
                    throw new ODataException(Messages.UnknownOperator.FormatWith(operatorType));
            }
        }
    }
}