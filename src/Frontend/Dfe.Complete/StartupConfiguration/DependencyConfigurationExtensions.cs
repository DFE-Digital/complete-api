﻿using Dfe.Complete.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Dfe.Complete.StartupConfiguration
{
    public static class DependencyConfigurationExtensions
    {
        public static IServiceCollection AddClientDependencies(this IServiceCollection services)
        {

            services.AddScoped<IErrorService, ErrorService>();
            services.AddScoped<CompleteApiClient, CompleteApiClient>();
            services.AddScoped<IAnalyticsConsentService, AnalyticsConsentService>();

            return services;
        }
    }
}
