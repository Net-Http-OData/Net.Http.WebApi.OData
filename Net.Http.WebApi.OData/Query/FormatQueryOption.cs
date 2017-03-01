// -----------------------------------------------------------------------
// <copyright file="FormatQueryOption.cs" company="Project Contributors">
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
    using System.Net.Http.Headers;

    /// <summary>
    /// A class containing deserialised values from the $format query option.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{RawValue}")]
    public sealed class FormatQueryOption : QueryOption
    {
        private static readonly MediaTypeHeaderValue AtomXml = new MediaTypeHeaderValue("application/atom+xml");
        private static readonly MediaTypeHeaderValue Json = new MediaTypeHeaderValue("application/json");
        private static readonly MediaTypeHeaderValue Xml = new MediaTypeHeaderValue("application/xml");

        /// <summary>
        /// Initialises a new instance of the <see cref="FormatQueryOption"/> class.
        /// </summary>
        /// <param name="rawValue">The raw request value.</param>
        public FormatQueryOption(string rawValue)
            : base(rawValue)
        {
            var equals = rawValue.IndexOf('=') + 1;
            var value = rawValue.Substring(equals, rawValue.Length - equals);

            switch (value)
            {
                case "atom":
                    this.MediaTypeHeaderValue = AtomXml;
                    break;

                case "json":
                    this.MediaTypeHeaderValue = Json;
                    break;

                case "xml":
                    this.MediaTypeHeaderValue = Xml;
                    break;

                default:
                    this.MediaTypeHeaderValue = new MediaTypeHeaderValue(value);
                    break;
            }
        }

        /// <summary>
        /// Gets the media type header value.
        /// </summary>
        public MediaTypeHeaderValue MediaTypeHeaderValue
        {
            get;
        }
    }
}