// -----------------------------------------------------------------------
// <copyright file="HttpRequestMessageExtensions.cs" company="Project Contributors">
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
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using Net.Http.OData;
using Net.Http.OData.Model;
using Net.Http.OData.Query;

namespace Net.Http.WebApi.OData
{
    /// <summary>
    /// Extensions for the <see cref="HttpRequestMessage"/> class.
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Creates the OData error response message from the specified request message with the specified status code and message text.
        /// </summary>
        /// <param name="request">The HTTP request message which led to the excetion.</param>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> applicable.</param>
        /// <param name="message">The message to return in the error detail.</param>
        /// <returns>An initialized System.Net.Http.HttpResponseMessage wired up to the associated System.Net.Http.HttpRequestMessage.</returns>
        /// <example>
        /// <code>request.CreateODataErrorResponse(HttpStatusCode.BadRequest, "Path segment not supported: 'Foo'.");</code>
        /// <para>{ "error": { "code": "400", "message": "Path segment not supported: 'Foo'." } }.</para>
        /// </example>
        public static HttpResponseMessage CreateODataErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, string message)
            => CreateODataErrorResponse(request, statusCode, message, null);

        /// <summary>
        /// Creates the OData error response message from the specified request message with the specified status code, code and message text.
        /// </summary>
        /// <param name="request">The HTTP request message which led to the excetion.</param>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> applicable.</param>
        /// <param name="message">The message to return in the error detail.</param>
        /// <param name="target">The target of the exception.</param>
        /// <returns>An initialized System.Net.Http.HttpResponseMessage wired up to the associated System.Net.Http.HttpRequestMessage.</returns>
        /// <example>
        /// <code>request.CreateODataErrorResponse(HttpStatusCode.BadRequest, "400", "Path segment not supported: 'Foo'.");</code>
        /// <para>{ "error": { "code": "400", "message": "Path segment not supported: 'Foo'." } }.</para>
        /// </example>
        public static HttpResponseMessage CreateODataErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, string message, string target)
            => request.CreateResponse(statusCode, ODataErrorContent.Create((int)statusCode, message, target));

        /// <summary>
        /// Creates the OData response message from the specified request message for the <see cref="ODataException"/>.
        /// </summary>
        /// <param name="request">The HTTP request message which led to the excetion.</param>
        /// <param name="exception">The <see cref="ODataException"/> indicating the error.</param>
        /// <returns>An initialized System.Net.Http.HttpResponseMessage wired up to the associated System.Net.Http.HttpRequestMessage.</returns>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     throw new new ODataException(HttpStatusCode.BadRequest, "Path segment not supported: 'Foo'.", "Foo");
        /// }
        /// catch (ODataException e)
        /// {
        ///     request.CreateODataErrorResponse(e);
        /// }
        /// </code>
        /// <para>{ "error": { "code": "400", "message": "Path segment not supported: 'Foo'.", "target": "Foo" } }.</para>
        /// </example>
        public static HttpResponseMessage CreateODataErrorResponse(this HttpRequestMessage request, ODataException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return CreateODataErrorResponse(request, exception.StatusCode, exception.Message, exception.Target);
        }

        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="statusCode">The HTTP response status code.</param>
        /// <returns>An initialized System.Net.Http.HttpResponseMessage wired up to the associated System.Net.Http.HttpRequestMessage.</returns>
        [Obsolete("No longer required, call HttpRequestMessageExtensions.CreateResponse(this HttpRequestMessage request, HttpStatusCode statusCode) directly")]
        public static HttpResponseMessage CreateODataResponse(this HttpRequestMessage request, HttpStatusCode statusCode)
            => request.CreateResponse(statusCode);

        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="statusCode">The HTTP response status code.</param>
        /// <param name="value">The string content of the HTTP response message.</param>
        /// <returns>An initialized System.Net.Http.HttpResponseMessage wired up to the associated System.Net.Http.HttpRequestMessage.</returns>
        public static HttpResponseMessage CreateODataResponse(this HttpRequestMessage request, HttpStatusCode statusCode, string value)
        {
            HttpResponseMessage response = request.CreateResponse(statusCode);

            if (value != null)
            {
                response.Content = new StringContent(value);
            }

            return response;
        }

        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="value">The string content of the HTTP response message.</param>
        /// <returns>An initialized System.Net.Http.HttpResponseMessage wired up to the associated System.Net.Http.HttpRequestMessage.</returns>
        public static HttpResponseMessage CreateODataResponse(this HttpRequestMessage request, string value)
            => CreateODataResponse(request, value is null ? HttpStatusCode.NoContent : HttpStatusCode.OK, value);

        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <typeparam name="T">The type of the HTTP response message.</typeparam>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="statusCode">The HTTP response status code.</param>
        /// <param name="value">The content of the HTTP response message.</param>
        /// <returns>An initialized System.Net.Http.HttpResponseMessage wired up to the associated System.Net.Http.HttpRequestMessage.</returns>
        [Obsolete("No longer required, call HttpRequestMessageExtensions.CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value) directly")]
        public static HttpResponseMessage CreateODataResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value)
            => request.CreateResponse(statusCode, value);

        /// <summary>
        /// Gets a value indicating whether the specified URI is an OData Metadata URI.
        /// </summary>
        /// <param name="request">The HTTP request message for the current request.</param>
        /// <returns>True if the URI is an OData Metadata URI, otherwise false.</returns>
        public static bool IsODataMetadataRequest(this HttpRequestMessage request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Uri uri = request.RequestUri;

            for (int i = 0; i < uri.Segments.Length; i++)
            {
                if (uri.Segments[i].StartsWith("$metadata", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a value indicating whether the specified URI is an OData URI.
        /// </summary>
        /// <param name="request">The HTTP request message for the current request.</param>
        /// <returns>True if the URI is an OData URI, otherwise false.</returns>
        public static bool IsODataRequest(this HttpRequestMessage request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Uri uri = request.RequestUri;

            for (int i = 0; i < uri.Segments.Length; i++)
            {
                if (uri.Segments[i].StartsWith("odata", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the @odata.nextLink for a paged OData query.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="resultsPerPage">The results per page.</param>
        /// <returns>The next link for a paged OData query.</returns>
        public static string NextLink(this HttpRequestMessage request, ODataQueryOptions queryOptions, int skip, int resultsPerPage)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (queryOptions is null)
            {
                throw new ArgumentNullException(nameof(queryOptions));
            }

            Uri requestUri = request.RequestUri;

            StringBuilder uriBuilder = new StringBuilder()
                .Append(requestUri.Scheme)
                .Append(Uri.SchemeDelimiter)
                .Append(requestUri.Authority)
                .Append(requestUri.LocalPath)
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
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <returns>The OData request options for the request.</returns>
        public static ODataRequestOptions ReadODataRequestOptions(this HttpRequestMessage request)
            => request?.Properties[typeof(ODataRequestOptions).FullName] as ODataRequestOptions;

        /// <summary>
        /// Resolves the <see cref="EntitySet"/> for the OData request.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <returns>The EntitySet the OData request relates to.</returns>
        public static EntitySet ResolveEntitySet(this HttpRequestMessage request)
            => EntityDataModel.Current.EntitySetForPath(request?.RequestUri.LocalPath);

        /// <summary>
        /// Resolves the @odata.context for the specified request.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <returns>A <see cref="string"/> containing the @odata.context, or null if the metadata for the request is none.</returns>
        public static string ResolveODataContext(this HttpRequestMessage request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ODataRequestOptions requestOptions = request.ReadODataRequestOptions();

            return ODataUtility.ODataContext(
                requestOptions.MetadataLevel,
                request.RequestUri.Scheme,
                request.RequestUri.Host,
                request.RequestUri.LocalPath);
        }

        /// <summary>
        /// Resolves the @odata.context for the specified request and Entity Set.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <returns>A <see cref="string"/> containing the @odata.context, or null if the metadata for the request is none.</returns>
        public static string ResolveODataContext(this HttpRequestMessage request, EntitySet entitySet)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ODataRequestOptions requestOptions = request.ReadODataRequestOptions();

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
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="selectQueryOption">The select query option.</param>
        /// <returns>A <see cref="string"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static string ResolveODataContext(this HttpRequestMessage request, EntitySet entitySet, SelectExpandQueryOption selectQueryOption)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ODataRequestOptions requestOptions = request.ReadODataRequestOptions();

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
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <typeparam name="TEntityKey">The type of entity key.</typeparam>
        /// <returns>A <see cref="string"/> containing the @odata.context, or null if the metadata for the request is none.</returns>
        public static string ResolveODataContext<TEntityKey>(this HttpRequestMessage request, EntitySet entitySet)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ODataRequestOptions requestOptions = request.ReadODataRequestOptions();

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
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="entityKey">The Entity Key for the item in the EntitySet.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <typeparam name="TEntityKey">The type of entity key.</typeparam>
        /// <returns>A <see cref="string"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static string ResolveODataContext<TEntityKey>(this HttpRequestMessage request, EntitySet entitySet, TEntityKey entityKey, string propertyName)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ODataRequestOptions requestOptions = request.ReadODataRequestOptions();

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
        /// Resolves the @odata.id for the specified request and Entity Set.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="entityKey">The Entity Key for the item in the EntitySet.</param>
        /// <typeparam name="TEntityKey">The type of entity key.</typeparam>
        /// <returns>A <see cref="string"/> containing the address of the Entity with the specified Entity Key.</returns>
        public static string ResolveODataId<TEntityKey>(this HttpRequestMessage request, EntitySet entitySet, TEntityKey entityKey)
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
    }
}
