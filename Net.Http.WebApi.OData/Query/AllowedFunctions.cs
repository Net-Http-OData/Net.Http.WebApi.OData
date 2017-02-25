// -----------------------------------------------------------------------
// <copyright file="AllowedFunctions.cs" company="Project Contributors">
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

    /// <summary>
    /// An enumeration which represents the functions allowed in the $filter query option of an OData query.
    /// </summary>
    [Flags]
    public enum AllowedFunctions
    {
        /// <summary>
        /// Specifies that no functions are allowed in the $filter query option.
        /// </summary>
        None = 0,

        /// <summary>
        /// Specifies that the 'StartsWith' function is allowed in the $filter query option.
        /// </summary>
        StartsWith = 1,

        /// <summary>
        /// Specifies that the 'EndsWith' function is allowed in the $filter query option.
        /// </summary>
        EndsWith = 2,

        /// <summary>
        /// Specifies that the 'Contains' function is allowed in the $filter query option.
        /// </summary>
        Contains = 4,

        /// <summary>
        /// Specifies that the 'ToLower' function is allowed in the $filter query option.
        /// </summary>
        ToLower = 8,

        /// <summary>
        /// Specifies that the 'ToUpper' function is allowed in the $filter query option.
        /// </summary>
        ToUpper = 16,

        /// <summary>
        /// Specifies that the 'Year' function is allowed in the $filter query option.
        /// </summary>
        Year = 32,

        /// <summary>
        /// Specifies that the 'Month' function is allowed in the $filter query option.
        /// </summary>
        Month = 64,

        /// <summary>
        /// Specifies that the 'Day' function is allowed in the $filter query option.
        /// </summary>
        Day = 128,

        /// <summary>
        /// Specifies that the 'Trim' function is allowed in the $filter query option.
        /// </summary>
        Trim = 256,

        /// <summary>
        /// Specifies that the 'Length' function is allowed in the $filter query option.
        /// </summary>
        Length = 512,

        /// <summary>
        /// Specifies that the 'Round' function is allowed in the $filter query option.
        /// </summary>
        Round = 1024,

        /// <summary>
        /// Specifies that the 'Ceiling' function is allowed in the $filter query option.
        /// </summary>
        Ceiling = 2048,

        /// <summary>
        /// Specifies that the 'Floor' function is allowed in the $filter query option.
        /// </summary>
        Floor = 4096,

        /// <summary>
        /// Specifies that the 'Hour' function is allowed in the $filter query option.
        /// </summary>
        Hour = 8192,

        /// <summary>
        /// Specifies that the 'Minute' function is allowed in the $filter query option.
        /// </summary>
        Minute = 16384,

        /// <summary>
        /// Specifies that the 'Second' function is allowed in the $filter query option.
        /// </summary>
        Second = 32768,

        /// <summary>
        /// Specifies that the 'IndexOf' function is allowed in the $filter query option.
        /// </summary>
        IndexOf = 65536,

        /// <summary>
        /// Specifies that the 'Replace' function is allowed in the $filter query option.
        /// </summary>
        Replace = 131072,

        /// <summary>
        /// Specifies that the 'Substring' function is allowed in the $filter query option.
        /// </summary>
        Substring = 262144,

        /// <summary>
        /// Specifies that the 'Concat' function is allowed in the $filter query option.
        /// </summary>
        Concat = 524288,

        /// <summary>
        /// Specifies that the 'IsOf' function is allowed in the $filter query option.
        /// </summary>
        IsOf = 1048576,

        /// <summary>
        /// Specifies that the 'Cast' function is allowed in the $filter query option.
        /// </summary>
        Cast = 2097152,

        /// <summary>
        /// Specifies that the 'FractionalSeconds' function is allowed in the $filter query option.
        /// </summary>
        FractionalSeconds = 4194304,

        /// <summary>
        /// Specifies that all string functions are allowed in the $filter query option.
        /// </summary>
        AllStringFunctions = StartsWith | EndsWith | Contains | Length | IndexOf | Substring | ToLower | ToUpper | Trim | Replace | Concat,

        /// <summary>
        /// Specifies that all date/time functions are allowed in the $filter query option.
        /// </summary>
        AllDateTimeFunctions = Year | Month | Day | Hour | Minute | Second | FractionalSeconds,

        /// <summary>
        /// Specifies that all math functions are allowed in the $filter query option.
        /// </summary>
        AllMathFunctions = Round | Floor | Ceiling,

        /// <summary>
        /// Specifies that all functions are allowed in the $filter query option.
        /// </summary>
        AllFunctions = AllStringFunctions | AllDateTimeFunctions | AllMathFunctions | IsOf | Cast
    }
}