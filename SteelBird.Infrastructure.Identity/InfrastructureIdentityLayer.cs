using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SteelBird.Application.Contracts;
using SteelBird.Application.Wrappers;
using SteelBird.Infrastructure.Identity.Contexts;
using SteelBird.Infrastructure.Identity.Models;
using SteelBird.Infrastructure.Identity.Services;
using SteelBird.Shared.Setting;
using System.Text;

namespace SteelBird.Infrastructure.Identity;

public static class InfrastructureIdentityLayer
{
    public static IServiceCollection RegisterInfrastructureIdentityServices(this IServiceCollection services, IConfiguration configuration,bool useInMemoryDatabase)
    {
        if (useInMemoryDatabase)
            services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityAppDbContext"));
        else
        {
            services.AddDbContext<IdentityContext>(options =>
                            options.UseSqlServer(
                            configuration.GetConnectionString("IdentityDbContext"),
                                b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
        }
        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
        #region Services
        services.AddTransient<IAccountService, AccountService>();
        #endregion
        services.Configure<JwtSetting>(configuration.GetSection("JwtSetting"));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSetting:ValidIssuer"],
                    ValidAudience = configuration["JwtSetting:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSetting:Secret"]))
                };
                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(Result.Failure(new Error("401", "You are not authorized to access this resource")));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(Result.Failure(new Error("403", "You are not authorized to access this resource")));
                        return context.Response.WriteAsync(result);
                    },
                };
            });

        return services;
    }

}
