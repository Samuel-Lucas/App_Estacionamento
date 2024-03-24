using Estacionamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Infrastructure.Configuration;

public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.HasKey(k => k.IdVeiculo);
        builder.Property(p => p.Marca).HasMaxLength(20).IsRequired();
        builder.Property(p => p.Modelo).HasMaxLength(20).IsRequired();
        builder.Property(p => p.Cor).HasMaxLength(20).IsRequired();
        builder.Property(p => p.Placa).HasMaxLength(20).IsRequired();
        builder.Property(f => f.IdPessoa).IsRequired();
    }
}