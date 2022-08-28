using EventAPI.DAL.Context;
using EventAPI.UI.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace EventAPI.UI.DI
{
    public static class AuthentificationDI
    {
        public static void AddAuthentificationDI(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 6;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
                .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
                .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                .AddInMemoryClients(IdentityConfiguration.Clients)
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<IdentityUser>();

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:7075/";
                    options.Audience = "NotesWebAPI";
                    options.RequireHttpsMetadata = false;
                });
        }
    }
}
