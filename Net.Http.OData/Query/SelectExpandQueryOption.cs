// -----------------------------------------------------------------------
// <copyright file="SelectExpandQueryOption.cs" company="Project Contributors">
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
    using System.Collections.Generic;
    using System.Linq;
    using Net.Http.OData.Model;

    /// <summary>
    /// A class containing deserialised values from the $select or $expand query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class SelectExpandQueryOption : QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SelectExpandQueryOption" /> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        /// <param name="model">The model.</param>
        public SelectExpandQueryOption(string rawValue, EdmComplexType model)
            : base(rawValue)
        {
            if (model is null)
            {
                throw new System.ArgumentNullException(nameof(model));
            }

            if (rawValue == "$select=*")
            {
                this.PropertyPaths = model.Properties.Where(p => !p.IsNavigable).Select(p => new PropertyPathSegment(p)).ToList();
            }
            else if (rawValue == "$expand=*")
            {
                this.PropertyPaths = model.Properties.Where(p => p.IsNavigable).Select(p => new PropertyPathSegment(p)).ToList();
            }
            else
            {
                var equals = rawValue.IndexOf('=') + 1;

                var properties = rawValue.Substring(equals, rawValue.Length - equals)
                    .Split(SplitCharacter.Comma)
                    .Select(p => PropertyPathSegment.For(p, model))
                    .ToList();

                this.PropertyPaths = properties;
            }
        }

        /// <summary>
        /// Gets the property paths specified in the query.
        /// </summary>
        public IReadOnlyList<PropertyPathSegment> PropertyPaths { get; }
    }
}