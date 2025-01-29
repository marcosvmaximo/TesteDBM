using Teste.Application;
using Teste.Core;
using Teste.Core.Notifications;
using Teste.Domain.Interfaces;
using Teste.Infra;
using Teste.Infra.Repositories;

namespace Teste.API.Configurations;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotifiable, Notifiable>();

        services.AddTransient<IProdutoRepository, ProdutoRepository>();
        services.AddTransient<IProdutoService, ProdutoService>();

        
        return services;
    }
}