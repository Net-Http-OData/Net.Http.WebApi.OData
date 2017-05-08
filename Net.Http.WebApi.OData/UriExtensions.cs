// -----------------------------------------------------------------------
// <copyright file="UriExtensions.cs" company="Project Contributors">
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
    using System.Text;

    internal static class UriExtensions
    {
        internal static string GetModelName(this Uri requestUri)
        {
            return requestUri.Segments[requestUri.Segments.Length - 1].TrimEnd('/');
        }

        internal static Uri GetODataServiceUri(this Uri requestUri)
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