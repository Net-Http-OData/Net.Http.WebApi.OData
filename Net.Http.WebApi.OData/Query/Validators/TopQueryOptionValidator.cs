// -----------------------------------------------------------------------
// <copyright file="TopQueryOptionValidator.cs" company="Project Contributors">
// Copyright 2012 - 2017 Project Contributors
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
    using System.Globalization;

    /// <summary>
    /// A class which validates the $top query option based upon the <see cref="ODataValidationSettings"/>.
    /// </summary>
    internal static class TopQueryOptionValidator
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <exception cref="ODataException">Thrown if the validation fails.</exception>
        internal static void Validate(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (queryOptions.RawValues.Top == null)
            {
                return;
            }

            if ((validationSettings.AllowedQueryOptions & AllowedQueryOptions.Top) != AllowedQueryOptions.Top)
            {
                throw new ODataException(Messages.UnsupportedQueryOption.FormatWith("$top"));
            }

            if (queryOptions.Top.Value < 0)
            {
                throw new ODataException(Messages.TopRawValueInvalid);
            }

            if (queryOptions.Top.Value > validationSettings.MaxTop)
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    Messages.TopValueExceedsMaxAllowed,
                    validationSettings.MaxTop.ToString(CultureInfo.InvariantCulture));

                throw new ODataException(message);
            }
        }
    }
}