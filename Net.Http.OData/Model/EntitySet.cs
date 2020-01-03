// -----------------------------------------------------------------------
// <copyright file="EntitySet.cs" company="Project Contributors">
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
    /// Represents an Entity Set in the Entity Data Model.
    /// </summary>
    public sealed class EntitySet
    {
        internal EntitySet(string name, EdmComplexType edmType, EdmProperty entityKey, Capabilities capabilities)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Entity Set name must be specified", nameof(name));
            }

            this.Name = name;
            this.EdmType = edmType ?? throw new ArgumentNullException(nameof(edmType));
            this.EntityKey = entityKey;
            this.Capabilities = capabilities;
        }

        /// <summary>
        /// Gets the <see cref="Capabilities"/> of the Entity Set.
        /// </summary>
        public Capabilities Capabilities { get; }

        /// <summary>
        /// Gets the <see cref="EdmComplexType"/> of the entities in the set.
        /// </summary>
        public EdmComplexType EdmType { get; }

        /// <summary>
        /// Gets the entity key property.
        /// </summary>
        public EdmProperty EntityKey { get; }

        /// <summary>
        /// Gets the name of the Entity Set.
        /// </summary>
        public string Name { get; }
    }
}