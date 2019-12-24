// -----------------------------------------------------------------------
// <copyright file="EdmProperty.cs" company="Project Contributors">
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
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    /// <summary>
    /// A class which represents an entity property in the Entity Data Model.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{Name}")]
    public sealed class EdmProperty
    {
        private readonly Func<EdmType, bool> isNavigableFunc;

        /// <summary>
        /// Initialises a new instance of the <see cref="EdmProperty" /> class.
        /// </summary>
        /// <param name="propertyInfo">The PropertyInfo for the property.</param>
        /// <param name="propertyType">Type of the edm.</param>
        /// <param name="declaringType">Type of the declaring.</param>
        /// <param name="isNavigableFunc">A function which indicates whether the property is a navigation property.</param>
        /// <exception cref="ArgumentNullException">Constructor argument not specified.</exception>
        internal EdmProperty(PropertyInfo propertyInfo, EdmType propertyType, EdmComplexType declaringType, Func<EdmType, bool> isNavigableFunc)
        {
            this.Name = propertyInfo?.Name ?? throw new ArgumentNullException(nameof(propertyInfo));
            this.PropertyType = propertyType ?? throw new ArgumentNullException(nameof(propertyType));
            this.DeclaringType = declaringType ?? throw new ArgumentNullException(nameof(declaringType));
            this.isNavigableFunc = isNavigableFunc;

            this.IsNullable = Nullable.GetUnderlyingType(propertyType.ClrType) != null
                || ((propertyType.ClrType.IsClass || propertyType.ClrType.IsInterface) && propertyInfo.GetCustomAttribute<RequiredAttribute>() == null);
        }

        /// <summary>
        /// Gets the type in the Entity Data Model which declares this property.
        /// </summary>
        public EdmComplexType DeclaringType { get; }

        /// <summary>
        /// Gets a value indicating whether the property is navigable (i.e. a navigation property).
        /// </summary>
        public bool IsNavigable => this.isNavigableFunc((this.PropertyType as EdmCollectionType)?.ContainedType ?? this.PropertyType);

        /// <summary>
        /// Gets a value indicating whether the property is nullable.
        /// </summary>
        public bool IsNullable { get; }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the property in the Entity Data Model.
        /// </summary>
        public EdmType PropertyType { get; }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => this.Name;
    }
}