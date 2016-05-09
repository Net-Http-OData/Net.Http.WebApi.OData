// -----------------------------------------------------------------------
// <copyright file="Lexer.cs" company="Project Contributors">
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
    internal struct Lexer
    {
        // More restrictive expressions should be added before less restrictive expressions which could also match.
        // Also, within those bounds then order by the most common first where possible.
        private static readonly TokenDefinition[] TokenDefinitions = new[]
        {
            new TokenDefinition(@"\(", TokenType.OpenParentheses),
            new TokenDefinition(@"\)", TokenType.CloseParentheses),
            new TokenDefinition(@"and(?=\s)", TokenType.And),
            new TokenDefinition(@"or(?=\s)", TokenType.Or),
            new TokenDefinition(@"true", TokenType.True),
            new TokenDefinition(@"false", TokenType.False),
            new TokenDefinition(@"null", TokenType.Null),
            new TokenDefinition(@"not(?=\s)", TokenType.UnaryOperator),
            new TokenDefinition(@"(eq|ne|gt|ge|lt|le)(?=\s)", TokenType.LogicalOperator),
            new TokenDefinition(@"(-)?\d+(\.\d+)?(m|M)", TokenType.Decimal),
            new TokenDefinition(@"(-)?\d+(\.\d+)?(d|D)", TokenType.Double),
            new TokenDefinition(@"(-)?\d+(\.\d+)?(f|F)", TokenType.Single),
            new TokenDefinition(@"(-)?\d+(l|L)", TokenType.Int64),
            new TokenDefinition(@"(-)?\d+", TokenType.Int32),
            new TokenDefinition(@"(add|sub|mul|div|mod)(?=\s)", TokenType.ArithmeticOperator),
            new TokenDefinition(@"\w+(?=\()", TokenType.FunctionName),
            new TokenDefinition(@",(?=\s?)", TokenType.Comma),
            new TokenDefinition(@"datetime'\d{4}-\d{2}-\d{2}(T\d{2}:\d{2}(:\d{2}(\.\d{7}(Z|-\d{2}:\d{2}))?)?)?'", TokenType.DateTime),
            new TokenDefinition(@"datetimeoffset'\d{4}-\d{2}-\d{2}(T\d{2}:\d{2}(:\d{2}(Z|-\d{2}:\d{2}))?)?'", TokenType.DateTimeOffset),
            new TokenDefinition(@"time'\d{2}:\d{2}:\d{2}'", TokenType.Time),
            new TokenDefinition(@"guid'[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}'", TokenType.Guid),
            new TokenDefinition(@"\w+", TokenType.PropertyName),
            new TokenDefinition(@"'(?:''|[^']*)*'", TokenType.String),
            new TokenDefinition(@"\s", TokenType.Whitespace, ignore: true)
        };

        private readonly string content;
        private Token current;
        private int position;

        internal Lexer(string content)
        {
            this.content = content;
            this.position = 0;
            this.current = default(Token);
        }

        internal Token Current
        {
            get
            {
                return this.current;
            }
        }

        internal bool MoveNext()
        {
            for (int i = 0; i < TokenDefinitions.Length; i++)
            {
                var tokenDefinition = TokenDefinitions[i];

                var match = tokenDefinition.Regex.Match(this.content, this.position);

                if (match.Success)
                {
                    if (tokenDefinition.Ignore)
                    {
                        this.position += match.Length;
                        i = -1;
                        continue;
                    }

                    this.current = new Token(match.Value, tokenDefinition.TokenType);
                    this.position += match.Length;

                    return true;
                }
            }

            return false;
        }
    }
}
