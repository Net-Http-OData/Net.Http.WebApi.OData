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

    internal static class UriExtensions
    {
        internal static string GetModelName(this Uri requestUri)
        {
            return requestUri.Segments[requestUri.Segments.Length - 1].TrimEnd('/');
        }
    }
}