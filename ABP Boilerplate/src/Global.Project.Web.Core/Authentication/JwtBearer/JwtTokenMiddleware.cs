using Global.Project.Authorization.Users;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
namespace Global.Project.Authentication.JwtBearer
{
    public class JwtTokenMiddleware
    {

        private readonly RequestDelegate _next;

        public JwtTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        // public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder app, string schema = JwtBearerDefaults.AuthenticationScheme)
        // {
        //     return app.Use(async (ctx, next) =>
        //     {
        //         if (ctx.User.Identity?.IsAuthenticated != true)
        //         {
        //             var result = await ctx.AuthenticateAsync(schema);
        //             if (result.Succeeded && result.Principal != null)
        //             {
        //                 ctx.User = result.Principal;
        //             }
        //         }

        //         await next();
        //     });
        // }


        public async Task Invoke(HttpContext context, UserManager<User> userManager)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = context.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId != null)
                {
                    var user = await userManager.FindByIdAsync(userId);
                    if (user != null && !user.IsActive)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            code = 403,
                            message = "User account is inactive.",
                            details = "Please contact the administrator."
                        });
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
