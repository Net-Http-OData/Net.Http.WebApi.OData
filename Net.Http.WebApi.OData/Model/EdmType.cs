// -----------------------------------------------------------------------
// <copyright file="EdmType.cs" company="Project Contributors">
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

    /// <summary>
    /// Represents a type in the Entity Data Model.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{FullName}: {ClrType}")]
    public abstract class EdmType : IEquatable<EdmType>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EdmType"/> class.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="fullName">The full name of the type.</param>
        /// <param name="clrType">The CLR type.</param>
        protected EdmType(string name, string fullName, Type clrType)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must be specified", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("FullName must be specified", nameof(fullName));
            }

            this.Name = name;
            this.FullName = fullName;
            this.ClrType = clrType ?? throw new ArgumentNullException(nameof(clrType));
        }

        /// <summary>
        /// Gets the CLR type.
        /// </summary>
        public Type ClrType
        {
            get;
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string FullName
        {
            get;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get;
        }

        /// <summary>
        /// Gets the type with the specified name in the Entity Data Model.
        /// </summary>
        /// <param name="edmTypeName">Name of the type in the Entity Data Model.</param>
        /// <returns>The EdmType with the specified name, if found; otherwise, null.</returns>
        public static EdmType GetEdmType(string edmTypeName)
        {
            foreach (var edmType in EdmTypeCache.Map.Values)
            {
                if (edmType.FullName.Equals(edmTypeName))
                {
                    return edmType;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the type for the specified CLR type in the Entity Data Model.
        /// </summary>
        /// <param name="clrType">The CLR type to find in the Entity Data Model.</param>
        /// <returns>The EdmType for the specified CLR type, if found; otherwise, null.</returns>
        public static EdmType GetEdmType(Type clrType)
        {
            if (EdmTypeCache.Map.TryGetValue(clrType, out EdmType edmType))
            {
                return edmType;
            }

            return null;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) => this.Equals(obj as EdmType);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(EdmType other)
        {
            if (other == null)
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return this.ClrType.Equals(other.ClrType);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode() => this.ClrType.GetHashCode();

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => this.FullName;
    }
}