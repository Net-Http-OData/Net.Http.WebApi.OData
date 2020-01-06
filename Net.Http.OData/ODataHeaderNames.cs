// -----------------------------------------------------------------------
// <copyright file="ODataHeaderNames.cs" company="Project Contributors">
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
    /// The header names for OData HTTP Headers.
    /// </summary>
    public static class ODataHeaderNames
    {
        /// <summary>
        /// The OData isolation header.
        /// </summary>
        public const string ODataIsolation = "OData-Isolation";

        /// <summary>
        /// The OData max version header.
        /// </summary>
        public const string ODataMaxVersion = "OData-MaxVersion";

        /// <summary>
        /// The OData version header.
        /// </summary>
        public const string ODataVersion = "OData-Version";
    }
}