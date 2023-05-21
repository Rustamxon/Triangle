using Triangle.Data.IRepositories;
using Triangle.Data.Repository;


namespace Triangle.Api.Extensions;

public static class ServiceExtension
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        //services.AddScoped<IUserService, UserService>();
        //services.AddScoped<IAuthService, AuthService>();
    }

    //#region Setup Swagger
    //public static void AddSwaggerService(this IServiceCollection services)
    //{
    //    services.AddSwaggerGen(c =>
    //    {
    //        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ahsan.Api", Version = "v1" });
    //        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    //        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //        {
    //            Name = "Authorization",
    //            Description =
    //                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
    //            In = ParameterLocation.Header,
    //            Type = SecuritySchemeType.ApiKey
    //        });

    //        c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //        {
    //            {
    //                new OpenApiSecurityScheme
    //                {
    //                    Reference = new OpenApiReference
    //                    {
    //                        Type = ReferenceType.SecurityScheme,
    //                        Id = "Bearer"
    //                    }
    //                },
    //                new string[] { }
    //            }
    //        });
    //    });
    //}
    //#endregion
}
