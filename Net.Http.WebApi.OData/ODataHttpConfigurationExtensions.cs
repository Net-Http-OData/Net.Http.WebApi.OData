// -----------------------------------------------------------------------
// <copyright file="ODataHttpConfigurationExtensions.cs" company="Project Contributors">
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
using System.Globalization;
using System.Net.Http.Formatting;
using Net.Http.OData;
using Net.Http.OData.Model;
using Net.Http.OData.Query;
using Net.Http.OData.Query.Parsers;
using Net.Http.WebApi.OData;

namespace System.Web.Http
{
    /// <summary>
    /// Extension methods for <see cref="HttpConfiguration"/> to add OData.
    /// </summary>
    public static class ODataHttpConfigurationExtensions
    {
        /// <summary>
        /// Adds OData services with the specified Entity Data Model with <see cref="DateTimeStyles.AssumeUniversal"/>
        /// for parsing <see cref="DateTimeOffset"/>s, and <see cref="StringComparer"/>.OrdinalIgnoreCase for the model name matching.
        /// </summary>
        /// <param name="configuration">The <see cref="HttpConfiguration"/>.</param>
        /// <param name="entityDataModelBuilderCallback">The call-back to configure the Entity Data Model.</param>
        public static void UseOData(
            this HttpConfiguration configuration,
            Action<EntityDataModelBuilder> entityDataModelBuilderCallback)
            => UseOData(configuration, entityDataModelBuilderCallback, ParserSettings.DateTimeStyles);

        /// <summary>
        /// Adds OData services with the specified Entity Data Model with the specified <see cref="DateTimeStyles"/>
        /// for parsing <see cref="DateTimeOffset"/>s, and <see cref="StringComparer"/>.OrdinalIgnoreCase for the model name matching.
        /// </summary>
        /// <param name="configuration">The <see cref="HttpConfiguration"/>.</param>
        /// <param name="entityDataModelBuilderCallback">The call-back to configure the Entity Data Model.</param>
        /// <param name="dateTimeOffsetParserStyle">The <see cref="DateTimeStyles"/> to use for parsing <see cref="DateTimeOffset"/> if no timezone is specified in the OData query.</param>
        public static void UseOData(
            this HttpConfiguration configuration,
            Action<EntityDataModelBuilder> entityDataModelBuilderCallback,
            DateTimeStyles dateTimeOffsetParserStyle)
            => UseOData(configuration, entityDataModelBuilderCallback, dateTimeOffsetParserStyle, StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Adds OData services with the specified Entity Data Model with the specified <see cref="DateTimeStyles"/>
        /// for parsing <see cref="DateTimeOffset"/>s, and <see cref="IEqualityComparer{T}"/> for the model name matching.
        /// </summary>
        /// <param name="configuration">The <see cref="HttpConfiguration"/>.</param>
        /// <param name="entityDataModelBuilderCallback">The call-back to configure the Entity Data Model.</param>
        /// <param name="dateTimeOffsetParserStyle">The <see cref="DateTimeStyles"/> to use for parsing <see cref="DateTimeOffset"/> if no timezone is specified in the OData query.</param>
        /// <param name="entitySetNameComparer">The comparer to use for the entty set name matching.</param>
        public static void UseOData(
            this HttpConfiguration configuration,
            Action<EntityDataModelBuilder> entityDataModelBuilderCallback,
            DateTimeStyles dateTimeOffsetParserStyle,
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

            ODataServiceOptions.Current = new ODataServiceOptions(
                ODataVersion.MinVersion,
                ODataVersion.MaxVersion,
                new[] { ODataIsolationLevel.None },
                new[] { "application/json", "text/plain" });

            configuration.MessageHandlers.Add(new ODataRequestDelegatingHandler(ODataServiceOptions.Current));

            configuration.Filters.Add(new ODataExceptionFilterAttribute());

            configuration.Formatters.JsonFormatter.AddQueryStringMapping("$format", "json", "application/json");

            configuration.ParameterBindingRules.Add(p => p.ParameterType == typeof(ODataQueryOptions) ? new ODataQueryOptionsHttpParameterBinding(p) : null);

            ParserSettings.DateTimeStyles = dateTimeOffsetParserStyle;

            var entityDataModelBuilder = new EntityDataModelBuilder(entitySetNameComparer);
            entityDataModelBuilderCallback(entityDataModelBuilder);
            entityDataModelBuilder.BuildModel();
        }
    }
}
