// -----------------------------------------------------------------------
// <copyright file="Token.cs" company="Project Contributors">
// Copyright 2012 - 2016 Project Contributors
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
    [System.Diagnostics.DebuggerDisplay("{TokenType}: {Value}")]
    internal struct Token
    {
        private readonly TokenType tokenType;
        private readonly string value;

        internal Token(string value, TokenType tokenType)
        {
            this.value = value;
            this.tokenType = tokenType;
        }

        internal TokenType TokenType
        {
            get
            {
                return this.tokenType;
            }
        }

        internal string Value
        {
            get
            {
                return this.value;
            }
        }
    }
}