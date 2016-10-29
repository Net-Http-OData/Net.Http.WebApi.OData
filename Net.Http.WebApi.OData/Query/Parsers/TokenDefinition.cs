// -----------------------------------------------------------------------
// <copyright file="TokenDefinition.cs" company="Project Contributors">
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
    using System.Text.RegularExpressions;

    [System.Diagnostics.DebuggerDisplay("{TokenType}: {Regex}")]
    internal struct TokenDefinition
    {
        internal TokenDefinition(string expression, TokenType tokenType)
            : this(expression, tokenType, false)
        {
        }

        internal TokenDefinition(string expression, TokenType tokenType, bool ignore)
        {
            this.Regex = new Regex(@"\G" + expression, RegexOptions.Singleline);
            this.TokenType = tokenType;
            this.Ignore = ignore;
        }

        internal bool Ignore
        {
            get;
        }

        internal Regex Regex
        {
            get;
        }

        internal TokenType TokenType
        {
            get;
        }
    }
}