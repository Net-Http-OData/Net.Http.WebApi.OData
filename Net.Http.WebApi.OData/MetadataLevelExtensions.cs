// -----------------------------------------------------------------------
// <copyright file="MetadataLevelExtensions.cs" company="Project Contributors">
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
    using System.Net.Http.Headers;

    internal static class MetadataLevelExtensions
    {
        internal const string HeaderKey = "odata.metadata";
        private static readonly NameValueHeaderValue MetadataLevelFull = new NameValueHeaderValue(HeaderKey, "full");
        private static readonly NameValueHeaderValue MetadataLevelMinimal = new NameValueHeaderValue(HeaderKey, "minimal");
        private static readonly NameValueHeaderValue MetadataLevelNone = new NameValueHeaderValue(HeaderKey, "none");

        internal static NameValueHeaderValue ToNameValueHeaderValue(this MetadataLevel metadataLevel)
        {
            switch (metadataLevel)
            {
                case MetadataLevel.Full:
                    return MetadataLevelFull;

                case MetadataLevel.Minimal:
                    return MetadataLevelMinimal;

                case MetadataLevel.None:
                    return MetadataLevelNone;

                default:
                    throw new NotSupportedException(metadataLevel.ToString());
            }
        }
    }
}