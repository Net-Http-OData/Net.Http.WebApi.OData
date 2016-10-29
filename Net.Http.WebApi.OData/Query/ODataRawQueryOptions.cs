// -----------------------------------------------------------------------
// <copyright file="ODataRawQueryOptions.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query
{
    using System;
    using System.Globalization;

    /// <summary>
    /// A class which contains the raw request values.
    /// </summary>
    public sealed class ODataRawQueryOptions
    {
        private static readonly char[] QuerySeparators = new[] { '&' };
        private readonly string rawQuery;

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataRawQueryOptions"/> class.
        /// </summary>
        /// <param name="rawQuery">The raw query.</param>
        public ODataRawQueryOptions(string rawQuery)
        {
            if (rawQuery == null)
            {
                throw new ArgumentNullException("rawQuery");
            }

            // Any + signs we want in the data should have been encoded as %2B,
            // so do the replace first otherwise we replace legitemate + signs!
            rawQuery = rawQuery.Replace('+', ' ');

            this.rawQuery = rawQuery;

            var start = rawQuery.Length > 0 ? 1 : 0;
            var length = rawQuery.Length > 0 ? rawQuery.Length - 1 : 0;
            var query = rawQuery.Substring(start, length);

            var queryOptions = query.Split(QuerySeparators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var queryOption in queryOptions)
            {
                // Decode the chunks to prevent splitting the query on an '&' which is actually part of a string value
                var rawQueryOption = Uri.UnescapeDataString(queryOption);

                if (rawQueryOption.StartsWith("$expand=", StringComparison.Ordinal))
                {
                    this.Expand = rawQueryOption;
                }
                else if (rawQueryOption.StartsWith("$filter=", StringComparison.Ordinal))
                {
                    this.Filter = rawQueryOption;
                }
                else if (rawQueryOption.StartsWith("$format=", StringComparison.Ordinal))
                {
                    this.Format = rawQueryOption;
                }
                else if (rawQueryOption.StartsWith("$inlinecount=", StringComparison.Ordinal))
                {
                    this.InlineCount = rawQueryOption;
                }
                else if (rawQueryOption.StartsWith("$orderby=", StringComparison.Ordinal))
                {
                    this.OrderBy = rawQueryOption;
                }
                else if (rawQueryOption.StartsWith("$select=", StringComparison.Ordinal))
                {
                    this.Select = rawQueryOption;
                }
                else if (rawQueryOption.StartsWith("$skip=", StringComparison.Ordinal))
                {
                    this.Skip = rawQueryOption;
                }
                else if (rawQueryOption.StartsWith("$skiptoken=", StringComparison.Ordinal))
                {
                    this.SkipToken = rawQueryOption;
                }
                else if (rawQueryOption.StartsWith("$top=", StringComparison.Ordinal))
                {
                    this.Top = rawQueryOption;
                }
                else if (rawQueryOption.StartsWith("$", StringComparison.Ordinal))
                {
                    var message = string.Format(CultureInfo.InvariantCulture, Messages.UnknownQueryOption, rawQueryOption);

                    throw new ODataException(message);
                }
            }
        }

        /// <summary>
        /// Gets the raw $expand query value from the incoming request Uri if specified.
        /// </summary>
        public string Expand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the raw $filter query value from the incoming request Uri if specified.
        /// </summary>
        public string Filter
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the raw $format query value from the incoming request Uri if specified.
        /// </summary>
        public string Format
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the raw $inline count query value from the incoming request Uri if specified.
        /// </summary>
        public string InlineCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the raw $order by query value from the incoming request Uri if specified.
        /// </summary>
        public string OrderBy
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the raw $select query value from the incoming request Uri if specified.
        /// </summary>
        public string Select
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the raw $skip query value from the incoming request Uri if specified.
        /// </summary>
        public string Skip
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the raw $skip token query value from the incoming request Uri if specified.
        /// </summary>
        public string SkipToken
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the raw $top query value from the incoming request Uri if specified.
        /// </summary>
        public string Top
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.rawQuery;
        }
    }
}