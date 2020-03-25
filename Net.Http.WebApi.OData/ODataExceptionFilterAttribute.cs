// -----------------------------------------------------------------------
// <copyright file="ODataExceptionFilterAttribute.cs" company="Project Contributors">
// Copyright Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Web.Http.Filters;
using Net.Http.OData;

namespace Net.Http.WebApi.OData
{
    /// <summary>
    /// An <see cref="ExceptionFilterAttribute"/> which returns the correct response for an <see cref="ODataException"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class ODataExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext?.Exception is ODataException odataException)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateODataErrorResponse(odataException);
            }

            base.OnException(actionExecutedContext);
        }
    }
}
