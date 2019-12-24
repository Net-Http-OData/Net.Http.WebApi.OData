// -----------------------------------------------------------------------
// <copyright file="ODataVersionHeaderValidationAttribute.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData
{
    using System;
    using System.Net;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    /// <summary>
    /// An <see cref="ActionFilterAttribute"/> which validates the OData-Version header in a request.
    /// </summary>
    /// <seealso cref="System.Web.Http.Filters.ActionFilterAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ODataVersionHeaderValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext != null)
            {
                var headerValue = actionContext.Request.ReadHeaderValue(ODataHeaderNames.ODataVersion);

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