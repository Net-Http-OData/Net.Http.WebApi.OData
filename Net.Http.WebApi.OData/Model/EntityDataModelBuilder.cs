﻿// -----------------------------------------------------------------------
// <copyright file="EntityDataModelBuilder.cs" company="Project Contributors">
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
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A class which builds the <see cref="EntityDataModel"/>.
    /// </summary>
    public sealed class EntityDataModelBuilder
    {
        private readonly Dictionary<string, EdmComplexType> entitySets;

        /// <summary>
        /// Initialises a new instance of the <see cref="EntityDataModelBuilder"/> class.
        /// </summary>
        /// <remarks>Uses <see cref="StringComparer"/>.OrdinalIgnoreCase for the entity set name comparer.</remarks>
        public EntityDataModelBuilder()
            : this(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="EntityDataModelBuilder"/> class.
        /// </summary>
        /// <param name="entitySetNameComparer">The equality comparer to use for the entity set name.</param>
        public EntityDataModelBuilder(IEqualityComparer<string> entitySetNameComparer)
        {
            this.entitySets = new Dictionary<string, EdmComplexType>(entitySetNameComparer);
        }

        /// <summary>
        /// Builds the Entity Data Model containing the collections registered.
        /// </summary>
        /// <returns>The Entity Data Model.</returns>
        public EntityDataModel BuildModel()
        {
            EntityDataModel.Current = new EntityDataModel(this.entitySets);

            return EntityDataModel.Current;
        }

        /// <summary>
        /// Registers an Entity Set of the specified type to the Entity Data Model with the specified name.
        /// </summary>
        /// <typeparam name="T">The type exposed by the collection.</typeparam>
        /// <param name="entitySetName">Name of the Entity Set.</param>
        public void RegisterEntitySet<T>(string entitySetName)
        {
            var edmType = (EdmComplexType)EdmTypeCache.Map.GetOrAdd(typeof(T), EdmTypeResolver);

            this.entitySets.Add(entitySetName, edmType);
        }

        private static EdmType EdmTypeResolver(Type clrType)
        {
            if (clrType.IsEnum)
            {
                var members = new List<EdmEnumMember>();

                foreach (var x in Enum.GetValues(clrType))
                {
                    members.Add(new EdmEnumMember(x.ToString(), (int)x));
                }

                return new EdmEnumType(clrType.FullName, clrType, members.AsReadOnly());
            }

            var clrTypeProperties = clrType.GetProperties().OrderBy(p => p.Name);

            var edmProperties = new List<EdmProperty>();
            var edmComplexType = new EdmComplexType(clrType.FullName, clrType, edmProperties.AsReadOnly());

            edmProperties.AddRange(
                clrTypeProperties.Select(
                    p => new EdmProperty(
                        p.Name,
                        EdmTypeCache.Map.GetOrAdd(p.PropertyType, EdmTypeResolver),
                        edmComplexType)));

            return edmComplexType;
        }
    }
}