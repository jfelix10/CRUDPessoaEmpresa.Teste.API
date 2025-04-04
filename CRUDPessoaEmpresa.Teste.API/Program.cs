using CRUDPessoaEmpresa.Teste.API.Configurations;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddApiConfig()
            .ResolveDependencies(builder.Configuration)
            .AddEntityDataBaseConfig(builder.Configuration);

        var app = builder.Build();
        app.UseApiConfig(app.Environment);
        app.Run();
    }
}