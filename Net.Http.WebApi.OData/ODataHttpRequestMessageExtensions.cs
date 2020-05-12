// -----------------------------------------------------------------------
// <copyright file="ODataHttpRequestMessageExtensions.cs" company="Project Contributors">
// Copyright Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Net.Http.OData;
using Net.Http.OData.Model;
using Net.Http.OData.Query;

namespace Net.Http.WebApi.OData
{
    /// <summary>
    /// OData extensions for the <see cref="HttpRequestMessage"/> class.
    /// </summary>
    public static class ODataHttpRequestMessageExtensions
    {
        /// <summary>
        /// Gets a value indicating whether the request is an OData Metadata request.
        /// </summary>
        /// <param name="request">The HTTP request for the current request.</param>
        /// <returns>True if the request is an OData Metadata request, otherwise false.</returns>
        public static bool IsODataMetadataRequest(this HttpRequestMessage request)
            => request?.RequestUri.LocalPath.IndexOf("$metadata", StringComparison.OrdinalIgnoreCase) > 0;

        /// <summary>
        /// Gets a value indicating whether the request is an OData request.
        /// </summary>
        /// <param name="request">The HTTP request for the current request.</param>
        /// <returns>True if the request is an OData request, otherwise false.</returns>
        public static bool IsODataRequest(this HttpRequestMessage request)
            => request?.RequestUri.LocalPath.IndexOf("odata", StringComparison.OrdinalIgnoreCase) > 0;

        /// <summary>
        /// Resolves the @odata.context for the specified request.
        /// </summary>
        /// <param name="request">The HTTP request which led to this OData request.</param>
        /// <returns>A <see cref="string"/> containing the @odata.context, or null if the metadata for the request is none.</returns>
        public static string ODataContext(this HttpRequestMessage request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ODataRequestOptions requestOptions = request.ODataRequestOptions();

            return ODataUtility.ODataContext(
                requestOptions.MetadataLevel,
                request.RequestUri.Scheme,
                request.RequestUri.Host,
                request.RequestUri.LocalPath);
        }

        /// <summary>
        /// Resolves the @odata.context for the specified request and Entity Set.
        /// </summary>
        /// <param name="request">The HTTP request which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <returns>A <see cref="string"/> containing the @odata.context, or null if the metadata for the request is none.</returns>
        public static string ODataContext(this HttpRequestMessage request, EntitySet entitySet)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ODataRequestOptions requestOptions = request.ODataRequestOptions();

            return ODataUtility.ODataContext(
                requestOptions.MetadataLevel,
                request.RequestUri.Scheme,
                request.RequestUri.Host,
                request.RequestUri.LocalPath,
                entitySet);
        }

        /// <summary>
        /// Resolves the @odata.context for the specified request and Entity Set and select query option.
        /// </summary>
        /// <param name="request">The HTTP request which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="selectQueryOption">The select query option.</param>
        /// <returns>A <see cref="string"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static string ODataContext(this HttpRequestMessage request, EntitySet entitySet, SelectExpandQueryOption selectQueryOption)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ODataRequestOptions requestOptions = request.ODataRequestOptions();

            return ODataUtility.ODataContext(
                requestOptions.MetadataLevel,
                request.RequestUri.Scheme,
                request.RequestUri.Host,
                request.RequestUri.LocalPath,
                entitySet,
                selectQueryOption);
        }

        /// <summary>
        /// Resolves the @odata.context for the specified request and Entity Set.
        /// </summary>
        /// <param name="request">The HTTP request which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <typeparam name="TEntityKey">The type of entity key.</typeparam>
        /// <returns>A <see cref="string"/> containing the @odata.context, or null if the metadata for the request is none.</returns>
        public static string ODataContext<TEntityKey>(this HttpRequestMessage request, EntitySet entitySet)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ODataRequestOptions requestOptions = request.ODataRequestOptions();

            return ODataUtility.ODataContext<TEntityKey>(
                requestOptions.MetadataLevel,
                request.RequestUri.Scheme,
                request.RequestUri.Host,
                request.RequestUri.LocalPath,
                entitySet);
        }

        /// <summary>
        /// Resolves the @odata.context for the specified request and Entity Set.
        /// </summary>
        /// <param name="request">The HTTP request which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="entityKey">The Entity Key for the item in the EntitySet.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <typeparam name="TEntityKey">The type of entity key.</typeparam>
        /// <returns>A <see cref="string"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static string ODataContext<TEntityKey>(this HttpRequestMessage request, EntitySet entitySet, TEntityKey entityKey, string propertyName)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ODataRequestOptions requestOptions = request.ODataRequestOptions();

            return ODataUtility.ODataContext(
                requestOptions.MetadataLevel,
                request.RequestUri.Scheme,
                request.RequestUri.Host,
                request.RequestUri.LocalPath,
                entitySet,
                entityKey,
                propertyName);
        }

        /// <summary>
        /// Resolves the <see cref="EntitySet"/> for the OData request.
        /// </summary>
        /// <param name="request">The HTTP request which led to this OData request.</param>
        /// <returns>The EntitySet the OData request relates to.</returns>
        public static EntitySet ODataEntitySet(this HttpRequestMessage request)
            => EntityDataModel.Current.EntitySetForPath(request?.RequestUri.LocalPath);

        /// <summary>
        /// Resolves the @odata.id for the specified request and Entity Set.
        /// </summary>
        /// <param name="request">The HTTP request which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="entityKey">The Entity Key for the item in the EntitySet.</param>
        /// <typeparam name="TEntityKey">The type of entity key.</typeparam>
        /// <returns>A <see cref="string"/> containing the address of the Entity with the specified Entity Key.</returns>
        public static string ODataId<TEntityKey>(this HttpRequestMessage request, EntitySet entitySet, TEntityKey entityKey)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return ODataUtility.ODataId(
                request.RequestUri.Scheme,
                request.RequestUri.Host,
                request.RequestUri.LocalPath,
                entitySet,
                entityKey);
        }

        /// <summary>
        /// Resolves the @odata.nextLink for the specified request and <see cref="ODataQueryOptions"/>.
        /// </summary>
        /// <param name="request">The HTTP request which led to this OData request.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="resultsPerPage">The results per page.</param>
        /// <returns>The next link for a paged OData query.</returns>
        public static string ODataNextLink(this HttpRequestMessage request, ODataQueryOptions queryOptions, int skip, int resultsPerPage)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (queryOptions is null)
            {
                throw new ArgumentNullException(nameof(queryOptions));
            }

            StringBuilder uriBuilder = new StringBuilder()
                .Append(request.RequestUri.Scheme)
                .Append(Uri.SchemeDelimiter)
                .Append(request.RequestUri.Authority)
                .Append(request.RequestUri.LocalPath)
                .Append("?$skip=").Append((skip + resultsPerPage).ToString(CultureInfo.InvariantCulture));

            if (queryOptions.RawValues.Count != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Count);
            }

            if (queryOptions.RawValues.Expand != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Expand);
            }

            if (queryOptions.RawValues.Filter != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Filter);
            }

            if (queryOptions.RawValues.Format != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Format);
            }

            if (queryOptions.RawValues.OrderBy != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.OrderBy);
            }

            if (queryOptions.RawValues.Search != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Search);
            }

            if (queryOptions.RawValues.Select != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Select);
            }

            if (queryOptions.RawValues.Top != null)
            {
                uriBuilder.Append('&').Append(queryOptions.RawValues.Top);
            }

            return uriBuilder.ToString();
        }

        /// <summary>
        /// Reads the OData request options.
        /// </summary>
        /// <param name="request">The HTTP request which led to this OData request.</param>
        /// <returns>The OData request options for the request.</returns>
        public static ODataRequestOptions ODataRequestOptions(this HttpRequestMessage request)
            => request?.Properties[typeof(ODataRequestOptions).FullName] as ODataRequestOptions;

        /// <summary>
        /// Creates the OData error response from the specified exception.
        /// </summary>
        /// <param name="request">The HTTP request which led to the error.</param>
        /// <param name="exception">The <see cref="ODataException"/> indicating the error.</param>
        /// <returns>An <see cref="HttpResponseMessage"/> representing the OData error.</returns>
        internal static HttpResponseMessage CreateODataErrorResponse(this HttpRequestMessage request, ODataException exception)
        {
            HttpResponseMessage response = request.CreateResponse(exception.StatusCode, exception.ToODataErrorContent());
            response.Content.Headers.ContentLanguage.Add("en-GB");

            return response;
        }

        internal static ODataIsolationLevel ReadIsolationLevel(this HttpRequestMessage request)
        {
            string headerValue = ReadHeaderValue(request, ODataRequestHeaderNames.ODataIsolation);

            if (headerValue != null)
            {
                if (headerValue == "Snapshot")
                {
                    return ODataIsolationLevel.Snapshot;
                }

                throw ODataException.BadRequest($"If specified, the {ODataRequestHeaderNames.ODataIsolation} must be 'Snapshot'.", ODataRequestHeaderNames.ODataIsolation);
            }

            return ODataIsolationLevel.None;
        }

        internal static ODataMetadataLevel ReadMetadataLevel(this HttpRequestMessage request)
        {
            if (!string.IsNullOrWhiteSpace(request.RequestUri.Query))
            {
                string formatQueryParameters = HttpUtility.ParseQueryString(request.RequestUri.Query).Get("$format");

                if (!string.IsNullOrWhiteSpace(formatQueryParameters))
                {
                    foreach (string formatQuery in formatQueryParameters.Split(';'))
                    {
                        if (formatQuery.StartsWith("odata.metadata=", StringComparison.Ordinal))
                        {
                            switch (formatQuery)
                            {
                                case "odata.metadata=none":
                                    return ODataMetadataLevel.None;

                                case "odata.metadata=minimal":
                                    return ODataMetadataLevel.Minimal;

                                case "odata.metadata=full":
                                    return ODataMetadataLevel.Full;

                                default:
                                    throw ODataException.BadRequest(
                                        $"If specified, the {ODataMetadataLevelExtensions.HeaderName} value in the $format query option must be 'none', 'minimal' or 'full'.",
                                        ODataMetadataLevelExtensions.HeaderName);
                            }
                        }
                    }
                }
            }

            foreach (MediaTypeWithQualityHeaderValue header in request.Headers.Accept)
            {
                foreach (NameValueHeaderValue parameter in header.Parameters)
                {
                    if (parameter.Name == ODataMetadataLevelExtensions.HeaderName)
                    {
                        switch (parameter.Value)
                        {
                            case "none":
                                return ODataMetadataLevel.None;

                            case "minimal":
                                return ODataMetadataLevel.Minimal;

                            case "full":
                                return ODataMetadataLevel.Full;

                            default:
                                throw ODataException.BadRequest(
                                    $"If specified, the {ODataMetadataLevelExtensions.HeaderName} value in the Accept header must be 'none', 'minimal' or 'full'.",
                                    ODataMetadataLevelExtensions.HeaderName);
                        }
                    }
                }
            }

            return ODataMetadataLevel.Minimal;
        }

        internal static ODataVersion ReadODataMaxVersion(this HttpRequestMessage request)
        {
            string headerValue = ReadHeaderValue(request, ODataRequestHeaderNames.ODataMaxVersion);

            if (headerValue != null)
            {
                if (ODataVersion.TryParse(headerValue, out ODataVersion odataVersion))
                {
                    return odataVersion;
                }

                throw ODataException.BadRequest($"If specified, the {ODataRequestHeaderNames.ODataMaxVersion} header must be a valid OData version.", ODataRequestHeaderNames.ODataMaxVersion);
            }

            return ODataVersion.MaxVersion;
        }

        internal static ODataVersion ReadODataVersion(this HttpRequestMessage request)
        {
            string headerValue = ReadHeaderValue(request, ODataRequestHeaderNames.ODataVersion);

            if (headerValue != null)
            {
                if (ODataVersion.TryParse(headerValue, out ODataVersion odataVersion))
                {
                    return odataVersion;
                }

                throw ODataException.BadRequest($"If specified, the {ODataRequestHeaderNames.ODataMaxVersion} header must be a valid OData version.", ODataRequestHeaderNames.ODataMaxVersion);
            }

            return ReadODataMaxVersion(request);
        }

        private static string ReadHeaderValue(HttpRequestMessage request, string name)
            => request.Headers.TryGetValues(name, out IEnumerable<string> values) ? values.FirstOrDefault() : default;
    }
}
