using Microsoft.OpenApi.Models;

namespace LV.Identidade.API.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        // configuração do Swagger
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "V1",
                Title = "Api de identificação",
                Description = "Micro-Service de identificação do sistema e-commerce do nerd store ",
                Contact = new OpenApiContact
                {
                    Email = "glaycow@gmail.com",
                    Name = "Glaycow Silveira Silva e Souza"
                },
                License = new OpenApiLicense
                {
                    Name = "Exemplo license",
                    Url = new Uri("https://example.com/license")
                }
            });
        });

        // fim configuração Swagger
        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return app;
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DocumentTitle = "V1 - Api Identidade";
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
        return app;
    }
}