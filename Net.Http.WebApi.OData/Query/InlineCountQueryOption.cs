// -----------------------------------------------------------------------
// <copyright file="InlineCountQueryOption.cs" company="MicroLite">
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
    /// A class containing deserialised values from the $inline count query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class InlineCountQueryOption
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="InlineCountQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        /// <exception cref="ODataException">Thrown if the raw value is invalid.</exception>
        public InlineCountQueryOption(string rawValue)
        {
            if (rawValue == null)
            {
                throw new ArgumentNullException("rawValue");
            }

            this.RawValue = rawValue;

            var pieces = rawValue.Split('=');

            switch (pieces[1])
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