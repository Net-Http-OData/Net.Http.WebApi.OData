// -----------------------------------------------------------------------
// <copyright file="SkipTokenQueryOption.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query
{
    /// <summary>
    /// A class containing deserialised values from the $skiptoken query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class SkipTokenQueryOption : QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SkipTokenQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        internal SkipTokenQueryOption(string rawValue)
            : base(rawValue)
        {
        }
    }
}