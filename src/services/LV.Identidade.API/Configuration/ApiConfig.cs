namespace LV.Identidade.API.Configuration;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        return services;
    }

    public static IApplicationBuilder UseApiConfiguration(this WebApplication app, IWebHostEnvironment environment)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseIdentityConfiguration();

        app.MapControllers();
        
        return app;
    }
}