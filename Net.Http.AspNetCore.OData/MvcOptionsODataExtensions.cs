using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Net.Http.OData.AspNetCore;
using Net.Http.OData.Model;

namespace Net.Http.AspNetCore.OData
{
    public static class MvcOptionsODataExtensions
    {
        public static void AddOData(
            this MvcOptions options,
            Action<EntityDataModelBuilder> entityDataModelBuilderCallback)
            => AddOData(options, entityDataModelBuilderCallback, StringComparer.OrdinalIgnoreCase);

        public static void AddOData(
            this MvcOptions options,
            Action<EntityDataModelBuilder> entityDataModelBuilderCallback,
            IEqualityComparer<string> entitySetNameComparer)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (entityDataModelBuilderCallback is null)
            {
                throw new ArgumentNullException(nameof(entityDataModelBuilderCallback));
            }

            // add custom binder to beginning of collection
            options.ModelBinderProviders.Insert(0, new ODataQueryOptionsModelBinderProvider());

            var entityDataModelBuilder = new EntityDataModelBuilder(entitySetNameComparer);
            entityDataModelBuilderCallback(entityDataModelBuilder);
            entityDataModelBuilder.BuildModel();
        }
    }
}