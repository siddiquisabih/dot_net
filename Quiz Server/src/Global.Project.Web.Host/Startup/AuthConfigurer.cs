using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Abp.Runtime.Security;
using Global.Project;

namespace Global.Project.Web.Host.Startup
{
    public static class AuthConfigurer
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]))
            {
                services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                }).AddJwtBearer("JwtBearer", options =>
                {
                    options.Audience = configuration["Authentication:JwtBearer:Audience"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),

                        ValidateIssuer = true,
                        ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = configuration["Authentication:JwtBearer:Audience"],

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = QueryStringTokenResolver
                    };
                });
            }
        }

        private static Task QueryStringTokenResolver(MessageReceivedContext context)
        {
            if (!context.HttpContext.Request.Path.HasValue ||
                !context.HttpContext.Request.Path.Value.StartsWith("/signalr"))
            {
                return Task.CompletedTask;
            }

            var qsAuthToken = context.HttpContext.Request.Query["access_token"].FirstOrDefault();
            if (qsAuthToken == null)
            {
                return Task.CompletedTask;
            }

            context.Token = SimpleStringCipher.Instance.Decrypt(qsAuthToken, AppConsts.DefaultPassPhrase);
            return Task.CompletedTask;
        }
    }
}
