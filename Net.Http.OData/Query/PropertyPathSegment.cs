// -----------------------------------------------------------------------
// <copyright file="PropertyPathSegment.cs" company="Project Contributors">
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
namespace Net.Http.OData.Query
{
    using System;
    using Net.Http.OData.Model;

    /// <summary>
    /// A class which represents a segment of a property path.
    /// </summary>
    public sealed class PropertyPathSegment
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PropertyPathSegment"/> class representing the end of a path segment.
        /// </summary>
        /// <param name="property">The <see cref="EdmProperty"/> that the path segment represents.</param>
        public PropertyPathSegment(EdmProperty property)
            : this(property, null)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="PropertyPathSegment"/> class representing part of a path segment.
        /// </summary>
        /// <param name="property">The <see cref="EdmProperty"/> that the path segment represents.</param>
        /// <param name="next">The next <see cref="PropertyPathSegment"/> in the property path.</param>
        public PropertyPathSegment(EdmProperty property, PropertyPathSegment next)
        {
            this.Property = property ?? throw new ArgumentNullException(nameof(property));
            this.Next = next;
        }

        /// <summary>
        /// Gets the next property in the path being referenced in the query.
        /// </summary>
        public PropertyPathSegment Next { get; }

        /// <summary>
        /// Gets the property being referenced in the query.
        /// </summary>
        public EdmProperty Property { get; }

        /// <summary>
        /// Creates the <see cref="PropertyPathSegment"/> for the given property path.
        /// </summary>
        /// <param name="propertyPath">The property path.</param>
        /// <param name="edmComplexType">The <see cref="EdmComplexType"/> which contains the first property in the property path</param>
        /// <returns>The <see cref="PropertyPathSegment"/> for the given property path.</returns>
        internal static PropertyPathSegment For(string propertyPath, EdmComplexType edmComplexType)
        {
            if (propertyPath.IndexOf('/') == -1)
            {
                return new PropertyPathSegment(edmComplexType.GetProperty(propertyPath));
            }

            var nameSegments = propertyPath.Split(SplitCharacter.ForwardSlash);

            var edmProperties = new EdmProperty[nameSegments.Length];

            var model = edmComplexType;

            for (int i = 0; i < nameSegments.Length; i++)
            {
                edmProperties[i] = model.GetProperty(nameSegments[i]);
                model = edmProperties[i].PropertyType as EdmComplexType;
            }

            PropertyPathSegment propertyPathSegment = null;

            for (int i = edmProperties.Length - 1; i >= 0; i--)
            {
                propertyPathSegment = new PropertyPathSegment(edmProperties[i], propertyPathSegment);
            }

            return propertyPathSegment;
        }
    }
}