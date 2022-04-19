using LV.Identidade.API.Configuration;

namespace LV.Identidade.API;

public class Startup : IStartup
{
    public Startup(IHostEnvironment hostEnvironment)
    {
        
        // Configuration = configuration;

        var builder = new ConfigurationBuilder()
            .SetBasePath(hostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();
        
        if (hostEnvironment.IsDevelopment())
        {
            builder.AddUserSecrets<Startup>();
        }
        
        Configuration = builder.Build();
    }
    
    public IConfiguration Configuration { get; }

    public void ConfigurationServices(IServiceCollection services)
    {
        services.AddIdentityConfiguration(Configuration);
        services.AddApiConfiguration();
        services.AddSwaggerConfiguration();
    }

    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {
        app.UseSwaggerConfiguration();
        app.UseApiConfiguration(environment);
    }
}

public interface IStartup
{
    IConfiguration Configuration { get; }
    void Configure(WebApplication app, IWebHostEnvironment environment);
    void ConfigurationServices(IServiceCollection service);
}

public static class StartupExtensions
{
    public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder webAppBuilder)
        where TStartup : IStartup
    {
        if (Activator.CreateInstance(typeof(TStartup), webAppBuilder.Environment) is not IStartup startup) throw new ArgumentException("Classe Startup.cs inválida!");
        startup.ConfigurationServices(webAppBuilder.Services);
        
        var app = webAppBuilder.Build();
        startup.Configure(app, app.Environment);
        app.Run();
        return webAppBuilder;
    }
}

/*
 webAppBuilder.Configuration.SetBasePath(webAppBuilder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{webAppBuilder.Environment.EnvironmentName}.json", true, true)
        .AddEnvironmentVariables();
        if (webAppBuilder.Environment.IsDevelopment())
        {
            webAppBuilder.Configuration.AddUserSecrets<Startup>();
        }
*/