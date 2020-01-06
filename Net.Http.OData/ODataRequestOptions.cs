// -----------------------------------------------------------------------
// <copyright file="ODataRequestOptions.cs" company="Project Contributors">
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

    /// <summary>
    /// Contains OData options for the request.
    /// </summary>
    public sealed class ODataRequestOptions
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ODataRequestOptions"/> class.
        /// </summary>
        /// <param name="dataServiceUri">The root URI of the OData Service.</param>
        /// <param name="isolationLevel">The OData-Isolation requested by the client, or None if not otherwise specified.</param>
        /// <param name="metadataLevel">The odata.metadata level specified in the ACCEPT header by the client, or Minimal if not otherwise specified.</param>
        public ODataRequestOptions(Uri dataServiceUri, ODataIsolationLevel isolationLevel, ODataMetadataLevel metadataLevel)
        {
            this.DataServiceUri = dataServiceUri;
            this.IsolationLevel = isolationLevel;
            this.MetadataLevel = metadataLevel;
        }

        /// <summary>
        /// Gets the root URI of the OData Service.
        /// </summary>
        public Uri DataServiceUri { get; }

        /// <summary>
        /// Gets the OData-Isolation requested by the client, or None if not otherwise specified.
        /// </summary>
        public ODataIsolationLevel IsolationLevel { get; }

        /// <summary>
        /// Gets the odata.metadata level specified in the ACCEPT header by the client, or Minimal if not otherwise specified.
        /// </summary>
        public ODataMetadataLevel MetadataLevel { get; }
    }
}