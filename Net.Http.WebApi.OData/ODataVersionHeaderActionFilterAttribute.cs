// -----------------------------------------------------------------------
// <copyright file="ODataVersionHeaderActionFilterAttribute.cs" company="Project Contributors">
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
using System;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Net.Http.OData;

namespace Net.Http.WebApi.OData
{
    /// <summary>
    /// An <see cref="ActionFilterAttribute"/> which validates the OData-Version header in a request.
    /// </summary>
    /// <seealso cref="System.Web.Http.Filters.ActionFilterAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ODataVersionHeaderActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs after the action method is invoked.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext?.Request.IsODataUri() == true)
            {
                actionExecutedContext.Response.Headers.Add(ODataHeaderNames.ODataVersion, ODataHeaderValues.ODataVersionString);
            }

            base.OnActionExecuted(actionExecutedContext);
        }

        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext != null)
            {
                string headerValue = actionContext.Request.ReadHeaderValue(ODataHeaderNames.ODataVersion);

                if (headerValue != null && headerValue != ODataHeaderValues.ODataVersionString)
                {
                    actionContext.Response =
                        actionContext.Request.CreateODataErrorResponse(HttpStatusCode.NotAcceptable, "This service only supports OData 4.0");
                }

                headerValue = actionContext.Request.ReadHeaderValue(ODataHeaderNames.ODataMaxVersion);

                if (headerValue != null && headerValue != ODataHeaderValues.ODataVersionString)
                {
                    actionContext.Response =
                        actionContext.Request.CreateODataErrorResponse(HttpStatusCode.NotAcceptable, "This service only supports OData 4.0");
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
