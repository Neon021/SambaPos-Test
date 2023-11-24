using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SambaPos.Application.Common.Interfaces.Persistance;
using SambaPos.Infrastructure.Persistence;
using SambaPos.Infrastructure.Persistence.Repositories;

namespace SambaPos.Infrastructure;
public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configration)
    {
        services
            .AddPersistance(configration);

        return services;
    }

    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SambaPosDbContext>(options =>
        //ConnectionString is accessed from the user-secrets of the Api layer which passed by IConfiguration
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}