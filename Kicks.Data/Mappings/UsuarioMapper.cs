using Kicks.Domain.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kicks.Data.Mappings
{
    public static class UsuarioMapper
    {
        public class Usuario : IEntityTypeConfiguration<UsuarioEntity>
        {
            public void Configure(EntityTypeBuilder<UsuarioEntity> b)
            {
                // Chave Primaria
                b.HasKey(i => i.UsuarioId);

                // Outras propriedades da tabela
                b.Property(i => i.PrimeiroNome);
                b.Property(i => i.SegundoNome);
                b.Property(i => i.Email);
                b.Property(i => i.Senha);

                b.Property(i => i.Admin)
                    .HasColumnName("Admin")  // Nome do campo no banco de dados
                    .HasColumnType("varchar(1)") // Tipo de dado no banco de dados
                    .IsRequired();

                b.ToTable("usuario");
            }
        }
    }
}
