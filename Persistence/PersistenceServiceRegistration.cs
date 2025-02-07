﻿using Application.Repositories;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
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
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUserRepository, UserRepository>();


            services.AddScoped<ITitleRepository, TitleRepository>();

            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientService, PatientService>();

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();

            services.AddScoped<IDoctorScheduleRepository, DoctorScheduleRepository>();

            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
            services.AddScoped<IUserOperationClaimService, UserOperationClaimService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAppointmentIntervalRepository, AppointmentIntervalRepository>();
            services.AddScoped<IAppointmentIntervalService, AppointmentIntervalService>();

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAppointmentService, AppointmentService>();

            services.AddScoped<IFeedbackRepository,FeedbackRepository>();

            services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();

            services.AddScoped<IReportRepository,ReportRepository>();

            //Repositories & Services
            return services;
        }
    }
}
