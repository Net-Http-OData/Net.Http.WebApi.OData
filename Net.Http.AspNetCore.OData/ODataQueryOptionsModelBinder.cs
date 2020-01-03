using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Net.Http.OData.AspNetCore
{
    internal class ODataQueryOptionsModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext is null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            throw new NotImplementedException();
        }
    }
}