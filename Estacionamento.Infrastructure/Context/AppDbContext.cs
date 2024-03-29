using Estacionamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pessoa>? Pessoas { get; set; }
    public DbSet<Veiculo>? Veiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Veiculo>()
            .HasOne(v => v.Pessoa)
            .WithMany(p => p.Veiculos)
            .HasForeignKey(v => v.IdPessoa);
            
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}