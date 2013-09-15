// -----------------------------------------------------------------------
// <copyright file="OrderByProperty.cs" company="MicroLite">
// Copyright 2012-2013 Trevor Pilley
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

    /// <summary>
    /// A class containing deserialised values from the $order by query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class OrderByProperty
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OrderByProperty"/> class.
        /// </summary>
        /// <param name="rawValue">The raw value.</param>
        /// <exception cref="ODataException">If supplied, the direction should be either 'asc' or 'desc'.</exception>
        public OrderByProperty(string rawValue)
        {
            if (rawValue == null)
            {
                throw new ArgumentNullException("rawValue");
            }

            this.RawValue = rawValue;

            var pieces = rawValue.Split(' ');
            this.Name = pieces[0];

            if (pieces.Length == 2)
            {
                switch (pieces[1])
                {
                    case "asc":
                        this.Direction = OrderByDirection.Ascending;
                        break;

                    case "desc":
                        this.Direction = OrderByDirection.Descending;
                        break;

                    default:
                        throw new ODataException(Messages.OrderByPropertyRawValueInvalid);
                }
            }
        }

        /// <summary>
        /// Gets the direction the property should be ordered by.
        /// </summary>
        public OrderByDirection Direction
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the property to order by.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the raw request value.
        /// </summary>
        public string RawValue
        {
            get;
            private set;
        }
    }
}