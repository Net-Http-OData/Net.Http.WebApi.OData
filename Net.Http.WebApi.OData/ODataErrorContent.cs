// -----------------------------------------------------------------------
// <copyright file="ODataErrorContent.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData
{
    using System.Globalization;
    using System.Runtime.Serialization;

    [DataContract]
    internal sealed class ODataErrorContent
    {
        private ODataErrorContent(ODataError error) => this.Error = error;

        [Newtonsoft.Json.JsonProperty("error", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 0)]
        public ODataError Error { get; }

        internal static ODataErrorContent Create(int code, string message, string target)
            => new ODataErrorContent(new ODataError(code.ToString(CultureInfo.InvariantCulture), message, target));
    }
}