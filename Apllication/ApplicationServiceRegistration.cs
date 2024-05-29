using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Autom Mapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //Fluent Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //MediatR
            services.AddMediatR(config => {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
                config.AddOpenBehavior(typeof(CachingBehavior<,>));
                config.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
            });
            //Rules
            services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
            return services;
        }
        public static IServiceCollection AddSubClassesOfType(
            this IServiceCollection services,
            Assembly assembly,
            Type type,
            Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
        )
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
            foreach (var item in types)
                if (addWithLifeCycle == null)
                    services.AddScoped(item);
                else
                    addWithLifeCycle(services, type);
            return services;
        }
    }
}
