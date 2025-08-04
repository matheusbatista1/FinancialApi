using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinancialApi.API.Extensions;

public static class JwtAuthenticationExtension
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, JwtBearerOptionsPostConfig>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer();

        services.ConfigureOptions<JwtBearerOptionsPostConfig>();

        return services;
    }

    private class JwtBearerOptionsPostConfig : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly ILogger<JwtBearerOptionsPostConfig> _logger;

        public JwtBearerOptionsPostConfig(ILogger<JwtBearerOptionsPostConfig> logger)
        {
            _logger = logger;
        }

        public void PostConfigure(string? name, JwtBearerOptions options)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JwtSettings__JwtKey");
            var jwtIssuer = Environment.GetEnvironmentVariable("JwtSettings__JwtIssuer");
            var jwtAudience = Environment.GetEnvironmentVariable("JwtSettings__JwtAudience");

            if (string.IsNullOrEmpty(jwtKey))
            {
                _logger.LogError("JwtSettings__JwtKey está vazio ou não foi configurado.");
                throw new InvalidOperationException("A variável de ambiente JwtSettings__JwtKey é obrigatória e não pode ser vazia.");
            }

            if (string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
            {
                _logger.LogError("JwtSettings__JwtIssuer ou JwtSettings__JwtAudience está vazio ou não foi configurado.");
                throw new InvalidOperationException("As variáveis de ambiente JwtSettings__JwtIssuer e JwtSettings__JwtAudience são obrigatórias.");
            }

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    _logger.LogError(context.Exception, "Falha na autenticação JWT");
                    return Task.CompletedTask;
                }
            };
        }
    }
}