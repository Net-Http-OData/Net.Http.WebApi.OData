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

    /// <summary>
    /// A class which represents an entity property in the Entity Data Model.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Name}")]
    public sealed class EdmProperty
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EdmProperty" /> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="propertyType">Type of the edm.</param>
        /// <param name="declaringType">Type of the declaring.</param>
        /// <exception cref="System.ArgumentException">Property name must be specified</exception>
        /// <exception cref="System.ArgumentNullException">Property type and declaring type must be specified.</exception>
        internal EdmProperty(string name, EdmType propertyType, EdmComplexType declaringType)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Property name must be specified", nameof(name));
            }

            if (propertyType == null)
            {
                throw new ArgumentNullException(nameof(propertyType));
            }

            if (declaringType == null)
            {
                throw new ArgumentNullException(nameof(declaringType));
            }

            this.Name = name;
            this.PropertyType = propertyType;
            this.DeclaringType = declaringType;
        }

        /// <summary>
        /// Gets the type in the Entity Data Model which declares this property.
        /// </summary>
        public EdmComplexType DeclaringType
        {
            get;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string Name
        {
            get;
        }

        /// <summary>
        /// Gets the type of the property in the Entity Data Model.
        /// </summary>
        public EdmType PropertyType
        {
            get;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => this.Name;
    }
}