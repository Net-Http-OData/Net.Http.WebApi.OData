// -----------------------------------------------------------------------
// <copyright file="SkipQueryOption.cs" company="Project Contributors">
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
    /// <summary>
    /// A class containing deserialised values from the $skip query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class SkipQueryOption : QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SkipQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        /// <exception cref="ODataException">If supplied, the $skip value should be an integer.</exception>
        public SkipQueryOption(string rawValue)
            : base(rawValue)
        {
            var equals = rawValue.IndexOf('=') + 1;
            var value = rawValue.Substring(equals, rawValue.Length - equals);

            int skip;

            if (int.TryParse(value, out skip))
            {
                this.Value = skip;
            }
            else
            {
                throw new ODataException(Messages.SkipRawValueInvalid);
            }
        }

        /// <summary>
        /// Gets the integer value of the $skip query option.
        /// </summary>
        public int Value
        {
            get;
        }
    }
}