// -----------------------------------------------------------------------
// <copyright file="EdmCollectionType.cs" company="Project Contributors">
// Copyright 2012 - 2019 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData.Model
{
    using System;

    /// <summary>
    /// Represents a collection type in the Entity Data Model.
    /// </summary>
    /// <seealso cref="EdmType"/>
    [System.Diagnostics.DebuggerDisplay("{FullName}: {ClrType}")]
    public sealed class EdmCollectionType : EdmType
    {
        internal EdmCollectionType(Type clrType, EdmType containedType)
            : base("Collection", $"Collection({containedType.FullName})", clrType)
        {
            this.ContainedType = containedType ?? throw new ArgumentNullException(nameof(containedType));
        }

        /// <summary>
        /// Gets the <see cref="EdmType"/> type contained in the collection.
        /// </summary>
        public EdmType ContainedType { get; }
    }
}