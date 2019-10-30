// -----------------------------------------------------------------------
// <copyright file="EdmComplexType.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a complex type in the Entity Data Model.
    /// </summary>
    /// <seealso cref="Net.Http.WebApi.OData.Model.EdmType" />
    [System.Diagnostics.DebuggerDisplay("{FullName}: {ClrType}")]
    public sealed class EdmComplexType : EdmType
    {
        internal EdmComplexType(Type clrType, IReadOnlyList<EdmProperty> properties)
            : this(clrType, null, properties)
        {
        }

        internal EdmComplexType(Type clrType, EdmType baseType, IReadOnlyList<EdmProperty> properties)
            : base(clrType.Name, clrType.FullName, clrType)
        {
            this.BaseType = baseType;
            this.Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        /// <summary>
        /// Gets the <see cref="EdmType"/> from which the current <see cref="EdmComplexType"/> directly inherits.
        /// </summary>
        public EdmType BaseType { get; }

        /// <summary>
        /// Gets the properties defined on the type.
        /// </summary>
        public IReadOnlyList<EdmProperty> Properties { get; }

        /// <summary>
        /// Gets the property with the specified name.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The <see cref="EdmProperty"/> declared in this type with the specified name.</returns>
        /// <exception cref="System.ArgumentException">The type does not contain a property with the specified name.</exception>
        public EdmProperty GetProperty(string name)
        {
            for (int i = 0; i < this.Properties.Count; i++)
            {
                var property = this.Properties[i];

                if (property.Name.Equals(name, StringComparison.Ordinal))
                {
                    return property;
                }
            }

            throw new ArgumentException($"The type '{this.FullName}' does not contain a property named '{name}'");
        }
    }
}