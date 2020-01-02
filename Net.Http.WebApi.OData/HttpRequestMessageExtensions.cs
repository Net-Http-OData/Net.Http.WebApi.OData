// -----------------------------------------------------------------------
// <copyright file="HttpRequestMessageExtensions.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using Net.Http.WebApi.OData.Model;
    using Net.Http.WebApi.OData.Query;

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
        {
            var value = ODataErrorContent.Create((int)statusCode, message, target);

            var response = request.CreateResponse(statusCode, value);
            response.Headers.Add(ODataHeaderNames.ODataVersion, ODataHeaderValues.ODataVersionString);

            return response;
        }

        /// <summary>
        /// Creates the OData response message from the specified request message for the <see cref="ODataException"/>.
        /// </summary>
        /// <param name="request">The HTTP request message which led to the excetion.</param>
        /// <param name="exception">The <see cref="ODataException"/> to create a response from.</param>
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
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return request.CreateODataErrorResponse(exception.StatusCode, exception.Message, exception.Target);
        }

        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="statusCode">The HTTP response status code.</param>
        /// <returns>An initialized System.Net.Http.HttpResponseMessage wired up to the associated System.Net.Http.HttpRequestMessage.</returns>
        public static HttpResponseMessage CreateODataResponse(this HttpRequestMessage request, HttpStatusCode statusCode)
        {
            var response = request.CreateResponse(statusCode);
            response.Headers.Add(ODataHeaderNames.ODataVersion, ODataHeaderValues.ODataVersionString);

            return response;
        }

        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="statusCode">The HTTP response status code.</param>
        /// <param name="value">The string content of the HTTP response message.</param>
        /// <returns>An initialized System.Net.Http.HttpResponseMessage wired up to the associated System.Net.Http.HttpRequestMessage.</returns>
        public static HttpResponseMessage CreateODataResponse(this HttpRequestMessage request, HttpStatusCode statusCode, string value)
        {
            var response = request.CreateResponse(statusCode);

            if (value != null)
            {
                response.Content = new StringContent(value);
            }

            response.Headers.Add(ODataHeaderNames.ODataVersion, ODataHeaderValues.ODataVersionString);

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
        public static HttpResponseMessage CreateODataResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value)
        {
            var requestOptions = request.ReadODataRequestOptions();

            var response = request.CreateResponse(statusCode, value);
            response.Content.Headers.ContentType.Parameters.Add(requestOptions.MetadataLevel.ToNameValueHeaderValue());
            response.Headers.Add(ODataHeaderNames.ODataVersion, ODataHeaderValues.ODataVersionString);

            return response;
        }

        /// <summary>
        /// Reads the OData request options.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <returns>The OData request options for the request.</returns>
        public static ODataRequestOptions ReadODataRequestOptions(this HttpRequestMessage request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (!request.Properties.TryGetValue(typeof(ODataRequestOptions).FullName, out object requestOptions))
            {
                requestOptions = new ODataRequestOptions(request.RequestUri.ResolveODataServiceUri(), request.ReadIsolationLevel(), request.ReadMetadataLevel());

                request.Properties.Add(typeof(ODataRequestOptions).FullName, requestOptions);
            }

            return (ODataRequestOptions)requestOptions;
        }

        /// <summary>
        /// Resolves the <see cref="EntitySet"/> for the OData request.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <returns>The EntitySet the OData request relates to.</returns>
        public static EntitySet ResolveEntitySet(this HttpRequestMessage request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entitySetName = request.RequestUri.ResolveODataEntitySetName();

            if (!EntityDataModel.Current.EntitySets.TryGetValue(entitySetName, out EntitySet entitySet))
            {
                throw new ODataException(HttpStatusCode.BadRequest, $"This service does not contain a collection named '{entitySetName}'");
            }

            return entitySet;
        }

        /// <summary>
        /// Resolves the @odata.context URI for the specified request.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <returns>A <see cref="Uri"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static Uri ResolveODataContextUri(this HttpRequestMessage request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var requestOptions = request.ReadODataRequestOptions();

            if (requestOptions.MetadataLevel == ODataMetadataLevel.None)
            {
                return null;
            }

            return new Uri(request.RequestUri.ODataContextUriBuilder().ToString());
        }

        /// <summary>
        /// Resolves the @odata.context URI for the specified request and Entity Set.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <returns>A <see cref="Uri"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static Uri ResolveODataContextUri(this HttpRequestMessage request, EntitySet entitySet)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (entitySet is null)
            {
                throw new ArgumentNullException(nameof(entitySet));
            }

            var requestOptions = request.ReadODataRequestOptions();

            if (requestOptions.MetadataLevel == ODataMetadataLevel.None)
            {
                return null;
            }

            return new Uri(request.RequestUri.ODataContextUriBuilder(entitySet).ToString());
        }

        /// <summary>
        /// Resolves the @odata.context URI for the specified request and Entity Set and select query option.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="selectExpandQueryOption">The select query option.</param>
        /// <returns>A <see cref="Uri"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static Uri ResolveODataContextUri(this HttpRequestMessage request, EntitySet entitySet, SelectExpandQueryOption selectExpandQueryOption)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (entitySet is null)
            {
                throw new ArgumentNullException(nameof(entitySet));
            }

            var requestOptions = request.ReadODataRequestOptions();

            if (requestOptions.MetadataLevel == ODataMetadataLevel.None)
            {
                return null;
            }

            return new Uri(request.RequestUri.ODataContextUriBuilder(entitySet, selectExpandQueryOption).ToString());
        }

        /// <summary>
        /// Resolves the @odata.context URI for the specified request and Entity Set.
        /// </summary>
        /// <typeparam name="TEntityKey">The type of entity key.</typeparam>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="entityKey">The Entity Key for the item in the EntitySet.</param>
        /// <returns>A <see cref="Uri"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static Uri ResolveODataContextUri<TEntityKey>(this HttpRequestMessage request, EntitySet entitySet, TEntityKey entityKey)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var requestOptions = request.ReadODataRequestOptions();

            if (requestOptions.MetadataLevel == ODataMetadataLevel.None)
            {
                return null;
            }

            return new Uri(request.RequestUri.ODataContextUriBuilder(entitySet, entityKey).ToString());
        }

        /// <summary>
        /// Resolves the @odata.context URI for the specified request and Entity Set.
        /// </summary>
        /// <typeparam name="TEntityKey">The type of entity key.</typeparam>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="entityKey">The Entity Key for the item in the EntitySet.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>A <see cref="Uri"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static Uri ResolveODataContextUri<TEntityKey>(this HttpRequestMessage request, EntitySet entitySet, TEntityKey entityKey, string propertyName)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (entitySet is null)
            {
                throw new ArgumentNullException(nameof(entitySet));
            }

            var requestOptions = request.ReadODataRequestOptions();

            if (requestOptions.MetadataLevel == ODataMetadataLevel.None)
            {
                return null;
            }

            return new Uri(request.RequestUri.ODataContextUriBuilder(entitySet, entityKey, propertyName).ToString());
        }

        /// <summary>
        /// Resolves the URI for an entity with the specified Entity Key.
        /// </summary>
        /// <typeparam name="TEntityKey">The type of entity key.</typeparam>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="entityKey">The Entity Key for the item in the EntitySet.</param>
        /// <returns>A <see cref="Uri"/> containing the address of the Entity with the specified Entity Key.</returns>
        public static Uri ResolveODataEntityUri<TEntityKey>(this HttpRequestMessage request, EntitySet entitySet, TEntityKey entityKey)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (entitySet is null)
            {
                throw new ArgumentNullException(nameof(entitySet));
            }

            return new Uri(request.RequestUri.ODataEntityUriBuilder(entitySet, entityKey).ToString());
        }

        internal static string ReadHeaderValue(this HttpRequestMessage request, string name)
        {
            string value = null;

            if (request.Headers.TryGetValues(name, out IEnumerable<string> values))
            {
                value = values.FirstOrDefault();
            }

            return value;
        }

        internal static ODataIsolationLevel ReadIsolationLevel(this HttpRequestMessage request)
        {
            var headerValue = request.ReadHeaderValue(ODataHeaderNames.ODataIsolation);

            if (headerValue != null)
            {
                if (headerValue == "Snapshot")
                {
                    return ODataIsolationLevel.Snapshot;
                }

                throw new ODataException(HttpStatusCode.BadRequest, "If specified, the OData-IsolationLevel must be 'Snapshot'");
            }

            return ODataIsolationLevel.None;
        }

        internal static ODataMetadataLevel ReadMetadataLevel(this HttpRequestMessage request)
        {
            foreach (var header in request.Headers.Accept)
            {
                foreach (var parameter in header.Parameters)
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
                                throw new ODataException(HttpStatusCode.BadRequest, "If specified, the odata.metadata value in the Accept header must be 'none', 'minimal' or 'full'");
                        }
                    }
                }
            }

            return ODataMetadataLevel.Minimal;
        }
    }
}