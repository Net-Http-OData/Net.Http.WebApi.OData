// -----------------------------------------------------------------------
// <copyright file="ODataQueryOptions.cs" company="Project Contributors">
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
    using System.Net.Http;
    using System.Web;

    /// <summary>
    /// An object which contains the query options in an OData query.
    /// </summary>
    [ODataQueryOptionsParameterBinding]
    public sealed class ODataQueryOptions
    {
        private readonly ODataRawQueryOptions rawValues;
        private readonly HttpRequestMessage request;
        private ExpandQueryOption expand;
        private FilterQueryOption filter;
        private FormatQueryOption format;
        private InlineCountQueryOption inlineCount;
        private OrderByQueryOption orderBy;
        private SelectQueryOption select;
        private SkipQueryOption skip;
        private TopQueryOption top;

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

            var rawQuery = HttpUtility.UrlDecode(request.RequestUri.Query);

            this.request = request;
            this.rawValues = new ODataRawQueryOptions(rawQuery);
        }

        /// <summary>
        /// Gets the expand query option.
        /// </summary>
        public ExpandQueryOption Expand
        {
            get
            {
                if (this.expand == null && this.rawValues.Expand != null)
                {
                    this.expand = new ExpandQueryOption(this.rawValues.Expand);
                }

                return this.expand;
            }
        }

        /// <summary>
        /// Gets the filter query option.
        /// </summary>
        public FilterQueryOption Filter
        {
            get
            {
                if (this.filter == null && this.rawValues.Filter != null)
                {
                    this.filter = new FilterQueryOption(this.rawValues.Filter);
                }

                return this.filter;
            }
        }

        /// <summary>
        /// Gets the format query option.
        /// </summary>
        public FormatQueryOption Format
        {
            get
            {
                if (this.format == null && this.rawValues.Format != null)
                {
                    this.format = new FormatQueryOption(this.rawValues.Format);
                }

                return this.format;
            }
        }

        /// <summary>
        /// Gets the inline count query option.
        /// </summary>
        public InlineCountQueryOption InlineCount
        {
            get
            {
                if (this.inlineCount == null && this.rawValues.InlineCount != null)
                {
                    this.inlineCount = new InlineCountQueryOption(this.rawValues.InlineCount);
                }

                return this.inlineCount;
            }
        }

        /// <summary>
        /// Gets the order by query option.
        /// </summary>
        public OrderByQueryOption OrderBy
        {
            get
            {
                if (this.orderBy == null && this.rawValues.OrderBy != null)
                {
                    this.orderBy = new OrderByQueryOption(this.rawValues.OrderBy);
                }

                return this.orderBy;
            }
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
            get
            {
                if (this.select == null && this.rawValues.Select != null)
                {
                    this.select = new SelectQueryOption(this.rawValues.Select);
                }

                return this.select;
            }
        }

        /// <summary>
        /// Gets the skip query option.
        /// </summary>
        public SkipQueryOption Skip
        {
            get
            {
                if (this.skip == null && this.rawValues.Skip != null)
                {
                    this.skip = new SkipQueryOption(this.rawValues.Skip);
                }

                return this.skip;
            }
        }

        /// <summary>
        /// Gets the top query option.
        /// </summary>
        public TopQueryOption Top
        {
            get
            {
                if (this.top == null && this.rawValues.Top != null)
                {
                    this.top = new TopQueryOption(this.rawValues.Top);
                }

                return this.top;
            }
        }
    }
}