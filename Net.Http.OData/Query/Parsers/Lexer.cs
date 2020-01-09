// -----------------------------------------------------------------------
// <copyright file="Lexer.cs" company="Project Contributors">
// Copyright 2012 - 2020 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.OData.Query.Parsers
{
    using System;
    using System.Net;

    internal struct Lexer
    {
        // More restrictive expressions should be added before less restrictive expressions which could also match.
        // Also, within those bounds then order by the most common first where possible.
        private static readonly TokenDefinition[] TokenDefinitions = new[]
        {
            new TokenDefinition(TokenType.OpenParentheses,      @"\("),
            new TokenDefinition(TokenType.CloseParentheses,     @"\)"),
            new TokenDefinition(TokenType.And,                  @"and(?=\s|$)"),
            new TokenDefinition(TokenType.Or,                   @"or(?=\s|$)"),
            new TokenDefinition(TokenType.True,                 @"true"),
            new TokenDefinition(TokenType.False,                @"false"),
            new TokenDefinition(TokenType.Null,                 @"null"),
            new TokenDefinition(TokenType.UnaryOperator,        @"not(?=\s|$)"),
            new TokenDefinition(TokenType.BinaryOperator,       @"(eq|ne|gt|ge|lt|le|has|add|sub|mul|div|mod)(?=\s|$)"),
            new TokenDefinition(TokenType.DateTimeOffset,       @"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}(:\d{2}(\.\d{1,12})?(Z|[+-]\d{2}:\d{2})?)?"),
            new TokenDefinition(TokenType.Date,                 @"\d{4}-\d{2}-\d{2}"),
            new TokenDefinition(TokenType.TimeOfDay,            @"\d{2}:\d{2}(:\d{2}(\.\d{1,12})?)?"),
            new TokenDefinition(TokenType.Guid,                 @"[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}"),
            new TokenDefinition(TokenType.Decimal,              @"(\+|-)?(\d+(\.\d+)?|(\.\d+))(m|M)"),
            new TokenDefinition(TokenType.Double,               @"(\+|-)?(\d+(\.\d+)?(d|D)|\d+\.\d+(e|E)\d+)"),
            new TokenDefinition(TokenType.Single,               @"(\+|-)?\d+(\.\d+)?(f|F)"),
            new TokenDefinition(TokenType.Integer,              @"(\+|-)?\d+(l|L)?"),
            new TokenDefinition(TokenType.FunctionName,         @"\w+(?=\()"),
            new TokenDefinition(TokenType.Comma,                @",(?=\s?)"),
            new TokenDefinition(TokenType.Duration,             @"duration'(-)?P\d+DT\d{2}H\d{2}M\d{2}\.\d+S'"),
            new TokenDefinition(TokenType.Enum,                 @"\w+(\.\w+)+'\w+(\,\w+)*'"),
            new TokenDefinition(TokenType.PropertyName,         @"[\w\/]+"),
            new TokenDefinition(TokenType.String,               @"'(?:''|[\w\s-.~!$&()*+,;=@\\\/]*)*'"),
            new TokenDefinition(TokenType.Whitespace,           @"\s", ignore: true),
        };

        private readonly string content;
        private Token current;
        private int position;

        internal Lexer(string content)
        {
            this.content = content;
            this.current = default;

            this.position = this.content.StartsWith("$filter=", StringComparison.Ordinal) ? content.IndexOf('=') + 1 : 0;
        }

        internal Token Current => this.current;

        internal bool MoveNext()
        {
            if (this.content.Length == this.position)
            {
                return false;
            }

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

                    this.current = tokenDefinition.CreateToken(match);
                    this.position += match.Length;

                    return true;
                }
            }

            if (this.content.Length != this.position)
            {
                throw new ODataException(HttpStatusCode.BadRequest, "Unable to parse the specified $filter system query option");
            }

            return false;
        }
    }
}