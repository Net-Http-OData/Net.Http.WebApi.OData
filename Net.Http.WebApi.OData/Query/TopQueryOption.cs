// -----------------------------------------------------------------------
// <copyright file="TopQueryOption.cs" company="Project Contributors">
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

    /// <summary>
    /// A class containing deserialised values from the $top query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class TopQueryOption : QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TopQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        /// <exception cref="ODataException">If supplied, the $top value should be an integer.</exception>
        public TopQueryOption(string rawValue)
            : base(rawValue)
        {
            var equals = rawValue.IndexOf('=') + 1;
            var value = rawValue.Substring(equals, rawValue.Length - equals);

            int top;

            if (int.TryParse(value, out top))
            {
                this.Value = top;
            }
            else
            {
                throw new ODataException(Messages.TopRawValueInvalid);
            }
        }

        /// <summary>
        /// Gets the integer value of the $top query option.
        /// </summary>
        public int Value
        {
            get;
        }
    }
}