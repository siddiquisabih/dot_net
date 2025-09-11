using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Global.Project.Binders
{
    public class QueryFilterBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(string) &&
                context.Metadata.Name == "Filters")
            {
                return new QueryFilterModelBinder();
            }

            return null;
        }
    }
}