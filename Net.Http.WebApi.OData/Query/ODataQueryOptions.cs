// -----------------------------------------------------------------------
// <copyright file="ODataQueryOptions.cs" company="Project Contributors">
// Copyright 2012 - 2017 Project Contributors
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Model;

    /// <summary>
    /// An object which contains the query options in an OData query.
    /// </summary>
    [ODataQueryOptionsParameterBinding]
    public sealed class ODataQueryOptions
    {
        private SelectExpandQueryOption expand;
        private FilterQueryOption filter;
        private FormatQueryOption format;
        private InlineCountQueryOption inlineCount;
        private OrderByQueryOption orderBy;
        private SelectExpandQueryOption select;
        private SkipQueryOption skip;
        private SkipTokenQueryOption skipToken;
        private TopQueryOption top;

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataQueryOptions" /> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="model">The model.</param>
        /// <exception cref="ArgumentNullException">Thrown if the request or model are null.</exception>
        public ODataQueryOptions(HttpRequestMessage request, EdmComplexType model)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            System.Diagnostics.Debug.Assert(model.Equals(EntityDataModel.Current.Collections[request.GetModelName()]), "The model appears to be incorrect for the URI");

            ValidateRequest(request);

            this.Request = request;
            this.Model = model;
            this.RawValues = new ODataRawQueryOptions(request.RequestUri.Query);
        }

        /// <summary>
        /// Gets the expand query option.
        /// </summary>
        public SelectExpandQueryOption Expand
        {
            get
            {
                if (this.expand == null && this.RawValues.Expand != null)
                {
                    this.expand = new SelectExpandQueryOption(this.RawValues.Expand, this.Model);
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
                if (this.filter == null && this.RawValues.Filter != null)
                {
                    this.filter = new FilterQueryOption(this.RawValues.Filter, this.Model);
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
                if (this.format == null && this.RawValues.Format != null)
                {
                    this.format = new FormatQueryOption(this.RawValues.Format);
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
                if (this.inlineCount == null && this.RawValues.InlineCount != null)
                {
                    this.inlineCount = new InlineCountQueryOption(this.RawValues.InlineCount);
                }

                return this.inlineCount;
            }
        }

        /// <summary>
        /// Gets the <see cref="EdmComplexType"/> which the OData query relates to.
        /// </summary>
        public EdmComplexType Model
        {
            get;
        }

        /// <summary>
        /// Gets the order by query option.
        /// </summary>
        public OrderByQueryOption OrderBy
        {
            get
            {
                if (this.orderBy == null && this.RawValues.OrderBy != null)
                {
                    this.orderBy = new OrderByQueryOption(this.RawValues.OrderBy, this.Model);
                }

                return this.orderBy;
            }
        }

        /// <summary>
        /// Gets the raw values of the OData query request.
        /// </summary>
        public ODataRawQueryOptions RawValues
        {
            get;
        }

        /// <summary>
        /// Gets the request message associated with this OData query.
        /// </summary>
        public HttpRequestMessage Request
        {
            get;
        }

        /// <summary>
        /// Gets the select query option.
        /// </summary>
        public SelectExpandQueryOption Select
        {
            get
            {
                if (this.select == null && this.RawValues.Select != null)
                {
                    this.select = new SelectExpandQueryOption(this.RawValues.Select, this.Model);
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
                if (this.skip == null && this.RawValues.Skip != null)
                {
                    this.skip = new SkipQueryOption(this.RawValues.Skip);
                }

                return this.skip;
            }
        }

        /// <summary>
        /// Gets the skip token query option.
        /// </summary>
        public SkipTokenQueryOption SkipToken
        {
            get
            {
                if (this.skipToken == null && this.RawValues.SkipToken != null)
                {
                    this.skipToken = new SkipTokenQueryOption(this.RawValues.SkipToken);
                }

                return this.skipToken;
            }
        }

        /// <summary>
        /// Gets the top query option.
        /// </summary>
        public TopQueryOption Top
        {
            get
            {
                if (this.top == null && this.RawValues.Top != null)
                {
                    this.top = new TopQueryOption(this.RawValues.Top);
                }

                return this.top;
            }
        }

        private static void ValidateRequest(HttpRequestMessage request)
        {
            IEnumerable<string> values;

            if (request.Headers.TryGetValues(ODataHeaderNames.DataServiceVersion, out values))
            {
                var value = values.FirstOrDefault();

                if (value != null && value != "3.0")
                {
                    throw new HttpResponseException(
                        request.CreateErrorResponse(HttpStatusCode.NotAcceptable, Messages.UnsupportedODataVersion));
                }
            }
        }
    }
}