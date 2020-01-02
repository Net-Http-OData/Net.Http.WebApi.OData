// -----------------------------------------------------------------------
// <copyright file="ODataError.cs" company="Project Contributors">
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
    using System.Runtime.Serialization;

    [DataContract]
    internal sealed class ODataError
    {
        internal ODataError(string code, string message, string target)
        {
            this.Code = code;
            this.Message = message;
            this.Target = target;
        }

        [Newtonsoft.Json.JsonProperty("code", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 0)]
        public string Code { get; }

        [Newtonsoft.Json.JsonProperty("message", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 1)]
        public string Message { get; }

        [Newtonsoft.Json.JsonProperty("target", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, Order = 2)]
        public string Target { get; }
    }
}