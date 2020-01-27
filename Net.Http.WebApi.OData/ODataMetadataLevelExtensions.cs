// -----------------------------------------------------------------------
// <copyright file="ODataMetadataLevelExtensions.cs" company="Project Contributors">
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
using System.Net.Http.Headers;
using Net.Http.OData;

namespace Net.Http.WebApi.OData
{
    /// <summary>
    /// A class containing extension methods for <see cref="ODataMetadataLevel"/>.
    /// </summary>
    public static class ODataMetadataLevelExtensions
    {
        /// <summary>
        /// Gets the name of the HTTP header.
        /// </summary>
        public const string HeaderName = "odata.metadata";

        private static readonly NameValueHeaderValue s_metadataLevelFull = new NameValueHeaderValue(HeaderName, "full");
        private static readonly NameValueHeaderValue s_metadataLevelMinimal = new NameValueHeaderValue(HeaderName, "minimal");
        private static readonly NameValueHeaderValue s_metadataLevelNone = new NameValueHeaderValue(HeaderName, "none");

        /// <summary>
        /// Gets the <see cref="NameValueHeaderValue"/> to represent the specified <see cref="ODataMetadataLevel"/>.
        /// </summary>
        /// <param name="metadataLevel">The <see cref="ODataMetadataLevel"/>.</param>
        /// <returns>The representative <see cref="NameValueHeaderValue"/>.</returns>
        public static NameValueHeaderValue ToNameValueHeaderValue(this ODataMetadataLevel metadataLevel)
        {
            switch (metadataLevel)
            {
                case ODataMetadataLevel.Full:
                    return s_metadataLevelFull;

                case ODataMetadataLevel.Minimal:
                    return s_metadataLevelMinimal;

                case ODataMetadataLevel.None:
                    return s_metadataLevelNone;

                default:
                    throw new NotSupportedException(metadataLevel.ToString());
            }
        }
    }
}
