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
using Application.Services;
using Persistence.Services;

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

            //IoC Inversion Of Control
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddScoped<ITitleRepository, TitleRepository>();


            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();


            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
            services.AddScoped<IUserOperationClaimService, UserOperationClaimService>();

            //Repositories & Services
            return services;
        }
    }
}
