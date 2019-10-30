// -----------------------------------------------------------------------
// <copyright file="SkipQueryOptionValidator.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query.Validators
{
    using System.Net;

    /// <summary>
    /// A class which validates the $skip query option based upon the <see cref="ODataValidationSettings"/>.
    /// </summary>
    internal static class SkipQueryOptionValidator
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <exception cref="ODataException">Thrown if the validation fails.</exception>
        internal static void Validate(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (queryOptions.RawValues.Skip is null)
            {
                return;
            }

            if ((validationSettings.AllowedQueryOptions & AllowedQueryOptions.Skip) != AllowedQueryOptions.Skip)
            {
                throw new ODataException(HttpStatusCode.NotImplemented, Messages.UnsupportedQueryOption.FormatWith("$skip"));
            }

            if (queryOptions.Skip.Value < 0)
            {
                throw new ODataException(HttpStatusCode.BadRequest, Messages.IntRawValueInvalid.FormatWith("$skip"));
            }
        }
    }
}