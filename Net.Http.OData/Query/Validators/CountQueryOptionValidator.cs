// -----------------------------------------------------------------------
// <copyright file="CountQueryOptionValidator.cs" company="Project Contributors">
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
    /// A class which validates the $count query option based upon the <see cref="ODataValidationSettings"/>.
    /// </summary>
    internal static class CountQueryOptionValidator
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <exception cref="ODataException">Thrown if the validation fails.</exception>
        internal static void Validate(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (queryOptions.RawValues.Count is null)
            {
                return;
            }

            if ((validationSettings.AllowedQueryOptions & AllowedQueryOptions.Count) != AllowedQueryOptions.Count)
            {
                throw new ODataException(HttpStatusCode.NotImplemented, "The query option $count is not implemented by this service");
            }

            if (queryOptions.RawValues.Count != "$count=true"
                && queryOptions.RawValues.Count != "$count=false")
            {
                throw new ODataException(HttpStatusCode.BadRequest, "The supplied value for OData query $count is invalid, valid options are 'true' and 'false'");
            }
        }
    }
}