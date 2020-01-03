using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Net.Http.OData.Query;

namespace Net.Http.OData.AspNetCore
{
    public sealed class ODataQueryOptionsModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(ODataQueryOptions))
            {
                return new BinderTypeModelBinder(typeof(ODataQueryOptionsModelBinder));
            }

            return null;
        }
    }
}