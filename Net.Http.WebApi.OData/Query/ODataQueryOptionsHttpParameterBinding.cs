// -----------------------------------------------------------------------
// <copyright file="ODataQueryOptionsHttpParameterBinding.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Query
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Metadata;

    /// <summary>
    /// The <see cref="HttpParameterBinding"/> which can create an <see cref="ODataQueryOptions"/> from the request parameters.
    /// </summary>
    internal sealed class ODataQueryOptionsHttpParameterBinding : HttpParameterBinding
    {
        private static readonly Task CompletedTask = Task.FromResult(0);

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "This method is only called by the ASP.NET runtime.")]
        public override Task ExecuteBindingAsync(
            ModelMetadataProvider metadataProvider,
            HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            var request = actionContext.Request;
            var entitySet = request.ResolveEntitySet();

            var queryOptions = new ODataQueryOptions(request, entitySet);

            this.SetValue(actionContext, queryOptions);

            return CompletedTask;
        }
    }
}