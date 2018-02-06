// -----------------------------------------------------------------------
// <copyright file="MetadataProvider.cs" company="Project Contributors">
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
namespace Net.Http.WebApi.OData.Metadata
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Xml.Linq;
    using Net.Http.WebApi.OData.Model;

    internal static class MetadataProvider
    {
        private static XNamespace edmNs = "http://docs.oasis-open.org/odata/ns/edm";
        private static XNamespace edmxNs = "http://docs.oasis-open.org/odata/ns/edmx";

        internal static XDocument Create(EntityDataModel entityDataModel)
        {
            var document = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement(
                    edmxNs + "Edmx",
                    new XAttribute("Version", "4.0"),
                    new XAttribute(XNamespace.Xmlns + "edmx", edmxNs),
                    new XElement(
                        edmxNs + "DataServices",
                        new XElement(
                            edmNs + "Schema",
                            new XAttribute("Namespace", entityDataModel.EntitySets.First().Value.EdmType.ClrType.Namespace),
                            GetEnumTypes(entityDataModel),
                            GetComplexTypes(entityDataModel),
                            GetEntityTypes(entityDataModel),
                            GetFunctions(entityDataModel),
                            GetActions(entityDataModel),
                            GetEntityContainer(entityDataModel),
                            GetAnnotations(entityDataModel)))));

            return document;
        }

        private static IEnumerable<XElement> GetActions(EntityDataModel entityDataModel) => Enumerable.Empty<XElement>();

        private static XElement GetAnnotations(EntityDataModel entityDataModel)
        {
            var annotations = new XElement(
                edmNs + "Annotations",
                new XAttribute("Target", entityDataModel.EntitySets.First().Value.EdmType.ClrType.Namespace + ".DefaultContainer"),
                new XElement(
                    edmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.DereferenceableIDs"),
                    new XAttribute("Bool", "true")),
                new XElement(
                    edmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.ConventionalIDs"),
                    new XAttribute("Bool", "true")),
                new XElement(
                    edmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.ConformanceLevel"),
                    new XElement(edmNs + "EnumMember", "Org.OData.Capabilities.V1.ConformanceLevelType/Minimal")),
                new XElement(
                    edmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.SupportedFormats"),
                    new XElement(
                        edmNs + "Collection",
                        entityDataModel.SupportedFormats.Select(format => new XElement(edmNs + "String", format)))),
                new XElement(
                    edmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.AsynchronousRequestsSupported"),
                    new XAttribute("Bool", "false")),
                new XElement(
                    edmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.BatchContinueOnErrorSupported"),
                    new XAttribute("Bool", "false")),
                new XElement(
                    edmNs + "Annotation",
                    new XAttribute("Term", "Org.OData.Capabilities.V1.FilterFunctions"),
                    new XElement(
                        edmNs + "Collection",
                        entityDataModel.FilterFunctions.Select(function => new XElement(edmNs + "String", function)))));

            return annotations;
        }

        private static IEnumerable<XElement> GetComplexTypes(EntityDataModel entityDataModel)
        {
            // Any types used in the model which aren't Entity Sets.
            var complexCollectionTypes = entityDataModel.EntitySets
                .SelectMany(kvp => kvp.Value.EdmType.Properties)
                .Select(p => p.PropertyType)
                .OfType<EdmCollectionType>()
                .Select(t => t.ContainedType)
                .OfType<EdmComplexType>();

            var complexTypes = entityDataModel.EntitySets
                .SelectMany(kvp => kvp.Value.EdmType.Properties)
                .Select(p => p.PropertyType)
                .OfType<EdmComplexType>()
                .Concat(complexCollectionTypes)
                .Where(t => !entityDataModel.EntitySets.Any(es => es.Value.EdmType == t))
                .Distinct()
                .Select(t => new XElement(
                    edmNs + "ComplexType",
                    new XAttribute("Name", t.Name),
                    t.Properties.Select(p => new XElement(
                        edmNs + "Property",
                        new XAttribute("Name", p.Name),
                        new XAttribute("Type", p.PropertyType.FullName),
                        new XAttribute("Nullable", "false"))))); // TODO: nullable needs to be set in model (use data annotations?)

            return complexTypes;
        }

        private static XElement GetEntityContainer(EntityDataModel entityDataModel)
        {
            var entityContainer = new XElement(
                edmNs + "EntityContainer",
                new XAttribute(
                    "Name", "DefaultContainer"),
                entityDataModel.EntitySets.Select(
                    kvp => new XElement(
                        edmNs + "EntitySet",
                        new XAttribute("Name", kvp.Key),
                        new XAttribute("EntityType", kvp.Value.EdmType.ClrType.FullName),
                        new XElement(
                            edmNs + "Annotation",
                            new XAttribute("Term", "Org.OData.Core.V1.ResourcePath"),
                            new XAttribute("String", kvp.Key)),
                        new XElement(
                            edmNs + "Annotation",
                            new XAttribute("Term", "Org.OData.Capabilities.V1.InsertRestrictions"),
                            new XElement(
                                edmNs + "Record",
                                new XElement(
                                    edmNs + "PropertyValue",
                                    new XAttribute("Property", "Insertable"),
                                    new XAttribute("Bool", ((kvp.Value.Capabilities & Capabilities.Insertable) == Capabilities.Insertable).ToString())))),
                        new XElement(
                            edmNs + "Annotation",
                            new XAttribute("Term", "Org.OData.Capabilities.V1.UpdateRestrictions"),
                            new XElement(
                                edmNs + "Record",
                                new XElement(
                                    edmNs + "PropertyValue",
                                    new XAttribute("Property", "Updatable"),
                                    new XAttribute("Bool", ((kvp.Value.Capabilities & Capabilities.Updatable) == Capabilities.Updatable).ToString())))),
                        new XElement(
                            edmNs + "Annotation",
                            new XAttribute("Term", "Org.OData.Capabilities.V1.DeleteRestrictions"),
                            new XElement(
                                edmNs + "Record",
                                new XElement(
                                    edmNs + "PropertyValue",
                                    new XAttribute("Property", "Deletable"),
                                    new XAttribute("Bool", ((kvp.Value.Capabilities & Capabilities.Deletable) == Capabilities.Deletable).ToString())))))));

            return entityContainer;
        }

        private static IEnumerable<XElement> GetEntityTypes(EntityDataModel entityDataModel)
        {
            var entityTypes = entityDataModel.EntitySets.Select(
                kvp => new XElement(
                    edmNs + "EntityType",
                    new XAttribute("Name", kvp.Value.Name),
                    new XElement(
                        edmNs + "Key",
                        new XElement(
                            edmNs + "PropertyRef",
                            new XAttribute("Name", kvp.Value.EntityKey.Name))),
                    kvp.Value.EdmType.Properties.Select(p =>
                        new XElement(
                            edmNs + "Property",
                            new XAttribute("Name", p.Name),
                            new XAttribute("Type", p.PropertyType.FullName),
                            new XAttribute("Nullable", "false"))))); // TODO: nullable needs to be set in model (use data annotations?)

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
                    edmNs + "EnumType",
                    new XAttribute("Name", t.Name),
                    new XAttribute("UnderlyingType", EdmType.GetEdmType(Enum.GetUnderlyingType(t.ClrType)).FullName),
                    new XAttribute("IsFlags", (t.ClrType.GetCustomAttribute<FlagsAttribute>() != null).ToString()),
                    t.Members.Select(m => new XElement(
                        edmNs + "Member",
                        new XAttribute("Name", m.Name),
                        new XAttribute("Value", m.Value.ToString(CultureInfo.InvariantCulture))))));

            return enumTypes;
        }

        private static IEnumerable<XElement> GetFunctions(EntityDataModel entityDataModel) => Enumerable.Empty<XElement>();

        internal sealed class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding { get; } = Encoding.UTF8;
        }
    }
}