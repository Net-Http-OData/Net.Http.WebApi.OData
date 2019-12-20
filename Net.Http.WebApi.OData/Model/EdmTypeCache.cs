// -----------------------------------------------------------------------
// <copyright file="EdmTypeCache.cs" company="Project Contributors">
// Copyright 2012 - 2019 Project Contributors
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
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    internal static class EdmTypeCache
    {
        internal static ConcurrentDictionary<Type, EdmType> Map { get; } = new ConcurrentDictionary<Type, EdmType>(
            new Dictionary<Type, EdmType>
            {
                [typeof(byte[])] = EdmPrimitiveType.Binary,
                [typeof(bool)] = EdmPrimitiveType.Boolean,
                [typeof(bool?)] = EdmPrimitiveType.NullableBoolean,
                [typeof(byte)] = EdmPrimitiveType.Byte,
                [typeof(byte?)] = EdmPrimitiveType.NullableByte,
                [typeof(DateTime)] = EdmPrimitiveType.Date,
                [typeof(DateTime?)] = EdmPrimitiveType.NullableDate,
                [typeof(DateTimeOffset)] = EdmPrimitiveType.DateTimeOffset,
                [typeof(DateTimeOffset?)] = EdmPrimitiveType.NullableDateTimeOffset,
                [typeof(decimal)] = EdmPrimitiveType.Decimal,
                [typeof(decimal?)] = EdmPrimitiveType.NullableDecimal,
                [typeof(double)] = EdmPrimitiveType.Double,
                [typeof(double?)] = EdmPrimitiveType.NullableDouble,
                [typeof(TimeSpan)] = EdmPrimitiveType.Duration,
                [typeof(TimeSpan?)] = EdmPrimitiveType.NullableDuration,
                [typeof(Guid)] = EdmPrimitiveType.Guid,
                [typeof(Guid?)] = EdmPrimitiveType.NullableGuid,
                [typeof(short)] = EdmPrimitiveType.Int16,
                [typeof(short?)] = EdmPrimitiveType.NullableInt16,
                [typeof(int)] = EdmPrimitiveType.Int32,
                [typeof(int?)] = EdmPrimitiveType.NullableInt32,
                [typeof(long)] = EdmPrimitiveType.Int64,
                [typeof(long?)] = EdmPrimitiveType.NullableInt64,
                [typeof(sbyte)] = EdmPrimitiveType.SByte,
                [typeof(sbyte?)] = EdmPrimitiveType.NullableSByte,
                [typeof(float)] = EdmPrimitiveType.Single,
                [typeof(float?)] = EdmPrimitiveType.NullableSingle,
                ////[typeof(Stream)] = EdmPrimitiveType.Stream,
                [typeof(char)] = EdmPrimitiveType.String,
                [typeof(char?)] = EdmPrimitiveType.String,
                [typeof(string)] = EdmPrimitiveType.String,
                ////[typeof(TimeSpan)] = EdmPrimitiveType.TimeOfDay,
                ////[typeof(TimeSpan?)] = EdmPrimitiveType.NullableTimeOfDay
            });
    }
}