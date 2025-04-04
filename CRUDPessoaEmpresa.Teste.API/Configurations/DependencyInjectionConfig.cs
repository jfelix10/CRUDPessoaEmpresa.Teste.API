using CRUDPessoaEmpresa.Teste.API.Application.Clients.Commands;
using CRUDPessoaEmpresa.Teste.API.Application.Clients.Validators;
using CRUDPessoaEmpresa.Teste.API.Infra.Repositories.Events;
using CRUDPessoaEmpresa.Teste.API.Interfaces.Repositories.Events;
using FluentValidation;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace CRUDPessoaEmpresa.Teste.API.Configurations;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbConnection>((sp) => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IValidator<CreateClienteCommand>, CreateClienteCommandValidator>();
        services.AddScoped<IEventStore, EventStore>();

        return services;
    }
}
