// -----------------------------------------------------------------------
// <copyright file="EdmEnumType.cs" company="Project Contributors">
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
    using System.Collections.Generic;

    /// <summary>
    /// Represents an Enum type in the Entity Data Model.
    /// </summary>
    /// <seealso cref="Net.Http.WebApi.OData.Model.EdmType" />
    [System.Diagnostics.DebuggerDisplay("{Name}: {ClrType}")]
    public sealed class EdmEnumType : EdmType
    {
        internal EdmEnumType(string name, Type clrType, IReadOnlyList<EdmEnumMember> members)
            : base(name, clrType)
        {
            if (members == null)
            {
                throw new ArgumentNullException(nameof(members));
            }

            this.Members = members;
        }

        /// <summary>
        /// Gets the enum members.
        /// </summary>
        public IReadOnlyList<EdmEnumMember> Members
        {
            get;
        }

        /// <summary>
        /// Gets the CLR Enum value for the specified Enum member in the Entity Data Model.
        /// </summary>
        /// <param name="edmEnumMember">The Enum member in the Entity Data Model.</param>
        /// <returns>An object containing the CLR Enum value.</returns>
        public object GetClrValue(EdmEnumMember edmEnumMember) => Enum.ToObject(this.ClrType, edmEnumMember.Value);

        /// <summary>
        /// Gets the member with the specified name.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <returns>The <see cref="EdmEnumMember"/> declared in this type with the specified name.</returns>
        /// <exception cref="System.ArgumentException">The type does not contain a member with the specified name.</exception>
        public EdmEnumMember GetMember(string name)
        {
            for (int i = 0; i < this.Members.Count; i++)
            {
                var member = this.Members[i];

                if (member.Name == name)
                {
                    return member;
                }
            }

            throw new ArgumentException($"The type '{this.Name}' does not contain a member named '{name}'");
        }
    }
}