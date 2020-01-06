// -----------------------------------------------------------------------
// <copyright file="ODataMetadataLevelExtensions.cs" company="Project Contributors">
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
namespace Net.Http.OData
{
    using System;
    using System.Net.Http.Headers;

    public static class ODataMetadataLevelExtensions
    {
        public const string HeaderName = "odata.metadata";
        private static readonly NameValueHeaderValue MetadataLevelFull = new NameValueHeaderValue(HeaderName, "full");
        private static readonly NameValueHeaderValue MetadataLevelMinimal = new NameValueHeaderValue(HeaderName, "minimal");
        private static readonly NameValueHeaderValue MetadataLevelNone = new NameValueHeaderValue(HeaderName, "none");

        public static NameValueHeaderValue ToNameValueHeaderValue(this ODataMetadataLevel metadataLevel)
        {
            switch (metadataLevel)
            {
                case ODataMetadataLevel.Full:
                    return MetadataLevelFull;

                case ODataMetadataLevel.Minimal:
                    return MetadataLevelMinimal;

                case ODataMetadataLevel.None:
                    return MetadataLevelNone;

                default:
                    throw new NotSupportedException(metadataLevel.ToString());
            }
        }
    }
}