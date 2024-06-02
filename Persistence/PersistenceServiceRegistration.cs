using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Tls;
using Persistence.Contexts;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Application.Repositories;
using Persistence.Repositories;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Db

            services.AddDbContext<AppointmentSystemContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITitleRepository, Titlerepository>();

            //Repositories & Services
            return services;
        }
    }
}
