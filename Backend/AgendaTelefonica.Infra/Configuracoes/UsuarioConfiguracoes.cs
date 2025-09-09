using AgendaTelefonica.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaTelefonica.Infra.Configuracoes;

public class UsuarioConfiguracoes : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("at_usuario");

        builder.HasKey(usuario => usuario.Id);
    }
}