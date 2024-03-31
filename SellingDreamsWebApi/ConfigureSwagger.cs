using Microsoft.OpenApi.Models;

namespace SellingDreamsWebApi;

public static class ConfigureSwagger
{
  public static IServiceCollection ConfigureSwaggerGen(this IServiceCollection services)
  {
    services.AddSwaggerGen(opt =>
    {
      opt.SwaggerDoc("v1", new OpenApiInfo { Title = "SellingDreamsWebApi", Version = "v1" });
      opt.AddSecurityDefinition(
              "Bearer",
              new OpenApiSecurityScheme
            {
              In = ParameterLocation.Header,
              Description = "Please enter token",
              Type = SecuritySchemeType.Http,
              BearerFormat = "JWT",
              Scheme = "bearer"
            }
          );

      opt.AddSecurityRequirement(
              new OpenApiSecurityRequirement
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
            }
          );
    });
    return services;
  }
}
