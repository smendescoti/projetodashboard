using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Mappings
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeConta)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.DataConta)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(c => c.ValorConta)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.Observacoes)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(c => c.Categoria)
                .IsRequired();

            builder.Property(c => c.Tipo)
               .IsRequired();

            builder.Property(c => c.FormaDePagamento)
               .IsRequired();
        }
    }
}
