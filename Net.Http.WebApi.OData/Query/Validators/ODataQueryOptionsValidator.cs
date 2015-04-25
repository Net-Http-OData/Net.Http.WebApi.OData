// -----------------------------------------------------------------------
// <copyright file="ODataQueryOptionsValidator.cs" company="Project Contributors">
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
                ValidateMathFunctions(queryOptions.Filter.RawValue, validationSettings);
                ValidateLogicalOperators(queryOptions.Filter.RawValue, validationSettings);
                ValidateArithmeticOperators(queryOptions.Filter.RawValue, validationSettings);
                ValidateFunctions(queryOptions.Filter.RawValue, validationSettings);
            }

            if (queryOptions.Format != null
                && (validationSettings.AllowedQueryOptions & AllowedQueryOptions.Format) != AllowedQueryOptions.Format)
            {
                throw new ODataException(Messages.FormatQueryOptionNotSupported);
            }

            if (queryOptions.InlineCount != null
                && (validationSettings.AllowedQueryOptions & AllowedQueryOptions.InlineCount) != AllowedQueryOptions.InlineCount)
            {
                throw new ODataException(Messages.InlineCountQueryOptionNotSupported);
            }

            if (queryOptions.OrderBy != null
                && (validationSettings.AllowedQueryOptions & AllowedQueryOptions.OrderBy) != AllowedQueryOptions.OrderBy)
            {
                throw new ODataException(Messages.OrderByQueryOptionNotSupported);
            }

            if (queryOptions.Select != null
                && (validationSettings.AllowedQueryOptions & AllowedQueryOptions.Select) != AllowedQueryOptions.Select)
            {
                throw new ODataException(Messages.SelectQueryOptionNotSupported);
            }

            if (queryOptions.Skip != null
                && (validationSettings.AllowedQueryOptions & AllowedQueryOptions.Skip) != AllowedQueryOptions.Skip)
            {
                throw new ODataException(Messages.SkipQueryOptionNotSupported);
            }

            if (queryOptions.Top != null
                && (validationSettings.AllowedQueryOptions & AllowedQueryOptions.Top) != AllowedQueryOptions.Top)
            {
                throw new ODataException(Messages.TopQueryOptionNotSupported);
            }
        }

        private static void ValidateFunctions(string rawFilterValue, ODataValidationSettings validationSettings)
        {
            if (validationSettings.AllowedFunctions == AllowedFunctions.AllFunctions)
            {
                return;
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.IsOf) != AllowedFunctions.IsOf
                && rawFilterValue.Contains("isof("))
            {
                throw new ODataException(Messages.IsOfFunctionNotSupported);
            }
        }

        private static void ValidateArithmeticOperators(string rawFilterValue, ODataValidationSettings validationSettings)
        {
            if (validationSettings.AllowedArithmeticOperators == AllowedArithmeticOperators.All)
            {
                return;
            }

            if ((validationSettings.AllowedArithmeticOperators & AllowedArithmeticOperators.Add) != AllowedArithmeticOperators.Add
                && rawFilterValue.Contains(" add "))
            {
                throw new ODataException(Messages.AddOperatorNotSupported);
            }

            if ((validationSettings.AllowedArithmeticOperators & AllowedArithmeticOperators.Divide) != AllowedArithmeticOperators.Divide
                && rawFilterValue.Contains(" div "))
            {
                throw new ODataException(Messages.DivideOperatorNotSupported);
            }

            if ((validationSettings.AllowedArithmeticOperators & AllowedArithmeticOperators.Modulo) != AllowedArithmeticOperators.Modulo
                && rawFilterValue.Contains(" mod "))
            {
                throw new ODataException(Messages.ModuloOperatorNotSupported);
            }

            if ((validationSettings.AllowedArithmeticOperators & AllowedArithmeticOperators.Multiply) != AllowedArithmeticOperators.Multiply
                && rawFilterValue.Contains(" mul "))
            {
                throw new ODataException(Messages.MultiplyOperatorNotSupported);
            }

            if ((validationSettings.AllowedArithmeticOperators & AllowedArithmeticOperators.Subtract) != AllowedArithmeticOperators.Subtract
                && rawFilterValue.Contains(" sub "))
            {
                throw new ODataException(Messages.SubtractOperatorNotSupported);
            }
        }

        private static void ValidateDateFunctions(string rawFilterValue, ODataValidationSettings validationSettings)
        {
            if (validationSettings.AllowedFunctions == AllowedFunctions.AllFunctions
                || validationSettings.AllowedFunctions == AllowedFunctions.AllDateTimeFunctions)
            {
                return;
            }

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

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Hour) != AllowedFunctions.Hour
                && rawFilterValue.Contains("hour("))
            {
                throw new ODataException(Messages.HourFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Minute) != AllowedFunctions.Minute
                && rawFilterValue.Contains("minute("))
            {
                throw new ODataException(Messages.MinuteFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Second) != AllowedFunctions.Second
                && rawFilterValue.Contains("second("))
            {
                throw new ODataException(Messages.SecondFunctionNotSupported);
            }
        }

        private static void ValidateLogicalOperators(string rawFilterValue, ODataValidationSettings validationSettings)
        {
            if (validationSettings.AllowedLogicalOperators == AllowedLogicalOperators.All)
            {
                return;
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.And) != AllowedLogicalOperators.And
                && rawFilterValue.Contains(" and "))
            {
                throw new ODataException(Messages.AndOperatorNotSupported);
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.Or) != AllowedLogicalOperators.Or
                && rawFilterValue.Contains(" or "))
            {
                throw new ODataException(Messages.OrOperatorNotSupported);
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.Equal) != AllowedLogicalOperators.Equal
                && rawFilterValue.Contains(" eq "))
            {
                throw new ODataException(Messages.EqualsOperatorNotSupported);
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.NotEqual) != AllowedLogicalOperators.NotEqual
                && rawFilterValue.Contains(" ne "))
            {
                throw new ODataException(Messages.NotEqualsOperatorNotSupported);
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.GreaterThan) != AllowedLogicalOperators.GreaterThan
                && rawFilterValue.Contains(" gt "))
            {
                throw new ODataException(Messages.GreaterThanOperatorNotSupported);
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.GreaterThanOrEqual) != AllowedLogicalOperators.GreaterThanOrEqual
                && rawFilterValue.Contains(" ge "))
            {
                throw new ODataException(Messages.GreaterThanOrEqualsOperatorNotSupported);
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.LessThan) != AllowedLogicalOperators.LessThan
                && rawFilterValue.Contains(" lt "))
            {
                throw new ODataException(Messages.LessThanOperatorNotSupported);
            }

            if ((validationSettings.AllowedLogicalOperators & AllowedLogicalOperators.LessThanOrEqual) != AllowedLogicalOperators.LessThanOrEqual
                && rawFilterValue.Contains(" le "))
            {
                throw new ODataException(Messages.LessThanOrEqualsOperatorNotSupported);
            }
        }

        private static void ValidateMathFunctions(string rawFilterValue, ODataValidationSettings validationSettings)
        {
            if (validationSettings.AllowedFunctions == AllowedFunctions.AllFunctions
                || validationSettings.AllowedFunctions == AllowedFunctions.AllMathFunctions)
            {
                return;
            }

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
            if (validationSettings.AllowedFunctions == AllowedFunctions.AllFunctions
                || validationSettings.AllowedFunctions == AllowedFunctions.AllStringFunctions)
            {
                return;
            }

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

            if ((validationSettings.AllowedFunctions & AllowedFunctions.IndexOf) != AllowedFunctions.IndexOf
                && rawFilterValue.Contains("indexof("))
            {
                throw new ODataException(Messages.IndexOfFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Replace) != AllowedFunctions.Replace
                && rawFilterValue.Contains("replace("))
            {
                throw new ODataException(Messages.ReplaceFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Substring) != AllowedFunctions.Substring
                && rawFilterValue.Contains("substring("))
            {
                throw new ODataException(Messages.SubstringFunctionNotSupported);
            }

            if ((validationSettings.AllowedFunctions & AllowedFunctions.Concat) != AllowedFunctions.Concat
                && rawFilterValue.Contains("concat("))
            {
                throw new ODataException(Messages.ConcatFunctionNotSupported);
            }
        }
    }
}