using CRUDPessoaEmpresa.Teste.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDPessoaEmpresa.Teste.API.Infra.Context;

public class PFPJDBContext : DbContext
{
    public PFPJDBContext(DbContextOptions<PFPJDBContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<EventEntity> EventStore { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.CpfCnpj)
            .IsUnique();

        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.Email)
            .IsUnique();

        modelBuilder.Entity<EventEntity>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<EventEntity>()
            .Property(e => e.Data)
            .HasColumnType("nvarchar(max)");

        base.OnModelCreating(modelBuilder);
    }
}
