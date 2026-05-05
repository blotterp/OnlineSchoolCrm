using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineSchoolCrm.Persistence.Database;
using OnlineSchoolCrm.Application.Abstractions.Data;
using OnlineSchoolCrm.Persistence.Repositories;

namespace OnlineSchoolCrm.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Connection string 'DefaultConnection' isn`t configured ");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<ILeadRepository, LeadRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}

