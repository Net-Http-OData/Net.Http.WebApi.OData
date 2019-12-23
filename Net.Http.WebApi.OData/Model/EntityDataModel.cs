// -----------------------------------------------------------------------
// <copyright file="EntityDataModel.cs" company="Project Contributors">
// Copyright 2012 - 2019 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// A class which represents the Entity Data Model.
    /// </summary>
    public sealed class EntityDataModel
    {
        internal EntityDataModel(IReadOnlyDictionary<string, EntitySet> entitySets)
        {
            this.EntitySets = entitySets;

            this.FilterFunctions = new[]
            {
                "cast",
                "isof",
                "endswith",
                "startswith",
                "contains",
                "tolower",
                "toupper",
                "trim",
                "length",
                "indexof",
                "replace",
                "substring",
                "concat",
                "year",
                "month",
                "day",
                "hour",
                "minute",
                "second",
                "fractionalseconds",
                "now",
                "mindatetime",
                "maxdatetime",
                "round",
                "ceiling",
                "floor",
            };

            this.SupportedFormats = new[]
            {
                "application/json;odata.metadata=none",
                "application/json;odata.metadata=minimal",
            };
        }

        /// <summary>
        /// Gets the current Entity Data Model.
        /// </summary>
        /// <remarks>
        /// Will be null until <see cref="EntityDataModelBuilder" />.BuildModel() has been called.
        /// </remarks>
        public static EntityDataModel Current { get; internal set; }

        /// <summary>
        /// Gets the Entity Sets defined in the Entity Data Model.
        /// </summary>
        public IReadOnlyDictionary<string, EntitySet> EntitySets { get; }

        /// <summary>
        /// Gets the filter functions provided by the service.
        /// </summary>
        public IReadOnlyCollection<string> FilterFunctions { get; }

        /// <summary>
        /// Gets the formats supported by the service.
        /// </summary>
        public IReadOnlyCollection<string> SupportedFormats { get; }
    }
}