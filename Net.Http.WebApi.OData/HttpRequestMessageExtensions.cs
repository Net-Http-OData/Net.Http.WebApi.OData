﻿// -----------------------------------------------------------------------
// <copyright file="HttpRequestMessageExtensions.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Net.Http.WebApi.OData.Model;

    /// <summary>
    /// Extensions for the <see cref="HttpRequestMessage"/> class
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="statusCode">The HTTP response status code.</param>
        /// <returns>An initialized System.Net.Http.HttpResponseMessage wired up to the associated System.Net.Http.HttpRequestMessage.</returns>
        public static HttpResponseMessage CreateODataResponse(this HttpRequestMessage request, HttpStatusCode statusCode)
        {
            var response = new HttpResponseMessage(statusCode);
            response.Headers.Add(ODataHeaderNames.ODataVersion, "4.0");

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
            var response = new HttpResponseMessage(statusCode);

            if (value != null)
            {
                response.Content = new StringContent(value);
            }

            response.Headers.Add(ODataHeaderNames.ODataVersion, "4.0");

            return response;
        }

        /// <summary>
        /// Creates the OData response message from the specified request message.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this response message.</param>
        /// <param name="value">The string content of the HTTP response message.</param>
        /// <returns>An initialized System.Net.Http.HttpResponseMessage wired up to the associated System.Net.Http.HttpRequestMessage.</returns>
        public static HttpResponseMessage CreateODataResponse(this HttpRequestMessage request, string value)
            => CreateODataResponse(request, value == null ? HttpStatusCode.NoContent : HttpStatusCode.OK, value);

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
            response.Headers.Add(ODataHeaderNames.ODataVersion, "4.0");

            return response;
        }

        /// <summary>
        /// Reads the OData request options.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <returns>The OData request options for the request.</returns>
        public static ODataRequestOptions ReadODataRequestOptions(this HttpRequestMessage request)
        {
            object requestOptions;

            if (!request.Properties.TryGetValue(typeof(ODataRequestOptions).FullName, out requestOptions))
            {
                requestOptions = new ODataRequestOptions(request);

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
            var entitySetName = request.RequestUri.ResolveEntitySetName();
            EntitySet entitySet;

            if (!EntityDataModel.Current.EntitySets.TryGetValue(entitySetName, out entitySet))
            {
                throw new HttpResponseException(
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, Messages.CollectionNameInvalid.FormatWith(entitySetName)));
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
            var requestOptions = request.ReadODataRequestOptions();

            if (requestOptions.MetadataLevel == ODataMetadataLevel.None)
            {
                return null;
            }

            return new Uri(request.RequestUri.ODataContextUriBuilder(entitySet).ToString());
        }

        /// <summary>
        /// Resolves the @odata.context URI for the specified request and Entity Set.
        /// </summary>
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="entityKey">The Entity Key for the item in the EntitySet.</param>
        /// <returns>A <see cref="Uri"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static Uri ResolveODataContextUri<TEntityKey>(this HttpRequestMessage request, EntitySet entitySet, TEntityKey entityKey)
        {
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
        /// <param name="request">The HTTP request message which led to this OData request.</param>
        /// <param name="entitySet">The EntitySet used in the request.</param>
        /// <param name="entityKey">The Entity Key for the item in the EntitySet.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>A <see cref="Uri"/> containing the @odata.context URI, or null if the metadata for the request is none.</returns>
        public static Uri ResolveODataContextUri<TEntityKey>(this HttpRequestMessage request, EntitySet entitySet, TEntityKey entityKey, string propertyName)
        {
            var requestOptions = request.ReadODataRequestOptions();

            if (requestOptions.MetadataLevel == ODataMetadataLevel.None)
            {
                return null;
            }

            return new Uri(request.RequestUri.ODataContextUriBuilder(entitySet, entityKey, propertyName).ToString());
        }

        internal static string ReadHeaderValue(this HttpRequestMessage request, string name)
        {
            IEnumerable<string> values;
            string value = null;

            if (request.Headers.TryGetValues(name, out values))
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

                throw new HttpResponseException(
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, Messages.UnsupportedIsolationLevel));
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
                                throw new HttpResponseException(
                                    request.CreateErrorResponse(HttpStatusCode.BadRequest, Messages.ODataMetadataValueInvalid));
                        }
                    }
                }
            }

            return ODataMetadataLevel.Minimal;
        }
    }
}