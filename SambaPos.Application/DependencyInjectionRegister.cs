using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SambaPos.Application.Common.Behaviours;
using SambaPos.Application.RabbitMQ.Consumer;
using SambaPos.Application.RabbitMQ.Publisher;
using System.Reflection;

namespace SambaPos.Application;
public static class DependencyInjectionRegister
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjectionRegister).GetTypeInfo().Assembly));


        services.AddSingleton<IPublisherService, PublisherService>();
        services.AddSingleton<IConsumerService, ConsumerService>();

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
