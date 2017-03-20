// -----------------------------------------------------------------------
// <copyright file="EdmType.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Model
{
    using System;

    /// <summary>
    /// Represents a type in the Entity Data Model.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Name}: {ClrType}")]
    public abstract class EdmType : IEquatable<EdmType>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EdmType"/> class.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="clrType">The CLR type.</param>
        protected EdmType(string name, Type clrType)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name must be specified", nameof(name));
            }

            if (clrType == null)
            {
                throw new ArgumentNullException(nameof(clrType));
            }

            this.Name = name;
            this.ClrType = clrType;
        }

        /// <summary>
        /// Gets the CLR type.
        /// </summary>
        public Type ClrType
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
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
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

            return this.ClrType.Equals(other.ClrType) && this.Name.Equals(other.Name);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode() => this.ClrType.GetHashCode() ^ this.Name.GetHashCode();

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => this.Name;
    }
}