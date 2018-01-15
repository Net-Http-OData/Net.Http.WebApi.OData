// -----------------------------------------------------------------------
// <copyright file="EdmEnumMember.cs" company="Project Contributors">
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
    /// <summary>
    /// A class which represents an enum member in the Entity Data Model.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Value}: {Name}")]
    public sealed class EdmEnumMember
    {
        internal EdmEnumMember(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets the name of the enum member.
        /// </summary>
        public string Name
        {
            get;
        }

        /// <summary>
        /// Gets the integer value of the enum member.
        /// </summary>
        public int Value
        {
            get;
        }
    }
}