using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using PropertySales.Application.Common.Behaviors;
using PropertySales.Application.Common.Caches;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        services.AddSingleton<IMemoryCache, MemoryCache>();
        services.AddScoped(typeof(ICacheManager<>), typeof(CacheManager<>));
        
        return services;
    }
}