﻿// -----------------------------------------------------------------------
// <copyright file="EdmPrimitiveType.cs" company="Project Contributors">
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
namespace Net.Http.OData.Model
{
    using System;
    using System.IO;

    /// <summary>
    /// Represents a primitive type in the Entity Data Model.
    /// </summary>
    /// <seealso cref="EdmType" />
    [System.Diagnostics.DebuggerDisplay("{FullName}: {ClrType}")]
    public sealed class EdmPrimitiveType : EdmType
    {
        private EdmPrimitiveType(string name, string fullName, Type clrType)
            : base(name, fullName, clrType)
        {
        }

        /// <summary>
        /// Gets the EdmType which represent fixed- or variable- length binary data.
        /// </summary>
        public static EdmType Binary { get; } = new EdmPrimitiveType("Binary", "Edm.Binary", typeof(byte[]));

        /// <summary>
        /// Gets the EdmType which represents the mathematical concept of binary-valued logic.
        /// </summary>
        public static EdmType Boolean { get; } = new EdmPrimitiveType("Boolean", "Edm.Boolean", typeof(bool));

        /// <summary>
        /// Gets the EdmType which represents an unsigned 8-bit integer value.
        /// </summary>
        public static EdmType Byte { get; } = new EdmPrimitiveType("Byte", "Edm.Byte", typeof(byte));

        /// <summary>
        /// Gets the EdmType which represents date with values ranging from January 1; 1753 A.D. through December 9999 A.D.
        /// </summary>
        public static EdmType Date { get; } = new EdmPrimitiveType("Date", "Edm.Date", typeof(DateTime));

        /// <summary>
        /// Gets the EdmType which represents date and time as an Offset in minutes from GMT; with values ranging from 12:00:00 midnight; January 1; 1753 A.D. through 11:59:59 P.M; December 9999 A.D.
        /// </summary>
        public static EdmType DateTimeOffset { get; } = new EdmPrimitiveType("DateTimeOffset", "Edm.DateTimeOffset", typeof(DateTimeOffset));

#pragma warning disable CA1720 // Identifier contains type name

        /// <summary>
        /// Gets the EdmType which represents numeric values with fixed precision and scale. This type can describe a numeric value ranging from negative 10^255 + 1 to positive 10^255 -1.
        /// </summary>
        public static EdmType Decimal { get; } = new EdmPrimitiveType("Decimal", "Edm.Decimal", typeof(decimal));

#pragma warning restore CA1720 // Identifier contains type name

#pragma warning disable CA1720 // Identifier contains type name

        /// <summary>
        /// Gets the EdmType which represents a floating point number with 15 digits precision that can represent values with approximate range of ± 2.23e -308 through ± 1.79e +308.
        /// </summary>
        public static EdmType Double { get; } = new EdmPrimitiveType("Double", "Edm.Double", typeof(double));

#pragma warning restore CA1720 // Identifier contains type name

        /// <summary>
        /// Gets the EdmType which represents a duration.
        /// </summary>
        public static EdmType Duration { get; } = new EdmPrimitiveType("Duration", "Edm.Duration", typeof(TimeSpan));

#pragma warning disable CA1720 // Identifier contains type name

        /// <summary>
        /// Gets the EdmType which represents a 16-byte (128-bit) unique identifier value.
        /// </summary>
        public static EdmType Guid { get; } = new EdmPrimitiveType("Guid", "Edm.Guid", typeof(Guid));

#pragma warning restore CA1720 // Identifier contains type name

#pragma warning disable CA1720 // Identifier contains type name

        /// <summary>
        /// Gets the EdmType which represents a signed 16-bit integer value.
        /// </summary>
        public static EdmType Int16 { get; } = new EdmPrimitiveType("Int16", "Edm.Int16", typeof(short));

#pragma warning restore CA1720 // Identifier contains type name

#pragma warning disable CA1720 // Identifier contains type name

        /// <summary>
        /// Gets the EdmType which represents a signed 32-bit integer value.
        /// </summary>
        public static EdmType Int32 { get; } = new EdmPrimitiveType("Int32", "Edm.Int32", typeof(int));

#pragma warning restore CA1720 // Identifier contains type name

#pragma warning disable CA1720 // Identifier contains type name

        /// <summary>
        /// Gets the EdmType which represents a signed 64-bit integer value.
        /// </summary>
        public static EdmType Int64 { get; } = new EdmPrimitiveType("Int64", "Edm.Int64", typeof(long));

#pragma warning restore CA1720 // Identifier contains type name

        /// <summary>
        /// Gets the EdmType which represents the mathematical concept of binary-valued logic.
        /// </summary>
        public static EdmType NullableBoolean { get; } = new EdmPrimitiveType("Boolean", "Edm.Boolean", typeof(bool?));

        /// <summary>
        /// Gets the EdmType which represents an unsigned 8-bit integer value.
        /// </summary>
        public static EdmType NullableByte { get; } = new EdmPrimitiveType("Byte", "Edm.Byte", typeof(byte?));

        /// <summary>
        /// Gets the EdmType which represents date with values ranging from January 1; 1753 A.D. through December 9999 A.D.
        /// </summary>
        public static EdmType NullableDate { get; } = new EdmPrimitiveType("Date", "Edm.Date", typeof(DateTime?));

        /// <summary>
        /// Gets the EdmType which represents date and time as an Offset in minutes from GMT; with values ranging from 12:00:00 midnight; January 1; 1753 A.D. through 11:59:59 P.M; December 9999 A.D.
        /// </summary>
        public static EdmType NullableDateTimeOffset { get; } = new EdmPrimitiveType("DateTimeOffset", "Edm.DateTimeOffset", typeof(DateTimeOffset?));

        /// <summary>
        /// Gets the EdmType which represents numeric values with fixed precision and scale. This type can describe a numeric value ranging from negative 10^255 + 1 to positive 10^255 -1.
        /// </summary>
        public static EdmType NullableDecimal { get; } = new EdmPrimitiveType("Decimal", "Edm.Decimal", typeof(decimal?));

        /// <summary>
        /// Gets the EdmType which represents a floating point number with 15 digits precision that can represent values with approximate range of ± 2.23e -308 through ± 1.79e +308.
        /// </summary>
        public static EdmType NullableDouble { get; } = new EdmPrimitiveType("Double", "Edm.Double", typeof(double?));

        /// <summary>
        /// Gets the EdmType which represents a duration.
        /// </summary>
        public static EdmType NullableDuration { get; } = new EdmPrimitiveType("Duration", "Edm.Duration", typeof(TimeSpan?));

        /// <summary>
        /// Gets the EdmType which represents a 16-byte (128-bit) unique identifier value.
        /// </summary>
        public static EdmType NullableGuid { get; } = new EdmPrimitiveType("Guid", "Edm.Guid", typeof(Guid?));

        /// <summary>
        /// Gets the EdmType which represents a signed 16-bit integer value.
        /// </summary>
        public static EdmType NullableInt16 { get; } = new EdmPrimitiveType("Int16", "Edm.Int16", typeof(short?));

        /// <summary>
        /// Gets the EdmType which represents a signed 32-bit integer value.
        /// </summary>
        public static EdmType NullableInt32 { get; } = new EdmPrimitiveType("Int32", "Edm.Int32", typeof(int?));

        /// <summary>
        /// Gets the EdmType which represents a signed 64-bit integer value.
        /// </summary>
        public static EdmType NullableInt64 { get; } = new EdmPrimitiveType("Int64", "Edm.Int64", typeof(long?));

        /// <summary>
        /// Gets the EdmType which represents a signed 8-bit integer value.
        /// </summary>
        public static EdmType NullableSByte { get; } = new EdmPrimitiveType("SByte", "Edm.SByte", typeof(sbyte?));

        /// <summary>
        /// Gets the EdmType which represents a floating point number with 7 digits precision that can represent values with approximate range of ± 1.18e -38 through ± 3.40e +38.
        /// </summary>
        public static EdmType NullableSingle { get; } = new EdmPrimitiveType("Single", "Edm.Single", typeof(float?));

        /// <summary>
        /// Gets the EdmType which represents the time of day with values ranging from 0:00:00.x to 23:59:59.y, where x and y depend upon the precision.
        /// </summary>
        public static EdmType NullableTimeOfDay { get; } = new EdmPrimitiveType("TimeOfDay", "Edm.TimeOfDay", typeof(TimeSpan?));

        /// <summary>
        /// Gets the EdmType which represents a signed 8-bit integer value.
        /// </summary>
        public static EdmType SByte { get; } = new EdmPrimitiveType("SByte", "Edm.SByte", typeof(sbyte));

#pragma warning disable CA1720 // Identifier contains type name

        /// <summary>
        /// Gets the EdmType which represents a floating point number with 7 digits precision that can represent values with approximate range of ± 1.18e -38 through ± 3.40e +38.
        /// </summary>
        public static EdmType Single { get; } = new EdmPrimitiveType("Single", "Edm.Single", typeof(float));

#pragma warning restore CA1720 // Identifier contains type name

        /// <summary>
        /// Gets the EdmType which represents a binary data stream.
        /// </summary>
        public static EdmType Stream { get; } = new EdmPrimitiveType("Stream", "Edm.Stream", typeof(Stream));

#pragma warning disable CA1720 // Identifier contains type name

        /// <summary>
        /// Gets the EdmType which represents fixed- or variable-length character data.
        /// </summary>
        public static EdmType String { get; } = new EdmPrimitiveType("String", "Edm.String", typeof(string));

#pragma warning restore CA1720 // Identifier contains type name

        /// <summary>
        /// Gets the EdmType which represents the time of day with values ranging from 0:00:00.x to 23:59:59.y, where x and y depend upon the precision.
        /// </summary>
        public static EdmType TimeOfDay { get; } = new EdmPrimitiveType("TimeOfDay", "Edm.TimeOfDay", typeof(TimeSpan));
    }
}