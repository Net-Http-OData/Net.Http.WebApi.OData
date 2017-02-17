// -----------------------------------------------------------------------
// <copyright file="InlineCountQueryOption.cs" company="Project Contributors">
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
    /// A class containing deserialised values from the $inline count query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class InlineCountQueryOption : QueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="InlineCountQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        /// <exception cref="ODataException">Thrown if the raw value is invalid.</exception>
        public InlineCountQueryOption(string rawValue)
            : base(rawValue)
        {
            var equals = rawValue.IndexOf('=') + 1;
            var value = rawValue.Substring(equals, rawValue.Length - equals);

            switch (value)
            {
                case "allpages":
                    this.InlineCount = InlineCount.AllPages;
                    break;

                case "none":
                    this.InlineCount = InlineCount.None;
                    break;

                default:
                    throw new ODataException(Messages.InlineCountRawValueInvalid);
            }
        }

        /// <summary>
        /// Gets the inline count.
        /// </summary>
        public InlineCount InlineCount
        {
            get;
        }
    }
}