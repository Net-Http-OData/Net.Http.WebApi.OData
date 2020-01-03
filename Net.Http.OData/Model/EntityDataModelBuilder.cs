﻿// -----------------------------------------------------------------------
// <copyright file="EntityDataModelBuilder.cs" company="Project Contributors">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// A class which builds the <see cref="EntityDataModel"/>.
    /// </summary>
    public sealed class EntityDataModelBuilder
    {
        private readonly EntityDataModel entityDataModel;
        private readonly Dictionary<string, EntitySet> entitySets;

        /// <summary>
        /// Initialises a new instance of the <see cref="EntityDataModelBuilder"/> class.
        /// </summary>
        /// <param name="entitySetNameComparer">The equality comparer to use for the Entity Set name.</param>
        public EntityDataModelBuilder(IEqualityComparer<string> entitySetNameComparer)
        {
            this.entitySets = new Dictionary<string, EntitySet>(entitySetNameComparer) ?? throw new ArgumentNullException(nameof(entitySetNameComparer));
            this.entityDataModel = new EntityDataModel(this.entitySets);
        }

        /// <summary>
        /// Builds the Entity Data Model containing the collections registered.
        /// </summary>
        /// <returns>The Entity Data Model.</returns>
        public EntityDataModel BuildModel() => EntityDataModel.Current = this.entityDataModel;

        /// <summary>
        /// Registers an Entity Set of the specified type to the Entity Data Model with the name of the type as the Entity Set name which can only be queried.
        /// </summary>
        /// <typeparam name="T">The type exposed by the collection.</typeparam>
        /// <param name="entityKeyExpression">The Entity Key expression.</param>
        [Obsolete("Please use RegisterEntitySet<T>(string entitySetName, Expression<Func<T, object>> entityKeyExpression), this method will be removed in a future version")]
        public void RegisterEntitySet<T>(Expression<Func<T, object>> entityKeyExpression)
            => this.RegisterEntitySet(typeof(T).Name, entityKeyExpression, Capabilities.None);

        /// <summary>
        /// Registers an Entity Set of the specified type to the Entity Data Model with the name of the type as the Entity Set name and <see cref="Capabilities"/>.
        /// </summary>
        /// <typeparam name="T">The type exposed by the collection.</typeparam>
        /// <param name="entityKeyExpression">The Entity Key expression.</param>
        /// <param name="capabilities">The capabilities of the Entity Set.</param>
        [Obsolete("Please use RegisterEntitySet<T>(string entitySetName, Expression<Func<T, object>> entityKeyExpression, Capabilities capabilities), this method will be removed in a future version")]
        public void RegisterEntitySet<T>(Expression<Func<T, object>> entityKeyExpression, Capabilities capabilities)
            => this.RegisterEntitySet(typeof(T).Name, entityKeyExpression, capabilities);

        /// <summary>
        /// Registers an Entity Set of the specified type to the Entity Data Model with the specified name which can only be queried.
        /// </summary>
        /// <typeparam name="T">The type exposed by the collection.</typeparam>
        /// <param name="entitySetName">Name of the Entity Set.</param>
        /// <param name="entityKeyExpression">The Entity Key expression.</param>
        public void RegisterEntitySet<T>(string entitySetName, Expression<Func<T, object>> entityKeyExpression)
            => this.RegisterEntitySet(entitySetName, entityKeyExpression, Capabilities.None);

        /// <summary>
        /// Registers an Entity Set of the specified type to the Entity Data Model with the specified name and <see cref="Capabilities"/>.
        /// </summary>
        /// <typeparam name="T">The type exposed by the collection.</typeparam>
        /// <param name="entitySetName">Name of the Entity Set.</param>
        /// <param name="entityKeyExpression">The Entity Key expression.</param>
        /// <param name="capabilities">The capabilities of the Entity Set.</param>
        public void RegisterEntitySet<T>(string entitySetName, Expression<Func<T, object>> entityKeyExpression, Capabilities capabilities)
        {
            if (entityKeyExpression is null)
            {
                throw new ArgumentNullException(nameof(entityKeyExpression));
            }

            var edmType = (EdmComplexType)EdmTypeCache.Map.GetOrAdd(typeof(T), this.EdmTypeResolver);

            var entityKey = edmType.BaseType is null ? edmType.GetProperty(entityKeyExpression.GetMemberInfo().Name) : null;

            var entitySet = new EntitySet(entitySetName, edmType, entityKey, capabilities);

            this.entitySets.Add(entitySet.Name, entitySet);
        }

        private EdmType EdmTypeResolver(Type clrType)
        {
            if (clrType.IsEnum)
            {
                var members = new List<EdmEnumMember>();

                foreach (var value in Enum.GetValues(clrType))
                {
                    members.Add(new EdmEnumMember(value.ToString(), (int)value));
                }

                return EdmTypeCache.Map.GetOrAdd(clrType, t => new EdmEnumType(t, members.AsReadOnly()));
            }

            if (clrType.IsGenericType)
            {
                var innerType = clrType.GetGenericArguments()[0];

                if (typeof(IEnumerable<>).MakeGenericType(innerType).IsAssignableFrom(clrType))
                {
                    var containedType = EdmTypeCache.Map.GetOrAdd(innerType, this.EdmTypeResolver);

                    return EdmTypeCache.Map.GetOrAdd(clrType, t => new EdmCollectionType(t, containedType));
                }
                else
                {
                    throw new NotSupportedException(clrType.FullName);
                }
            }

            EdmType baseEdmType = clrType.BaseType != typeof(object) ? EdmTypeCache.Map.GetOrAdd(clrType.BaseType, this.EdmTypeResolver) : null;

            var clrTypeProperties = clrType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            var edmProperties = new List<EdmProperty>(clrTypeProperties.Length);
            var edmComplexType = (EdmComplexType)EdmTypeCache.Map.GetOrAdd(clrType, t => new EdmComplexType(t, baseEdmType, edmProperties.AsReadOnly()));

            edmProperties.AddRange(clrTypeProperties
                .OrderBy(p => p.Name)
                .Select(p => new EdmProperty(p, EdmTypeCache.Map.GetOrAdd(p.PropertyType, this.EdmTypeResolver), edmComplexType, this.entityDataModel.IsEntitySet)));

            return edmComplexType;
        }
    }
}