// -----------------------------------------------------------------------
// <copyright file="CountQueryOptionValidator.cs" company="Project Contributors">
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
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

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
        /// <exception cref="HttpResponseException">Thrown if the validation fails.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "We're throwing an exception with the HttpResponseMessage")]
        internal static void Validate(ODataQueryOptions queryOptions, ODataValidationSettings validationSettings)
        {
            if (queryOptions.RawValues.Count == null)
            {
                return;
            }

            if ((validationSettings.AllowedQueryOptions & AllowedQueryOptions.Count) != AllowedQueryOptions.Count)
            {
                throw new HttpResponseException(
                    queryOptions.Request.CreateErrorResponse(HttpStatusCode.NotImplemented, Messages.UnsupportedQueryOption.FormatWith("$count")));
            }

            if (queryOptions.RawValues.Count != "$count=true"
                && queryOptions.RawValues.Count != "$count=false")
            {
                throw new HttpResponseException(
                    queryOptions.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Messages.CountRawValueInvalid));
            }
        }
    }
}