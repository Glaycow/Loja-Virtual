using System.Text;
using LV.Identidade.API.Data;
using LV.Identidade.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LV.Identidade.API.Configuration;

public static class IdentityConfig
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Adicionando o banco de dados.
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddErrorDescriber<IdentityMensagensPortugues>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        // fim adicionado banco de dados.
        
        // Configuração do JWT
        var appSettingsSection = configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSettingsSection);
        var appSettings = appSettingsSection.Get<AppSettings>();
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOptions =>
        {
            bearerOptions.RequireHttpsMetadata = true;
            bearerOptions.SaveToken = true;
            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = appSettings.ValidoEm,
                ValidIssuer = appSettings.Emissor
            };
        });
        // Fim Configuração JWT
        
        return services;
    }

    public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}