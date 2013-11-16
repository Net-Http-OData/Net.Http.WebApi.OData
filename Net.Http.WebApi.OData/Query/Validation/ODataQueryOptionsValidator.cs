// -----------------------------------------------------------------------
// <copyright file="ODataQueryOptionsValidator.cs" company="Project Contributors">
// Copyright 2012-2013 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData.Query.Validation
{
    /// <summary>
    /// A class which validates ODataQueryOptions based upon the ODataValidationSettings.
    /// </summary>
    internal static class ODataQueryOptionsValidator
    {
        /// <summary>
        /// Validates the specified query options.
        /// </summary>
        /// <param name="queryOptions">The query options.</param>
        /// <param name="validationSettings">The validation settings.</param>
        /// <exception cref="ODataException">Thrown if the validation fails.</exception>
        internal static void Validate(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (queryOptions.Filter != null)
            {
                if ((validationSettings.AllowedQueryOptions & AllowedQueryOptions.Filter) != AllowedQueryOptions.Filter)
                {
                    throw new ODataException(Messages.FilterQueryOptionNotSupported);
                }

                ValidateStringFunctions(queryOptions.Filter.RawValue, validationSettings);
                ValidateDateFunctions(queryOptions.Filter.RawValue, validationSettings);
                ValidateNumberFunctions(queryOptions.Filter.RawValue, validationSettings);
            }

            if (queryOptions.Format != null && (validationSettings.AllowedQueryOptions & AllowedQueryOptions.Format) != AllowedQueryOptions.Format)
            {
                throw new ODataException(Messages.FormatQueryOptionNotSupported);
            }

            if (queryOptions.InlineCount != null && (validationSettings.AllowedQueryOptions & AllowedQueryOptions.InlineCount) != AllowedQueryOptions.InlineCount)
            {
                throw new ODataException(Messages.InlineCountQueryOptionNotSupported);
            }

            if (queryOptions.OrderBy != null && (validationSettings.AllowedQueryOptions & AllowedQueryOptions.OrderBy) != AllowedQueryOptions.OrderBy)
            {
                throw new ODataException(Messages.OrderByQueryOptionNotSupported);
            }

            if (queryOptions.Select != null && (validationSettings.AllowedQueryOptions & AllowedQueryOptions.Select) != AllowedQueryOptions.Select)
            {
                throw new ODataException(Messages.SelectQueryOptionNotSupported);
            }

            if (queryOptions.Skip != null && (validationSettings.AllowedQueryOptions & AllowedQueryOptions.Skip) != AllowedQueryOptions.Skip)
            {
                throw new ODataException(Messages.SkipQueryOptionNotSupported);
            }

            if (queryOptions.Top != null && (validationSettings.AllowedQueryOptions & AllowedQueryOptions.Top) != AllowedQueryOptions.Top)
            {
                throw new ODataException(Messages.TopQueryOptionNotSupported);
            }
        }

        private static void ValidateDateFunctions(string rawFilterValue, ODataValidationSettings validationSettings)
        {
            if ((validationSettings.AllowedFunctions & AllowedFunctions.Year) != AllowedFunctions.Year
                && rawFilterValue.Contains("year("))
            {
                throw new ODataException(Messages.YearFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Month) != AllowedFunctions.Month
                && rawFilterValue.Contains("month("))
            {
                throw new ODataException(Messages.MonthFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Day) != AllowedFunctions.Day
                && rawFilterValue.Contains("day("))
            {
                throw new ODataException(Messages.DayFunctionNotSupported);
            }
        }

        private static void ValidateNumberFunctions(string rawFilterValue, ODataValidationSettings validationSettings)
        {
            if ((validationSettings.AllowedFunctions & AllowedFunctions.Round) != AllowedFunctions.Round
                && rawFilterValue.Contains("round("))
            {
                throw new ODataException(Messages.RoundFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Ceiling) != AllowedFunctions.Ceiling
                && rawFilterValue.Contains("ceiling("))
            {
                throw new ODataException(Messages.CeilingFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Floor) != AllowedFunctions.Floor
                && rawFilterValue.Contains("floor("))
            {
                throw new ODataException(Messages.FloorFunctionNotSupported);
            }
        }

        private static void ValidateStringFunctions(string rawFilterValue, ODataValidationSettings validationSettings)
        {
            if ((validationSettings.AllowedFunctions & AllowedFunctions.EndsWith) != AllowedFunctions.EndsWith
                && rawFilterValue.Contains("endswith("))
            {
                throw new ODataException(Messages.EndsWithFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.StartsWith) != AllowedFunctions.StartsWith
                && rawFilterValue.Contains("startswith("))
            {
                throw new ODataException(Messages.StartsWithFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.SubstringOf) != AllowedFunctions.SubstringOf
                && rawFilterValue.Contains("substringof("))
            {
                throw new ODataException(Messages.SubstringOfFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.ToLower) != AllowedFunctions.ToLower
                && rawFilterValue.Contains("tolower("))
            {
                throw new ODataException(Messages.ToLowerFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.ToUpper) != AllowedFunctions.ToUpper
                && rawFilterValue.Contains("toupper("))
            {
                throw new ODataException(Messages.ToUpperFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Trim) != AllowedFunctions.Trim
                && rawFilterValue.Contains("trim("))
            {
                throw new ODataException(Messages.TrimFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Length) != AllowedFunctions.Length
                && rawFilterValue.Contains("length("))
            {
                throw new ODataException(Messages.LengthFunctionNotSupported);
            }
        }
    }
}