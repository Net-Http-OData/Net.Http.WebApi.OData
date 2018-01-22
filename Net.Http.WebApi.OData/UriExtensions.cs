// -----------------------------------------------------------------------
// <copyright file="UriExtensions.cs" company="Project Contributors">
// Copyright 2012 - 2018 Project Contributors
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
    using System.Text;
    using Net.Http.WebApi.OData.Model;
    using Net.Http.WebApi.OData.Query;

    /// <summary>
    /// Extensions for the Uri class.
    /// </summary>
    internal static class UriExtensions
    {
        private static readonly char[] NonNameCharacters = new[] { '(', '/', '$', '%' };

        internal static StringBuilder ODataContextUriBuilder(this Uri requestUri)
        {
            var contextUriBuilder = ODataServiceUriBuilder(requestUri);
            contextUriBuilder.Append("$metadata");

            return contextUriBuilder;
        }

        internal static StringBuilder ODataContextUriBuilder(this Uri requestUri, EntitySet entitySet)
        {
            var contextUriBuilder = ODataContextUriBuilder(requestUri);
            contextUriBuilder.Append("#").Append(entitySet.Name);

            return contextUriBuilder;
        }

        internal static StringBuilder ODataContextUriBuilder(this Uri requestUri, EntitySet entitySet, SelectExpandQueryOption selectExpandQueryOption)
        {
            var contextUriBuilder = ODataContextUriBuilder(requestUri);
            contextUriBuilder.Append("#").Append(entitySet.Name);

            if (selectExpandQueryOption?.RawValue.Equals("$select=*") == true)
            {
                contextUriBuilder.Append("(*)");
            }
            else if (selectExpandQueryOption?.Properties.Count > 0)
            {
                contextUriBuilder.AppendFormat("({0})", string.Join(",", selectExpandQueryOption.Properties));
            }

            return contextUriBuilder;
        }

        internal static StringBuilder ODataContextUriBuilder<TEntityKey>(this Uri requestUri, EntitySet entitySet, TEntityKey entityKey)
        {
            var contextUriBuilder = ODataContextUriBuilder(requestUri, entitySet);
            contextUriBuilder.Append("/$entity");

            return contextUriBuilder;
        }

        internal static StringBuilder ODataContextUriBuilder<TEntityKey>(this Uri requestUri, EntitySet entitySet, TEntityKey entityKey, string propertyName)
        {
            var contextUriBuilder = ODataContextUriBuilder(requestUri, entitySet);

            if (typeof(TEntityKey) == typeof(string))
            {
                contextUriBuilder.Append("('").Append(entityKey.ToString()).Append("')");
            }
            else
            {
                contextUriBuilder.Append("(").Append(entityKey.ToString()).Append(")");
            }

            contextUriBuilder.Append('/').Append(propertyName);

            return contextUriBuilder;
        }

        internal static StringBuilder ODataEntityUriBuilder<TEntityKey>(this Uri requestUri, EntitySet entitySet, TEntityKey entityKey)
        {
            var contextUriBuilder = ODataServiceUriBuilder(requestUri);
            contextUriBuilder.Append(entitySet.Name);

            if (typeof(TEntityKey) == typeof(string))
            {
                contextUriBuilder.Append("('").Append(entityKey.ToString()).Append("')");
            }
            else
            {
                contextUriBuilder.Append("(").Append(entityKey.ToString()).Append(")");
            }

            return contextUriBuilder;
        }

        /// <summary>
        /// Resolves the name of the Entity Set referenced in the request.
        /// </summary>
        /// <param name="requestUri">The HTTP request message which led to ths OData request.</param>
        /// <returns>The name of the Entity Set referenced in the request, or null if no entity set was referenced.</returns>
        internal static string ResolveODataEntitySetName(this Uri requestUri)
        {
            var modelNameSegmentIndex = -1;

            for (int i = 0; i < requestUri.Segments.Length; i++)
            {
                if (requestUri.Segments[i].StartsWith("odata", StringComparison.OrdinalIgnoreCase))
                {
                    modelNameSegmentIndex = i + 1;
                    break;
                }
            }

            if (modelNameSegmentIndex < 0 || modelNameSegmentIndex >= requestUri.Segments.Length)
            {
                return null;
            }

            var modelNameSegment = requestUri.Segments[modelNameSegmentIndex];

            var nonNameCharacterIndex = modelNameSegment.IndexOfAny(NonNameCharacters);

            if (nonNameCharacterIndex > 0)
            {
                modelNameSegment = modelNameSegment.Substring(0, nonNameCharacterIndex);
            }

            return modelNameSegment;
        }

        internal static Uri ResolveODataServiceUri(this Uri requestUri)
            => new Uri(ODataServiceUriBuilder(requestUri).ToString());

        private static StringBuilder ODataServiceUriBuilder(Uri requestUri)
        {
            var uriBuilder = new StringBuilder()
                .Append(requestUri.Scheme)
                .Append(Uri.SchemeDelimiter)
                .Append(requestUri.Authority);

            for (int i = 0; i < requestUri.Segments.Length; i++)
            {
                var segment = requestUri.Segments[i];
                uriBuilder.Append(segment);

                if (segment.StartsWith("odata", StringComparison.OrdinalIgnoreCase))
                {
                    if (!segment.EndsWith("/", StringComparison.Ordinal))
                    {
                        uriBuilder.Append('/');
                    }

                    break;
                }
            }

            return uriBuilder;
        }
    }
}