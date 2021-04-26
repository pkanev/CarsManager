using System.Reflection;
using AutoMapper;
using CarsManager.Application.Common.Behaviours;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Application.Common.Utils;
using CarsManager.Application.Issues;
using CarsManager.Application.Issues.Queries.GetAllIssues;
using CarsManager.Application.Liabilities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarsManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorisationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddSingleton<IImageManager, ImageManager>();
            services.AddSingleton<ILiabilityUtils, LiabilityUtils>();
            services.AddSingleton<IUrlHelper, UrlHelper>();
            services.AddSingleton<IIssuesHelper, IssuesHelper>();
            services.AddTransient<IRepairIssuesGetter, RepairIssuesGetter>();
            services.AddTransient<ILiabilityIssuesGetter, LiabilityIssuesGetter>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
