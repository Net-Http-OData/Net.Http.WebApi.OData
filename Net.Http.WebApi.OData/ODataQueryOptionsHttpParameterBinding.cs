// -----------------------------------------------------------------------
// <copyright file="ODataQueryOptionsHttpParameterBinding.cs" company="Project Contributors">
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
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using Net.Http.OData;
using Net.Http.OData.Model;
using Net.Http.OData.Query;

namespace Net.Http.WebApi.OData
{
    /// <summary>
    /// The <see cref="HttpParameterBinding"/> which can create an <see cref="ODataQueryOptions"/> from the request parameters.
    /// </summary>
    internal sealed class ODataQueryOptionsHttpParameterBinding : HttpParameterBinding
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ODataQueryOptionsHttpParameterBinding"/> class.
        /// </summary>
        /// <param name="parameterDescriptor">The parameter descriptor.</param>
        internal ODataQueryOptionsHttpParameterBinding(HttpParameterDescriptor parameterDescriptor)
            : base(parameterDescriptor)
        {
        }

        /// <summary>
        /// Asynchronously executes the binding for the given request.
        /// </summary>
        /// <param name="metadataProvider">Metadata provider to use for validation.</param>
        /// <param name="actionContext">The action context for the binding. The action context contains the parameter dictionary that will get populated with the parameter.</param>
        /// <param name="cancellationToken">Cancellation token for cancelling the binding operation.</param>
        /// <returns>
        /// A task object representing the asynchronous operation.
        /// </returns>
        public override Task ExecuteBindingAsync(
            ModelMetadataProvider metadataProvider,
            HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            if (actionContext != null)
            {
                HttpRequestMessage request = actionContext.Request;

                string query = request.RequestUri.Query;
                EntitySet entitySet = request.ODataEntitySet();
                ODataRequestOptions odataRequestOptions = request.ODataRequestOptions();
                IODataQueryOptionsValidator validator = ODataQueryOptionsValidator.GetValidator(odataRequestOptions.ODataVersion);

                var queryOptions = new ODataQueryOptions(query, entitySet, validator);

                SetValue(actionContext, queryOptions);
            }

            return TaskEx.CompletedTask;
        }
    }
}
