// -----------------------------------------------------------------------
// <copyright file="ODataQueryOptionsParameterBindingAttribute.cs" company="Project Contributors">
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
    using System;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    /// <summary>
    /// A <see cref="ParameterBindingAttribute"/> which returns the <see cref="HttpParameterBinding"/> which can
    /// create an <see cref="ODataQueryOptions"/> from the request parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    public sealed class ODataQueryOptionsParameterBindingAttribute : ParameterBindingAttribute
    {
        /// <summary>
        /// Gets the parameter binding.
        /// </summary>
        /// <param name="parameter">The parameter description.</param>
        /// <returns>
        /// The parameter binding.
        /// </returns>
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            return new ODataQueryOptionsHttpParameterBinding(parameter);
        }
    }
}