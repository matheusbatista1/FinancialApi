using FinancialApi.Domain.ExternalServices.Compliance.Interfaces;
using FinancialApi.Domain.Interfaces.Repositories;
using FinancialApi.Domain.Interfaces.Services;
using FinancialApi.Infrastructure.Data;
using FinancialApi.Infrastructure.Data.Repositories;
using FinancialApi.Infrastructure.ExternalServices.Compliance.Services;
using FinancialApi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace FinancialApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A variável de ambiente ConnectionStrings__DefaultConnection não está definida.");
        }

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

        // Repositórios
        services.AddScoped<IPeopleRepository, PeopleRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        // Services
        services.AddSingleton<ITokenService, TokenService>();

        // Refit – ExternalServices
        var complianceApiUrl = Environment.GetEnvironmentVariable("ComplianceApi__BaseUrl");
        if (string.IsNullOrEmpty(complianceApiUrl))
        {
            throw new InvalidOperationException("A variável de ambiente ComplianceApi__BaseUrl não está definida.");
        }

        services.AddRefitClient<IComplianceValidationService>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(complianceApiUrl));

        services.AddRefitClient<IComplianceAuthService>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(complianceApiUrl));

        // External services
        services.AddScoped<IComplianceTokenProvider, ComplianceTokenProvider>();

        return services;
    }
}