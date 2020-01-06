// -----------------------------------------------------------------------
// <copyright file="ODataMetadataLevel.cs" company="Project Contributors">
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
    /// <summary>
    /// The different levels of metadata which should be included in the response.
    /// </summary>
    public enum ODataMetadataLevel
    {
        /// <summary>
        /// No metadata should be included in the response.
        /// </summary>
        None = 0,

        /// <summary>
        /// The minimal metadata should be included in the response.
        /// </summary>
        Minimal = 1,

        /// <summary>
        /// The full metadata should be included in the response.
        /// </summary>
        Full = 2,
    }
}