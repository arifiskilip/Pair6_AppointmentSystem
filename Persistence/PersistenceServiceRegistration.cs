﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Db

            //services.AddDbContext<TaskManagerContext>(opt =>
            //{
            //    opt.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            //});

            //Repositories & Services
            return services;
        }
    }
}
