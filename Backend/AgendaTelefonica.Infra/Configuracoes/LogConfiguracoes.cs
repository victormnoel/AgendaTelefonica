using AgendaTelefonica.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaTelefonica.Infra.Configuracoes;

public class LogConfiguracoes: IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.ToTable("at_log");

        builder.HasKey(log => log.Id);
    }
}