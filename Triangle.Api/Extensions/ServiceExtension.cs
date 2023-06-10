using Triangle.Data.IRepositories;
using Triangle.Data.Repository;


namespace Triangle.Api.Extensions;

public static class ServiceExtension
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
