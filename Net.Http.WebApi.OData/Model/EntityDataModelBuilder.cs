// -----------------------------------------------------------------------
// <copyright file="EntityDataModelBuilder.cs" company="Project Contributors">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// A class which builds the <see cref="EntityDataModel"/>.
    /// </summary>
    public sealed class EntityDataModelBuilder
    {
        private readonly Dictionary<string, EntitySet> entitySets;

        /// <summary>
        /// Initialises a new instance of the <see cref="EntityDataModelBuilder"/> class.
        /// </summary>
        /// <param name="entitySetNameComparer">The equality comparer to use for the Entity Set name.</param>
        internal EntityDataModelBuilder(IEqualityComparer<string> entitySetNameComparer)
        {
            this.entitySets = new Dictionary<string, EntitySet>(entitySetNameComparer) ?? throw new ArgumentNullException(nameof(entitySetNameComparer));
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

            var edmType = (EdmComplexType)EdmTypeCache.Map.GetOrAdd(typeof(T), t => EdmTypeResolver(t));

            var entityKey = edmType.BaseType is null ? edmType.GetProperty(entityKeyExpression.GetMemberInfo().Name) : null;

            var entitySet = new EntitySet(entitySetName, edmType, entityKey, capabilities);

            this.entitySets.Add(entitySet.Name, entitySet);
        }

        private static EdmType EdmTypeResolver(Type clrType)
        {
            if (clrType.IsEnum)
            {
                var members = new List<EdmEnumMember>();

                foreach (var value in Enum.GetValues(clrType))
                {
                    members.Add(new EdmEnumMember(value.ToString(), (int)value));
                }

                return new EdmEnumType(clrType, members.AsReadOnly());
            }

            if (clrType.IsGenericType)
            {
                var innerType = clrType.GetGenericArguments()[0];

                if (typeof(IEnumerable<>).MakeGenericType(innerType).IsAssignableFrom(clrType))
                {
                    var containedType = EdmTypeCache.Map.GetOrAdd(innerType, EdmTypeResolver(innerType));

                    return EdmTypeCache.Map.GetOrAdd(clrType, t => new EdmCollectionType(t, containedType));
                }
            }

            EdmType baseType = clrType.BaseType != typeof(object) ? baseType = EdmTypeResolver(clrType.BaseType) : null;

            var clrTypeProperties = clrType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .OrderBy(p => p.Name);

            var edmProperties = new List<EdmProperty>();
            var edmComplexType = new EdmComplexType(clrType, baseType, edmProperties);

            edmProperties.AddRange(
                clrTypeProperties.Select(
                    p => new EdmProperty(
                        p.Name,
                        EdmTypeCache.Map.GetOrAdd(p.PropertyType, t => EdmTypeResolver(t)),
                        edmComplexType)));

            return edmComplexType;
        }
    }
}