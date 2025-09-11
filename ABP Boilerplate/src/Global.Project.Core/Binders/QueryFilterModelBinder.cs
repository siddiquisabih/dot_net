using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Global.Project.Binders
{

    public class QueryFilterModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext context)
        {
            var filters = context.HttpContext.Request.Query["Filters"];
            context.Result = ModelBindingResult.Success(filters.ToString());
            return Task.CompletedTask;
        }
    }
}
