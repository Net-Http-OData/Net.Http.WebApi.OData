﻿// -----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Project Contributors">
// Copyright 2012 - 2018 Project Contributors
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
    using System.Globalization;

    internal static class StringExtensions
    {
        internal static string FormatWith(this string value, string arg0)
            => string.Format(CultureInfo.InvariantCulture, value, arg0);
    }
}