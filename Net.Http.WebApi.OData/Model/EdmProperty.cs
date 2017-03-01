// -----------------------------------------------------------------------
// <copyright file="EdmProperty.cs" company="Project Contributors">
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
    using System.Collections.Concurrent;

    /// <summary>
    /// A class which represents an entity property in the entity data model.
    /// </summary>
    public sealed class EdmProperty
    {
        private static readonly ConcurrentDictionary<string, EdmProperty> EdmPropertyCache = new ConcurrentDictionary<string, EdmProperty>();

        /// <summary>
        /// Initialises a new instance of the <see cref="EdmProperty"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <exception cref="System.ArgumentException">Property name must be specified</exception>
        private EdmProperty(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Property name must be specified", nameof(name));
            }

            this.Name = name;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string Name
        {
            get;
        }

        internal static EdmProperty From(string propertyName)
        {
            return EdmPropertyCache.GetOrAdd(propertyName, propName => new EdmProperty(propName));
        }
    }
}