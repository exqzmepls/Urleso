﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Urleso.Application.Abstractions.Data;
using Urleso.Application.Abstractions.Data.Repositories;
using Urleso.Infrastructure.Data.Repositories;
using Urleso.Persistence;

namespace Urleso.Infrastructure.Data;

internal static class DependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddDatabase();
        services.TryAddScoped<IUnitOfWork, UnitOfWork>();
        services.TryAddScoped<IShortenedUrlRepository, ShortenedUrlRepository>();

        return services;
    }
}