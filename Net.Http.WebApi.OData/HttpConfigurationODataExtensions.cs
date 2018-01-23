// -----------------------------------------------------------------------
// <copyright file="HttpConfigurationODataExtensions.cs" company="Project Contributors">
// Copyright 2012 - 2018 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace System.Web.Http
{
    using System;
    using global::Net.Http.WebApi.OData;

    /// <summary>
    /// Contains extension methods for the <see cref="HttpConfiguration"/> class.
    /// </summary>
    public static class HttpConfigurationODataExtensions
    {
        /// <summary>
        /// Use OData services with the specified Entity Data Model.
        /// </summary>
        /// <param name="configuration">The server configuration.</param>
        public static void UseOData(this HttpConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            configuration.Filters.Add(new ODataExceptionFilterAttribute());
        }
    }
}