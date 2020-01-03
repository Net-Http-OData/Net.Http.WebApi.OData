// -----------------------------------------------------------------------
// <copyright file="Capabilities.cs" company="Project Contributors">
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
namespace Net.Http.OData.Model
{
    using System;

    /// <summary>
    /// Specifies the permitted capabilities of an <see cref="EntitySet"/> in the <see cref="EntityDataModel"/>.
    /// </summary>
    [Flags]
    public enum Capabilities
    {
        /// <summary>
        /// The Entity Set cannot be modified, just queried.
        /// </summary>
        None = 0,

        /// <summary>
        /// Entity records can be inserted.
        /// </summary>
        Insertable = 1,

        /// <summary>
        /// Entity records can be updated.
        /// </summary>
        Updatable = 2,

        /// <summary>
        /// Entity records can be deleted.
        /// </summary>
        Deletable = 3,
    }
}