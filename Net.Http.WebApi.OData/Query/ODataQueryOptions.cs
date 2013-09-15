// -----------------------------------------------------------------------
// <copyright file="ODataQueryOptions.cs" company="MicroLite">
// Copyright 2012-2013 Trevor Pilley
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
    using System.Net.Http;

    /// <summary>
    /// An object which contains the query options in an OData query.
    /// </summary>
    [ODataQueryOptionsParameterBinding]
    public sealed class ODataQueryOptions
    {
        private readonly ODataRawQueryOptions rawValues;
        private readonly HttpRequestMessage request;

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataQueryOptions"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        public ODataQueryOptions(HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var unescapedQuery = Uri.UnescapeDataString(request.RequestUri.Query);

            var start = unescapedQuery.Length > 0 ? 1 : 0;
            var length = unescapedQuery.Length > 0 ? unescapedQuery.Length - 1 : 0;
            var rawQuery = unescapedQuery.Substring(start, length);

            this.request = request;
            this.rawValues = new ODataRawQueryOptions(rawQuery);

            if (this.rawValues.Filter != null)
            {
                this.Filter = new FilterQueryOption(this.rawValues.Filter);
            }

            if (this.rawValues.Format != null)
            {
                this.Format = new FormatQueryOption(this.rawValues.Format);
            }

            if (this.rawValues.InlineCount != null)
            {
                this.InlineCount = new InlineCountQueryOption(this.rawValues.InlineCount);
            }

            if (this.rawValues.OrderBy != null)
            {
                this.OrderBy = new OrderByQueryOption(this.rawValues.OrderBy);
            }

            if (this.rawValues.Select != null)
            {
                this.Select = new SelectQueryOption(this.rawValues.Select);
            }

            if (this.rawValues.Skip != null)
            {
                this.Skip = new SkipQueryOption(this.rawValues.Skip);
            }

            if (this.rawValues.Top != null)
            {
                this.Top = new TopQueryOption(this.rawValues.Top);
            }
        }

        /// <summary>
        /// Gets the filter query option.
        /// </summary>
        public FilterQueryOption Filter
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the format query option.
        /// </summary>
        public FormatQueryOption Format
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the inline count query option.
        /// </summary>
        public InlineCountQueryOption InlineCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the order by query option.
        /// </summary>
        public OrderByQueryOption OrderBy
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the raw values of the OData query request.
        /// </summary>
        public ODataRawQueryOptions RawValues
        {
            get
            {
                return this.rawValues;
            }
        }

        /// <summary>
        /// Gets the request message associated with this OData query.
        /// </summary>
        public HttpRequestMessage Request
        {
            get
            {
                return this.request;
            }
        }

        /// <summary>
        /// Gets the select query option.
        /// </summary>
        public SelectQueryOption Select
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the skip query option.
        /// </summary>
        public SkipQueryOption Skip
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the top query option.
        /// </summary>
        public TopQueryOption Top
        {
            get;
            private set;
        }
    }
}