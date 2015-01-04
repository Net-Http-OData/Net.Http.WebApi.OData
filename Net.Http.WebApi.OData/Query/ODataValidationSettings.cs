// -----------------------------------------------------------------------
// <copyright file="ODataValidationSettings.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query
{
    using System;

    /// <summary>
    /// A class which defines the validation settings to use when validating values in <see cref="ODataQueryOptions"/>.
    /// </summary>
    public sealed class ODataValidationSettings : IEquatable<ODataValidationSettings>
    {
        /// <summary>
        /// Gets the validation settings for when all OData queries are allowed.
        /// </summary>
        public static ODataValidationSettings All
        {
            get
            {
                return new ODataValidationSettings
                {
                    AllowedArithmeticOperators = AllowedArithmeticOperators.All,
                    AllowedFunctions = AllowedFunctions.AllFunctions,
                    AllowedLogicalOperators = AllowedLogicalOperators.All,
                    AllowedQueryOptions = AllowedQueryOptions.All,
                    MaxTop = 100
                };
            }
        }

        /// <summary>
        /// Gets the validation settings for when no OData queries are allowed.
        /// </summary>
        public static ODataValidationSettings None
        {
            get
            {
                return new ODataValidationSettings
                {
                    AllowedArithmeticOperators = AllowedArithmeticOperators.None,
                    AllowedFunctions = AllowedFunctions.None,
                    AllowedLogicalOperators = AllowedLogicalOperators.None,
                    AllowedQueryOptions = AllowedQueryOptions.None,
                    MaxTop = 0
                };
            }
        }

        /// <summary>
        /// Gets or sets the allowed arithmetic operators.
        /// </summary>
        public AllowedArithmeticOperators AllowedArithmeticOperators
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the allowed functions.
        /// </summary>
        public AllowedFunctions AllowedFunctions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the allowed logical operators.
        /// </summary>
        public AllowedLogicalOperators AllowedLogicalOperators
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the allowed query options.
        /// </summary>
        public AllowedQueryOptions AllowedQueryOptions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the max value allowed in the $top query option.
        /// </summary>
        /// <remarks>
        /// This is used to ensure that 'paged' queries do not return excessive results on each call.
        /// </remarks>
        public int MaxTop
        {
            get;
            set;
        }

        /// <summary>
        /// Checks whether two separate ODataValidationSettings instances are not equal.
        /// </summary>
        /// <param name="settings1">The validation settings to check.</param>
        /// <param name="settings2">The validation settings to check against.</param>
        /// <returns><c>true</c> if the instances are not considered equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ODataValidationSettings settings1, ODataValidationSettings settings2)
        {
            if (settings1 != null && settings2 != null)
            {
                return !settings1.Equals(settings2);
            }

            return true;
        }

        /// <summary>
        /// Checks whether two separate ODataValidationSettings instances are equal.
        /// </summary>
        /// <param name="settings1">The validation settings to check.</param>
        /// <param name="settings2">The validation settings to check against.</param>
        /// <returns><c>true</c> if the instances are considered equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ODataValidationSettings settings1, ODataValidationSettings settings2)
        {
            if (settings1 != null && settings2 != null)
            {
                return settings1.Equals(settings2);
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var other = obj as ODataValidationSettings;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ODataValidationSettings" /> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="ODataValidationSettings"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="ODataValidationSettings" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(ODataValidationSettings other)
        {
            if (other == null)
            {
                return false;
            }

            return other.AllowedArithmeticOperators == this.AllowedArithmeticOperators
                && other.AllowedFunctions == this.AllowedFunctions
                && other.AllowedLogicalOperators == this.AllowedLogicalOperators
                && other.AllowedQueryOptions == this.AllowedQueryOptions
                && other.MaxTop == this.MaxTop;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return this.AllowedArithmeticOperators.GetHashCode()
                ^ this.AllowedFunctions.GetHashCode()
                ^ this.AllowedLogicalOperators.GetHashCode()
                ^ this.AllowedQueryOptions.GetHashCode()
                ^ this.MaxTop.GetHashCode();
        }
    }
}