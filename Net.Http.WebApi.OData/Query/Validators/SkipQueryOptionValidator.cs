// -----------------------------------------------------------------------
// <copyright file="SkipQueryOptionValidator.cs" company="Project Contributors">
// Copyright 2012 - 2015 Project Contributors
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
    /// <summary>
    /// A class which validates the $skip query option based upon the ODataValidationSettings.
    /// </summary>
    internal static class SkipQueryOptionValidator
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <exception cref="ODataException">Thrown if the validation fails.</exception>
        internal static void Validate(ODataQueryOptions queryOptions)
        {
            if (queryOptions.Skip != null)
            {
                if (queryOptions.Skip.Value < 0)
                {
                    throw new ODataException(Messages.SkipRawValueInvalid);
                }
            }
        }
    }
}