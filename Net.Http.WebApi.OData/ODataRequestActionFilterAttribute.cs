// -----------------------------------------------------------------------
// <copyright file="ODataRequestActionFilterAttribute.cs" company="Project Contributors">
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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Net.Http.OData;

namespace Net.Http.WebApi.OData
{
    /// <summary>
    /// An <see cref="ActionFilterAttribute"/> which parses the OData request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class ODataRequestActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs after the action method is invoked.
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext?.Request.IsODataRequest() == true)
            {
                ODataRequestOptions requestOptions = actionExecutedContext.Request.ReadODataRequestOptions();

                HttpResponseMessage response = actionExecutedContext.Response;

                response.Content.Headers.ContentType.Parameters.Add(requestOptions.MetadataLevel.ToNameValueHeaderValue());
                response.Headers.Add(ODataHeaderNames.ODataVersion, requestOptions.Version.ToString());
            }

            base.OnActionExecuted(actionExecutedContext);
        }

        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext?.Request.IsODataRequest() == true)
            {
                HttpRequestMessage request = actionContext.Request;

                var requestOptions = new ODataRequestOptions(
                    ODataUtility.ODataServiceRootUri(request.RequestUri.Scheme, request.RequestUri.Host, request.RequestUri.LocalPath),
                    ReadIsolationLevel(request),
                    ReadMetadataLevel(request),
                    ReadODataVersion(request));

                request.Properties.Add(typeof(ODataRequestOptions).FullName, requestOptions);
            }

            base.OnActionExecuting(actionContext);
        }

        private static string ReadHeaderValue(HttpRequestMessage request, string name)
            => request.Headers.TryGetValues(name, out IEnumerable<string> values) ? values.FirstOrDefault() : default;

        private static ODataIsolationLevel ReadIsolationLevel(HttpRequestMessage request)
        {
            string headerValue = ReadHeaderValue(request, ODataHeaderNames.ODataIsolation);

            if (headerValue != null)
            {
                switch (headerValue)
                {
                    case "Snapshot":
                        return ODataIsolationLevel.Snapshot;

                    default:
                        throw new ODataException(HttpStatusCode.BadRequest, $"If specified, the {ODataHeaderNames.ODataIsolation} must be 'Snapshot'");
                }
            }

            return ODataIsolationLevel.None;
        }

        private static ODataMetadataLevel ReadMetadataLevel(HttpRequestMessage request)
        {
            foreach (MediaTypeWithQualityHeaderValue header in request.Headers.Accept)
            {
                foreach (NameValueHeaderValue parameter in header.Parameters)
                {
                    if (parameter.Name == ODataMetadataLevelExtensions.HeaderName)
                    {
                        switch (parameter.Value)
                        {
                            case "none":
                                return ODataMetadataLevel.None;

                            case "minimal":
                                return ODataMetadataLevel.Minimal;

                            case "full":
                                return ODataMetadataLevel.Full;

                            default:
                                throw new ODataException(HttpStatusCode.BadRequest, $"If specified, the {ODataMetadataLevelExtensions.HeaderName} value in the Accept header must be 'none', 'minimal' or 'full'");
                        }
                    }
                }
            }

            return ODataMetadataLevel.Minimal;
        }

        private static ODataVersion ReadODataVersion(HttpRequestMessage request)
        {
            string headerValue = ReadHeaderValue(request, ODataHeaderNames.ODataVersion);

            if (headerValue != null)
            {
                if (ODataVersion.TryParse(headerValue, out ODataVersion odataVersion) && odataVersion >= ODataVersion.MinVersion && odataVersion <= ODataVersion.MaxVersion)
                {
                    return odataVersion;
                }
                else
                {
                    throw new ODataException(HttpStatusCode.BadRequest, $"If specified, the {ODataHeaderNames.ODataVersion} header must be a valid OData version such as 4.0");
                }
            }

            headerValue = ReadHeaderValue(request, ODataHeaderNames.ODataMaxVersion);

            if (headerValue != null)
            {
                if (ODataVersion.TryParse(headerValue, out ODataVersion odataVersion) && odataVersion >= ODataVersion.MinVersion && odataVersion <= ODataVersion.MaxVersion)
                {
                    return odataVersion;
                }
                else
                {
                    throw new ODataException(HttpStatusCode.BadRequest, $"If specified, the {ODataHeaderNames.ODataMaxVersion} header must be a valid OData version such as 4.0");
                }
            }

            return ODataVersion.MaxVersion;
        }
    }
}
