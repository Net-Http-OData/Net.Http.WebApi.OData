// -----------------------------------------------------------------------
// <copyright file="ConstantNodeParser.cs" company="Project Contributors">
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
    using System;
    using System.Globalization;
    using Net.Http.WebApi.OData.Query.Expressions;

    internal static class ConstantNodeParser
    {
        private static readonly string[] DateTimeFormats = new[] { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm", "s", "o", "yyyy-MM-ddTHH:mm:ss.fffZ" };

        internal static ConstantNode ParseConstantNode(Token token)
        {
            switch (token.TokenType)
            {
                case TokenType.DateTime:
                    var dateTimeText = token.Value.Substring(9, token.Value.Length - 10);
                    var dateTimeValue = DateTime.ParseExact(dateTimeText, DateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    return ConstantNode.DateTime(token.Value, dateTimeValue);

                case TokenType.DateTimeOffset:
                    var dateTimeOffsetText = token.Value.Substring(15, token.Value.Length - 16);
                    var dateTimeOffsetValue = DateTimeOffset.Parse(dateTimeOffsetText, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    return ConstantNode.DateTimeOffset(token.Value, dateTimeOffsetValue);

                case TokenType.Decimal:
                    var decimalText = token.Value.Substring(0, token.Value.Length - 1);
                    var decimalValue = decimal.Parse(decimalText, CultureInfo.InvariantCulture);
                    return ConstantNode.Decimal(token.Value, decimalValue);

                case TokenType.Double:
                    var doubleText = token.Value.Substring(0, token.Value.Length - 1);
                    var doubleValue = double.Parse(doubleText, CultureInfo.InvariantCulture);
                    return ConstantNode.Double(token.Value, doubleValue);

                case TokenType.False:
                    return ConstantNode.False;

                case TokenType.Guid:
                    var guidText = token.Value.Substring(5, token.Value.Length - 6);
                    var guidValue = Guid.ParseExact(guidText, "D");
                    return ConstantNode.Guid(token.Value, guidValue);

                case TokenType.Int32:
                    var int32Value = int.Parse(token.Value, CultureInfo.InvariantCulture);
                    return ConstantNode.Int32(token.Value, int32Value);

                case TokenType.Int64:
                    var int64Text = token.Value.Substring(0, token.Value.Length - 1);
                    var int64Value = long.Parse(int64Text, CultureInfo.InvariantCulture);
                    return ConstantNode.Int64(token.Value, int64Value);

                case TokenType.Null:
                    return ConstantNode.Null;

                case TokenType.Single:
                    var singleText = token.Value.Substring(0, token.Value.Length - 1);
                    var singleValue = float.Parse(singleText, CultureInfo.InvariantCulture);
                    return ConstantNode.Single(token.Value, singleValue);

                case TokenType.String:
                    var stringText = token.Value.Trim('\'').Replace("''", "'");
                    return ConstantNode.String(token.Value, stringText);

                case TokenType.Time:
                    var timeSpanText = token.Value.Substring(5, token.Value.Length - 6);
                    var timeSpanValue = TimeSpan.Parse(timeSpanText, CultureInfo.InvariantCulture);
                    return ConstantNode.Time(token.Value, timeSpanValue);

                case TokenType.True:
                    return ConstantNode.True;

                default:
                    throw new NotSupportedException(token.TokenType.ToString());
            }
        }
    }
}