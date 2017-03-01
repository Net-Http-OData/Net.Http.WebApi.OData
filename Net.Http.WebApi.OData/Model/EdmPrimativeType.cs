// -----------------------------------------------------------------------
// <copyright file="EdmPrimativeType.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Model
{
    /// <summary>
    /// The primitive types in the entity data model.
    /// </summary>
    public enum EdmPrimativeType
    {
        /// <summary>
        /// Represents the absence of a value
        /// </summary>
        Null = 0,

        /// <summary>
        /// Represent fixed- or variable- length binary data
        /// </summary>
        Binary = 1,

        /// <summary>
        /// Represents the mathematical concept of binary-valued logic
        /// </summary>
        Boolean = 2,

        /// <summary>
        /// Unsigned 8-bit integer value
        /// </summary>
        Byte = 3,

        /// <summary>
        /// Represents a date with a value ranging from January 1, 1753 A.D. through December 9999 A.D.
        /// </summary>
        Date = 4,

        /// <summary>
        /// Represents numeric values with fixed precision and scale. This type can describe a numeric value ranging from negative 10^255 + 1 to positive 10^255 -1
        /// </summary>
        Decimal = 5,

        /// <summary>
        /// Represents a floating point number with 15 digits precision that can represent values with approximate range of ± 2.23e -308 through ± 1.79e +308
        /// </summary>
        Double = 6,

        /// <summary>
        /// Represents a floating point number with 7 digits precision that can represent values with approximate range of ± 1.18e -38 through ± 3.40e +38
        /// </summary>
        Single = 7,

        /// <summary>
        /// Represents a 16-byte (128-bit) unique identifier value
        /// </summary>
        Guid = 8,

        /// <summary>
        /// Represents a signed 16-bit integer value
        /// </summary>
        Int16 = 9,

        /// <summary>
        /// Represents a signed 32-bit integer value
        /// </summary>
        Int32 = 10,

        /// <summary>
        /// Represents a signed 64-bit integer value
        /// </summary>
        Int64 = 11,

        /// <summary>
        /// Represents a signed 8-bit integer value
        /// </summary>
        SByte = 12,

        /// <summary>
        /// Represents fixed- or variable-length character data
        /// </summary>
        String = 13,

        /// <summary>
        /// Represents the time of day with values ranging from 0:00:00.x to 23:59:59.y, where x and y depend upon the precision
        /// </summary>
        TimeOfDay = 14,

        /// <summary>
        /// Represents date and time as an Offset in minutes from GMT, with values ranging from 12:00:00 midnight, January 1, 1753 A.D. through 11:59:59 P.M, December 9999 A.D
        /// </summary>
        DateTimeOffset = 15
    }
}