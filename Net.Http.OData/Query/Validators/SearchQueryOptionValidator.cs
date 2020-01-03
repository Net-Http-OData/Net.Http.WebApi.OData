﻿// -----------------------------------------------------------------------
// <copyright file="SearchQueryOptionValidator.cs" company="Project Contributors">
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
namespace Net.Http.OData.Query.Validators
{
    using System.Net;

    /// <summary>
    /// A class which validates the $search query option based upon the <see cref="ODataValidationSettings"/>.
    /// </summary>
    internal static class SearchQueryOptionValidator
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <exception cref="ODataException">Thrown if the validation fails.</exception>
        internal static void Validate(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (queryOptions.RawValues.Search is null)
            {
                return;
            }

            if ((validationSettings.AllowedQueryOptions & AllowedQueryOptions.Search) != AllowedQueryOptions.Search)
            {
                throw new ODataException(HttpStatusCode.NotImplemented, "The query option $search is not implemented by this service");
            }
        }
    }
}