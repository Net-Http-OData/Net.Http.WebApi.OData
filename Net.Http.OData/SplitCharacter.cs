// -----------------------------------------------------------------------
// <copyright file="SplitCharacter.cs" company="Project Contributors">
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
namespace Net.Http.OData
{
    /// <summary>
    /// Performance/Allocations: Contains character arrays for characters used in string.Split() calls to avoid the array being created each time.
    /// </summary>
    internal static class SplitCharacter
    {
        internal static char[] Ampersand { get; } = new[] { '&' };

        internal static char[] Comma { get; } = new[] { ',' };
    }
}