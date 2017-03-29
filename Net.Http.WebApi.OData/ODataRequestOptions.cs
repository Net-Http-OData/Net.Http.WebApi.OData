// -----------------------------------------------------------------------
// <copyright file="ODataRequestOptions.cs" company="Project Contributors">
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
    using System.Net.Http;

    /// <summary>
    /// Contains OData options for the request.
    /// </summary>
    public sealed class ODataRequestOptions
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ODataRequestOptions"/> class.
        /// </summary>
        /// <param name="request">The current http request message.</param>
        internal ODataRequestOptions(HttpRequestMessage request)
        {
            this.MetadataLevel = request.ReadMetadataLevel();
        }

        /// <summary>
        /// Gets the metadata level requested by the client.
        /// </summary>
        public MetadataLevel MetadataLevel
        {
            get;
        }
    }
}