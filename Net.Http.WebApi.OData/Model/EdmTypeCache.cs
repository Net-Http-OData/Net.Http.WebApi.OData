// -----------------------------------------------------------------------
// <copyright file="EdmTypeCache.cs" company="Project Contributors">
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
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    internal static class EdmTypeCache
    {
        private static ConcurrentDictionary<Type, EdmType> map = new ConcurrentDictionary<Type, EdmType>(
            new Dictionary<Type, EdmType>
            {
                [typeof(byte[])] = EdmPrimitiveType.Binary,
                [typeof(bool)] = EdmPrimitiveType.Boolean,
                [typeof(bool?)] = EdmPrimitiveType.Boolean,
                [typeof(byte)] = EdmPrimitiveType.Byte,
                [typeof(byte?)] = EdmPrimitiveType.Byte,
                [typeof(DateTime)] = EdmPrimitiveType.Date,
                [typeof(DateTime?)] = EdmPrimitiveType.Date,
                [typeof(DateTimeOffset)] = EdmPrimitiveType.DateTimeOffset,
                [typeof(DateTimeOffset?)] = EdmPrimitiveType.DateTimeOffset,
                [typeof(decimal)] = EdmPrimitiveType.Decimal,
                [typeof(decimal?)] = EdmPrimitiveType.Decimal,
                [typeof(double)] = EdmPrimitiveType.Double,
                [typeof(double?)] = EdmPrimitiveType.Double,
                [typeof(TimeSpan)] = EdmPrimitiveType.Duration,
                [typeof(TimeSpan?)] = EdmPrimitiveType.Duration,
                [typeof(Guid)] = EdmPrimitiveType.Guid,
                [typeof(Guid?)] = EdmPrimitiveType.Guid,
                [typeof(short)] = EdmPrimitiveType.Int16,
                [typeof(short?)] = EdmPrimitiveType.Int16,
                [typeof(int)] = EdmPrimitiveType.Int32,
                [typeof(int?)] = EdmPrimitiveType.Int32,
                [typeof(long)] = EdmPrimitiveType.Int64,
                [typeof(long?)] = EdmPrimitiveType.Int64,
                [typeof(sbyte)] = EdmPrimitiveType.SByte,
                [typeof(sbyte?)] = EdmPrimitiveType.SByte,
                [typeof(float)] = EdmPrimitiveType.Single,
                [typeof(float?)] = EdmPrimitiveType.Single,
                [typeof(char)] = EdmPrimitiveType.String,
                [typeof(char?)] = EdmPrimitiveType.String,
                [typeof(string)] = EdmPrimitiveType.String,
                ////[typeof(TimeSpan)] = EdmPrimitiveType.TimeOfDay,
                ////[typeof(TimeSpan?)] = EdmPrimitiveType.TimeOfDay
            });

        internal static ConcurrentDictionary<Type, EdmType> Map => map;
    }
}