// -----------------------------------------------------------------------
// <copyright file="MetadataProvider.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Metadata
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;
    using Net.Http.WebApi.OData.Model;

    /// <summary>
    /// Provides the Metadata XML document for the Entity Data Model.
    /// </summary>
    internal static class MetadataProvider
    {
        private static readonly XNamespace EdmNs = "http://docs.oasis-open.org/odata/ns/edm";
        private static readonly XNamespace EdmxNs = "http://docs.oasis-open.org/odata/ns/edmx";

        /// <summary>
        /// Creates an <see cref="XDocument"/> containing the Metadata XML document for the Entity Data Model.
        /// </summary>
        /// <param name="entityDataModel">The Entity Data Model to include the Metadata for.</param>
        /// <returns>An <see cref="XDocument"/> containing the Metadata XML document for the Entity Data Model.</returns>
        internal static XDocument Create(EntityDataModel entityDataModel)
        {
            if (entityDataModel is null)
            {
                throw new ArgumentNullException(nameof(entityDataModel));
            }

            var document = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement(
                    EdmxNs + "Edmx",
                    new XAttribute(XNamespace.Xmlns + "edmx", EdmxNs),
                    new XAttribute("Version", "4.0"),
                    new XElement(
                        EdmxNs + "DataServices",
                        new XElement(
                            EdmNs + "Schema",
                            new XAttribute("xmlns", EdmNs),
                            new XAttribute("Namespace", entityDataModel.EntitySets.First().Value.EdmType.ClrType.Namespace),
                            GetEnumTypes(entityDataModel),
                            GetComplexTypes(entityDataModel),
                            GetEntityTypes(entityDataModel),
                            GetFunctions(),
                            GetActions(),
                            GetEntityContainer(entityDataModel),
                            GetAnnotations(entityDataModel)))));

            return document;
        }

        private static IEnumerable<XElement> GetActions() => Enumerable.Empty<XElement>();

        private static XElement GetAnnotations(EntityDataModel entityDataModel)
        {
            var annotations = new XElement(
                EdmNs + "Annotations",
                new XAttribute("Target", entityDataModel.EntitySets.First().Value.EdmType.ClrType.Namespace + ".DefaultContainer"),
                new XElement(
                    EdmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.DereferenceableIDs"),
                    new XAttribute("Bool", "true")),
                new XElement(
                    EdmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.ConventionalIDs"),
                    new XAttribute("Bool", "true")),
                new XElement(
                    EdmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.ConformanceLevel"),
                    new XElement(EdmNs + "EnumMember", "Org.OData.Capabilities.V1.ConformanceLevelType/Minimal")),
                new XElement(
                    EdmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.SupportedFormats"),
                    new XElement(
                        EdmNs + "Collection",
                        entityDataModel.SupportedFormats.Select(format => new XElement(EdmNs + "String", format)))),
                new XElement(
                    EdmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.AsynchronousRequestsSupported"),
                    new XAttribute("Bool", "false")),
                new XElement(
                    EdmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.BatchContinueOnErrorSupported"),
                    new XAttribute("Bool", "false")),
                new XElement(
                    EdmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.FilterFunctions"),
                    new XElement(
                        EdmNs + "Collection",
                        entityDataModel.FilterFunctions.Select(function => new XElement(EdmNs + "String", function)))));

            return annotations;
        }

        private static IEnumerable<XElement> GetComplexTypes(EntityDataModel entityDataModel)
        {
            // Any types used in the model which aren't Entity Sets.
            var complexCollectionTypes = entityDataModel.EntitySets.Values
                .SelectMany(t => t.EdmType.Properties)
                .Select(p => p.PropertyType)
                .OfType<EdmCollectionType>()
                .Select(t => t.ContainedType)
                .OfType<EdmComplexType>();

            var complexTypes = entityDataModel.EntitySets.Values
                .SelectMany(t => t.EdmType.Properties)
                .Select(p => p.PropertyType)
                .OfType<EdmComplexType>()
                .Concat(complexCollectionTypes)
                .Where(t => !entityDataModel.IsEntitySet(t))
                .Distinct()
                .Select(t =>
                {
                    var element = new XElement(
                       EdmNs + "ComplexType",
                       new XAttribute("Name", t.Name),
                       GetProperties(entityDataModel, t.Properties));

                    if (t.BaseType != null)
                    {
                        element.Add(new XAttribute("BaseType", t.BaseType.FullName));
                    }

                    return element;
                });

            return complexTypes;
        }

        private static XElement GetEntityContainer(EntityDataModel entityDataModel)
        {
            var entityContainer = new XElement(
                EdmNs + "EntityContainer",
                new XAttribute(
                    "Name", "DefaultContainer"),
                entityDataModel.EntitySets.Select(
                    kvp => new XElement(
                        EdmNs + "EntitySet",
                        new XAttribute("Name", kvp.Key),
                        new XAttribute("EntityType", kvp.Value.EdmType.ClrType.FullName),
                        new XElement(
                            EdmNs + "Annotation",
                            new XAttribute("Term", "Org.OData.Core.V1.ResourcePath"),
                            new XAttribute("String", kvp.Key)),
                        new XElement(
                            EdmNs + "Annotation",
                            new XAttribute("Term", "Org.OData.Capabilities.V1.InsertRestrictions"),
                            new XElement(
                                EdmNs + "Record",
                                new XElement(
                                    EdmNs + "PropertyValue",
                                    new XAttribute("Property", "Insertable"),
                                    new XAttribute("Bool", ((kvp.Value.Capabilities & Capabilities.Insertable) == Capabilities.Insertable).ToString(CultureInfo.InvariantCulture))))),
                        new XElement(
                            EdmNs + "Annotation",
                            new XAttribute("Term", "Org.OData.Capabilities.V1.UpdateRestrictions"),
                            new XElement(
                                EdmNs + "Record",
                                new XElement(
                                    EdmNs + "PropertyValue",
                                    new XAttribute("Property", "Updatable"),
                                    new XAttribute("Bool", ((kvp.Value.Capabilities & Capabilities.Updatable) == Capabilities.Updatable).ToString(CultureInfo.InvariantCulture))))),
                        new XElement(
                            EdmNs + "Annotation",
                            new XAttribute("Term", "Org.OData.Capabilities.V1.DeleteRestrictions"),
                            new XElement(
                                EdmNs + "Record",
                                new XElement(
                                    EdmNs + "PropertyValue",
                                    new XAttribute("Property", "Deletable"),
                                    new XAttribute("Bool", ((kvp.Value.Capabilities & Capabilities.Deletable) == Capabilities.Deletable).ToString(CultureInfo.InvariantCulture))))))));

            return entityContainer;
        }

        private static XElement GetEntityKey(EdmProperty edmProperty)
            => new XElement(
                EdmNs + "Key",
                new XElement(
                    EdmNs + "PropertyRef",
                    new XAttribute("Name", edmProperty.Name)));

        private static IEnumerable<XElement> GetEntityTypes(EntityDataModel entityDataModel)
        {
            var entityTypes = entityDataModel.EntitySets.Values.Select(
                t =>
                {
                    var element = new XElement(
                      EdmNs + "EntityType",
                      new XAttribute("Name", t.Name),
                      GetProperties(entityDataModel, t.EdmType.Properties));

                    if (t.EdmType.BaseType is null)
                    {
                        element.AddFirst(GetEntityKey(t.EntityKey));
                    }
                    else
                    {
                        element.Add(new XAttribute("BaseType", t.EdmType.BaseType.FullName));
                    }

                    return element;
                });

            return entityTypes;
        }

        private static IEnumerable<XElement> GetEnumTypes(EntityDataModel entityDataModel)
        {
            // Any enums defined in the model.
            var enumTypes = entityDataModel.EntitySets
                .SelectMany(kvp => kvp.Value.EdmType.Properties)
                .Select(p => p.PropertyType)
                .OfType<EdmEnumType>()
                .Distinct()
                .Select(t => new XElement(
                    EdmNs + "EnumType",
                    new XAttribute("Name", t.Name),
                    new XAttribute("UnderlyingType", EdmType.GetEdmType(Enum.GetUnderlyingType(t.ClrType)).FullName),
                    new XAttribute("IsFlags", (t.ClrType.GetCustomAttribute<FlagsAttribute>() != null).ToString(CultureInfo.InvariantCulture)),
                    t.Members.Select(m => new XElement(
                        EdmNs + "Member",
                        new XAttribute("Name", m.Name),
                        new XAttribute("Value", m.Value.ToString(CultureInfo.InvariantCulture))))));

            return enumTypes;
        }

        private static IEnumerable<XElement> GetFunctions() => Enumerable.Empty<XElement>();

        private static IEnumerable<XElement> GetProperties(EntityDataModel entityDataModel, IEnumerable<EdmProperty> properties)
            => properties
            .Where(p => !entityDataModel.IsEntitySet(p.PropertyType))
            .Select(p =>
            {
                var element = new XElement(EdmNs + "Property", new XAttribute("Name", p.Name), new XAttribute("Type", p.PropertyType.FullName));

                if (!p.IsNullable)
                {
                    element.Add(new XAttribute("Nullable", "false"));
                }

                return element;
            })
            .Concat(properties
                .Where(p => entityDataModel.IsEntitySet(p.PropertyType))
                .Select(p => new XElement(EdmNs + "NavigationProperty", new XAttribute("Name", p.Name), new XAttribute("Type", p.PropertyType.FullName))));
    }
}