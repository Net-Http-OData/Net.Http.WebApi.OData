// -----------------------------------------------------------------------
// <copyright file="UnaryOperatorKindParser.cs" company="Project Contributors">
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
    internal static class UnaryOperatorKindParser
    {
        internal static UnaryOperatorKind ToUnaryOperatorKind(string operatorType)
        {
            switch (operatorType)
            {
                case "not":
                    return UnaryOperatorKind.Not;

                default:
                    throw new ODataException("The operator type '" + operatorType + "' is not currently supported.");
            }
        }
    }
}