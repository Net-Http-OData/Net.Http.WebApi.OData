// -----------------------------------------------------------------------
// <copyright file="TopQueryOption.cs" company="MicroLite">
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
    /// A class containing deserialised values from the $top query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class TopQueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TopQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        /// <exception cref="ODataException">If supplied, the $top value should be an integer.</exception>
        public TopQueryOption(string rawValue)
        {
            if (rawValue == null)
            {
                throw new ArgumentNullException("rawValue");
            }

            this.RawValue = rawValue;

            var pieces = rawValue.Split('=');
            int top;

            if (int.TryParse(pieces[1], out top))
            {
                this.Value = top;
            }
            else
            {
                throw new ODataException(Messages.TopRawValueInvalid);
            }
        }

        /// <summary>
        /// Gets the raw request value.
        /// </summary>
        public string RawValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the integer value of the $top query option.
        /// </summary>
        public int Value
        {
            get;
            private set;
        }
    }
}