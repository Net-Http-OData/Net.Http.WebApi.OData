// -----------------------------------------------------------------------
// <copyright file="ODataRawQueryOptions.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query
{
    using System;
    using System.Net;

    /// <summary>
    /// A class which contains the raw request values.
    /// </summary>
    public sealed class ODataRawQueryOptions
    {
        private readonly string rawQuery;

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataRawQueryOptions"/> class.
        /// </summary>
        /// <param name="rawQuery">The raw query.</param>
        /// <exception cref="ArgumentNullException">Thrown if raw query is null.</exception>
        internal ODataRawQueryOptions(string rawQuery)
        {
            if (rawQuery is null)
            {
                throw new ArgumentNullException(nameof(rawQuery));
            }

            // Any + signs we want in the data should have been encoded as %2B,
            // so do the replace first otherwise we replace legitemate + signs!
            this.rawQuery = rawQuery.Replace('+', ' ');

            if (this.rawQuery.Length > 0)
            {
                // Drop the ?
                var query = this.rawQuery.Substring(1, this.rawQuery.Length - 1);

                var queryOptions = query.Split(SplitCharacter.Ampersand, StringSplitOptions.RemoveEmptyEntries);

                foreach (var queryOption in queryOptions)
                {
                    // Decode the chunks to prevent splitting the query on an '&' which is actually part of a string value
                    var rawQueryOption = Uri.UnescapeDataString(queryOption);

                    if (rawQueryOption.StartsWith("$select=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$select=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, "The OData query option $select cannot be empty");
                        }

                        this.Select = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$filter=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$filter=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, "The OData query option $filter cannot be empty");
                        }

                        this.Filter = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$orderby=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$orderby=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, "The OData query option $orderby cannot be empty");
                        }

                        this.OrderBy = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$skip=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$skip=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, "The OData query option $skip cannot be empty");
                        }

                        this.Skip = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$top=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$top=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, "The OData query option $top cannot be empty");
                        }

                        this.Top = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$count=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$count=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, "The OData query option $count cannot be empty");
                        }

                        this.Count = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$format=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$format=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, "The OData query option $format cannot be empty");
                        }

                        this.Format = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$expand=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$expand=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, "The OData query option $expand cannot be empty");
                        }

                        this.Expand = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$search=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$search=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, "The OData query option $search cannot be empty");
                        }

                        this.Search = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$skiptoken=", StringComparison.Ordinal))
                    {
                        if (rawQueryOption.Equals("$skiptoken=", StringComparison.Ordinal))
                        {
                            throw new ODataException(HttpStatusCode.BadRequest, "The OData query option $skiptoken cannot be empty");
                        }

                        this.SkipToken = rawQueryOption;
                    }
                    else if (rawQueryOption.StartsWith("$", StringComparison.Ordinal))
                    {
                        var optionName = rawQueryOption.Substring(0, rawQueryOption.IndexOf('='));

                        throw new ODataException(HttpStatusCode.BadRequest, $"Unknown OData query option {optionName}");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the raw $count query value from the incoming request Uri if specified.
        /// </summary>
        public string Count { get; }

        /// <summary>
        /// Gets the raw $expand query value from the incoming request Uri if specified.
        /// </summary>
        public string Expand { get; }

        /// <summary>
        /// Gets the raw $filter query value from the incoming request Uri if specified.
        /// </summary>
        public string Filter { get; }

        /// <summary>
        /// Gets the raw $format query value from the incoming request Uri if specified.
        /// </summary>
        public string Format { get; }

        /// <summary>
        /// Gets the raw $orderby query value from the incoming request Uri if specified.
        /// </summary>
        public string OrderBy { get; }

        /// <summary>
        /// Gets the raw $search query value from the incoming request Uri if specified.
        /// </summary>
        public string Search { get; }

        /// <summary>
        /// Gets the raw $select query value from the incoming request Uri if specified.
        /// </summary>
        public string Select { get; }

        /// <summary>
        /// Gets the raw $skip query value from the incoming request Uri if specified.
        /// </summary>
        public string Skip { get; }

        /// <summary>
        /// Gets the raw $skip token query value from the incoming request Uri if specified.
        /// </summary>
        public string SkipToken { get; }

        /// <summary>
        /// Gets the raw $top query value from the incoming request Uri if specified.
        /// </summary>
        public string Top { get; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => this.rawQuery;
    }
}