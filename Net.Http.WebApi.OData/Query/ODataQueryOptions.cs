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
        private OrderByQueryOption orderBy;
        private SelectExpandQueryOption select;
        private SkipTokenQueryOption skipToken;

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataQueryOptions" /> class.
        /// </summary>
        /// <param name="request">The current http request message.</param>
        /// <param name="entitySet">The Entity Set to apply the OData query against.</param>
        /// <exception cref="ArgumentNullException">Thrown if the request or model are null.</exception>
        public ODataQueryOptions(HttpRequestMessage request, EntitySet entitySet)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (entitySet == null)
            {
                throw new ArgumentNullException(nameof(entitySet));
            }

            System.Diagnostics.Debug.Assert(entitySet.Equals(EntityDataModel.Current.EntitySets[request.RequestUri.ResolveEntitySetName()]), "The model appears to be incorrect for the URI");

            this.Request = request;
            this.EntitySet = entitySet;
            this.ReadHeaders();
            this.RawValues = new ODataRawQueryOptions(request.RequestUri.Query);
        }

        /// <summary>
        /// Gets a value indicating whether the count query option has been specified.
        /// </summary>
        public bool Count => this.RawValues.Count?.Equals("$count=true") == true;

        /// <summary>
        /// Gets the <see cref="EntitySet"/> to apply the OData query against.
        /// </summary>
        public EntitySet EntitySet
        {
            get;
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
                    this.expand = new SelectExpandQueryOption(this.RawValues.Expand, this.EntitySet.EdmType);
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
                    this.filter = new FilterQueryOption(this.RawValues.Filter, this.EntitySet.EdmType);
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
        /// Gets the order by query option.
        /// </summary>
        public OrderByQueryOption OrderBy
        {
            get
            {
                if (this.orderBy == null && this.RawValues.OrderBy != null)
                {
                    this.orderBy = new OrderByQueryOption(this.RawValues.OrderBy, this.EntitySet.EdmType);
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
        /// Gets the search query option.
        /// </summary>
        public string Search => this.RawValues.Search?.Substring(this.RawValues.Search.IndexOf('=') + 1);

        /// <summary>
        /// Gets the select query option.
        /// </summary>
        public SelectExpandQueryOption Select
        {
            get
            {
                if (this.select == null && this.RawValues.Select != null)
                {
                    this.select = new SelectExpandQueryOption(this.RawValues.Select, this.EntitySet.EdmType);
                }

                return this.select;
            }
        }

        /// <summary>
        /// Gets the skip query option.
        /// </summary>
        public int? Skip => ParseInt(this.RawValues.Skip);

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
        public int? Top => ParseInt(this.RawValues.Top);

        private static int? ParseInt(string rawValue)
        {
            if (rawValue == null)
            {
                return null;
            }

            var equals = rawValue.IndexOf('=') + 1;
            var value = rawValue.Substring(equals, rawValue.Length - equals);

            int integer;

            if (int.TryParse(value, out integer))
            {
                return integer;
            }

            throw new ArgumentOutOfRangeException(nameof(rawValue), Messages.IntRawValueInvalid.FormatWith(value.Substring(0, equals)));
        }

        private void ReadHeaders()
        {
            var headerValue = this.Request.ReadHeaderValue(ODataHeaderNames.ODataVersion);

            if (headerValue != null && headerValue != "4.0")
            {
                throw new HttpResponseException(
                    this.Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, Messages.UnsupportedODataVersion));
            }

            headerValue = this.Request.ReadHeaderValue(ODataHeaderNames.ODataMaxVersion);

            if (headerValue != null && headerValue != "4.0")
            {
                throw new HttpResponseException(
                    this.Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, Messages.UnsupportedODataVersion));
            }
        }
    }
}