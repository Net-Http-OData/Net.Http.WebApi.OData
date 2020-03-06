// -----------------------------------------------------------------------
// <copyright file="HttpConfigurationODataExtensions.cs" company="Project Contributors">
// Copyright Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Net.Http.Formatting;
using global::Net.Http.OData.Model;
using global::Net.Http.OData.Query;
using global::Net.Http.WebApi.OData;
using Net.Http.OData;

namespace System.Web.Http
{
    /// <summary>
    /// Contains extension methods for the <see cref="HttpConfiguration"/> class.
    /// </summary>
    public static class HttpConfigurationODataExtensions
    {
        /// <summary>
        /// Use OData services with the specified Entity Data Model with <see cref="StringComparer"/>.OrdinalIgnoreCase for the model name matching.
        /// </summary>
        /// <param name="configuration">The server configuration.</param>
        /// <param name="entityDataModelBuilderCallback">The call-back to configure the Entity Data Model.</param>
        public static void UseOData(
            this HttpConfiguration configuration,
            Action<EntityDataModelBuilder> entityDataModelBuilderCallback)
            => UseOData(configuration, entityDataModelBuilderCallback, StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Use OData services with the specified Entity Data Model.
        /// </summary>
        /// <param name="configuration">The server configuration.</param>
        /// <param name="entityDataModelBuilderCallback">The call-back to configure the Entity Data Model.</param>
        /// <param name="entitySetNameComparer">The comparer to use for the entty set name matching.</param>
        public static void UseOData(
            this HttpConfiguration configuration,
            Action<EntityDataModelBuilder> entityDataModelBuilderCallback,
            IEqualityComparer<string> entitySetNameComparer)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (entityDataModelBuilderCallback is null)
            {
                throw new ArgumentNullException(nameof(entityDataModelBuilderCallback));
            }

            configuration.Filters.Add(new ODataExceptionFilterAttribute());

            configuration.Formatters.JsonFormatter.AddQueryStringMapping("$format", "json", "application/json");

            ODataServiceOptions.Current = new ODataServiceOptions(
                ODataVersion.MinVersion,
                ODataVersion.MaxVersion,
                new[] { ODataIsolationLevel.None },
                new[] { "application/json", "text/plain" });

            configuration.MessageHandlers.Add(new ODataRequestDelegatingHandler(ODataServiceOptions.Current));

            configuration.ParameterBindingRules.Add(p => p.ParameterType == typeof(ODataQueryOptions) ? new ODataQueryOptionsHttpParameterBinding(p) : null);

            var entityDataModelBuilder = new EntityDataModelBuilder(entitySetNameComparer);
            entityDataModelBuilderCallback(entityDataModelBuilder);
            entityDataModelBuilder.BuildModel();
        }
    }
}
