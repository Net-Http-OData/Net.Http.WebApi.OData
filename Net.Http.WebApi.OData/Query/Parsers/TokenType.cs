// -----------------------------------------------------------------------
// <copyright file="TokenType.cs" company="Project Contributors">
// Copyright 2012 - 2019 Project Contributors
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
    internal enum TokenType
    {
        OpenParentheses,
        CloseParentheses,
        And,
        Or,
        True,
        False,
        Null,
        BinaryOperator,
        Comma,
        Whitespace,
        Decimal,
        Double,
        Integer,
        Single,
        Date,
        DateTimeOffset,
        Guid,
        String,
        FunctionName,
        PropertyName,
        UnaryOperator,
        TimeOfDay,
        Duration,
        Enum,
    }
}