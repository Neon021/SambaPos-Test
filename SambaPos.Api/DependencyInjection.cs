using Microsoft.AspNetCore.Mvc.Infrastructure;
using SambaPos.Api.Common.Mapping;
using SambaPos.Api.Errors;

namespace SambaPos.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, SambaPosProblemDetailsFactory>();
        services.AddMappings();

        return services;
    }

}