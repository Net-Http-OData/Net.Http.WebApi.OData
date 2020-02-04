// -----------------------------------------------------------------------
// <copyright file="TaskEx.cs" company="Project Contributors">
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
using System.Threading.Tasks;

namespace Net.Http.WebApi.OData
{
    /// <summary>
    /// Extensions to <see cref="Task"/> which are not present in .NET 4.5.
    /// </summary>
    internal static class TaskEx
    {
        /// <summary>
        /// Gets a task that has already completed successfully.
        /// </summary>
        /// <remarks>Equivalent of Task.CompletedTask.</remarks>
        internal static Task CompletedTask { get; } = Task.FromResult(0);
    }
}
