// -----------------------------------------------------------------------
// <copyright file="TokenDefinition.cs" company="Project Contributors">
// Copyright 2012 - 2015 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData.Query
{
    using System.Text.RegularExpressions;

    [System.Diagnostics.DebuggerDisplay("{TokenType}: {Regex}")]
    internal struct TokenDefinition
    {
        private readonly bool ignore;
        private readonly Regex regex;
        private readonly TokenType tokenType;

        internal TokenDefinition(string expression, TokenType tokenType)
            : this(expression, tokenType, false)
        {
        }

        internal TokenDefinition(string expression, TokenType tokenType, bool ignore)
        {
            this.regex = new Regex(@"\G" + expression, RegexOptions.Singleline);
            this.tokenType = tokenType;
            this.ignore = ignore;
        }

        internal bool Ignore
        {
            get
            {
                return this.ignore;
            }
        }

        internal Regex Regex
        {
            get
            {
                return this.regex;
            }
        }

        internal TokenType TokenType
        {
            get
            {
                return this.tokenType;
            }
        }
    }
}