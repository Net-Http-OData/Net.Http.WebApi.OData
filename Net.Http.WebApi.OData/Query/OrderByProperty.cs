// -----------------------------------------------------------------------
// <copyright file="OrderByProperty.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query
{
    using System;
    using Model;

    /// <summary>
    /// A class containing deserialised values from the $orderby query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class OrderByProperty
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OrderByProperty"/> class.
        /// </summary>
        /// <param name="rawValue">The raw value.</param>
        /// <param name="model">The model.</param>
        /// <exception cref="ArgumentNullException">Thrown if raw value or model are null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If supplied, the direction should be either 'asc' or 'desc'.</exception>
        internal OrderByProperty(string rawValue, EdmComplexType model)
        {
            if (rawValue == null)
            {
                throw new ArgumentNullException(nameof(rawValue));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            this.RawValue = rawValue;

            var space = rawValue.IndexOf(' ');

            if (space == -1)
            {
                this.Property = model.GetProperty(rawValue);
            }
            else
            {
                this.Property = model.GetProperty(rawValue.Substring(0, space));

                switch (rawValue.Substring(space + 1, rawValue.Length - (space + 1)))
                {
                    case "asc":
                        this.Direction = OrderByDirection.Ascending;
                        break;

                    case "desc":
                        this.Direction = OrderByDirection.Descending;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(rawValue), Messages.OrderByPropertyRawValueInvalid);
                }
            }
        }

        /// <summary>
        /// Gets the direction the property should be ordered by.
        /// </summary>
        public OrderByDirection Direction
        {
            get;
        }

        /// <summary>
        /// Gets the property to order by.
        /// </summary>
        public EdmProperty Property
        {
            get;
        }

        /// <summary>
        /// Gets the raw request value.
        /// </summary>
        public string RawValue
        {
            get;
        }
    }
}