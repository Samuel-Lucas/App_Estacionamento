using Estacionamento.Domain.Interfaces;
using Estacionamento.Infrastructure.Context;
using Estacionamento.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estacionamento.CrossCutting.DependenciesApp;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration
                              .GetConnectionString("SqLite");

        services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(connectionString));

        services.AddScoped<IPessoaRepository, PessoaRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

        return services;
    }
}