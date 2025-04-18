﻿// <auto-generated />
using System;
using Kicks.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kicks.Data.Migrations
{
    [DbContext(typeof(KicksDataContext))]
    partial class KicksDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Kicks.Domain.Categoria.CategoriaEntity", b =>
                {
                    b.Property<Guid>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ImagemUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("CategoriaId");

                    b.ToTable("categoria", (string)null);
                });

            modelBuilder.Entity("Kicks.Domain.Produto.ProdutoEntity", b =>
                {
                    b.Property<Guid>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<string>("Marca")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<double>("Preco")
                        .HasColumnType("double");

                    b.Property<double>("PrecoPromocao")
                        .HasColumnType("double");

                    b.Property<int>("QtdEstoque")
                        .HasColumnType("int");

                    b.Property<string>("SKU")
                        .HasColumnType("longtext");

                    b.HasKey("ProdutoId");

                    b.ToTable("produto", (string)null);
                });

            modelBuilder.Entity("Kicks.Domain.Produto.ProdutoImagemEntity", b =>
                {
                    b.Property<Guid>("ProdutoImagemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<string>("ImagemUrl")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ProdutoEntityProdutoId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("char(36)");

                    b.HasKey("ProdutoImagemId");

                    b.HasIndex("ProdutoEntityProdutoId");

                    b.ToTable("produtoImagem", (string)null);
                });

            modelBuilder.Entity("Kicks.Domain.Produto.ProdutoTagEntity", b =>
                {
                    b.Property<Guid>("ProdutoTagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ProdutoEntityProdutoId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("char(36)");

                    b.HasKey("ProdutoTagId");

                    b.HasIndex("ProdutoEntityProdutoId");

                    b.ToTable("produtoTag", (string)null);
                });

            modelBuilder.Entity("Kicks.Domain.Usuario.UsuarioEntity", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Admin")
                        .IsRequired()
                        .HasColumnType("varchar(1)")
                        .HasColumnName("Admin");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PrimeiroNome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SegundoNome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UsuarioId");

                    b.ToTable("usuario", (string)null);
                });

            modelBuilder.Entity("Kicks.Domain.Produto.ProdutoImagemEntity", b =>
                {
                    b.HasOne("Kicks.Domain.Produto.ProdutoEntity", null)
                        .WithMany("Imagens")
                        .HasForeignKey("ProdutoEntityProdutoId");
                });

            modelBuilder.Entity("Kicks.Domain.Produto.ProdutoTagEntity", b =>
                {
                    b.HasOne("Kicks.Domain.Produto.ProdutoEntity", null)
                        .WithMany("Tags")
                        .HasForeignKey("ProdutoEntityProdutoId");
                });

            modelBuilder.Entity("Kicks.Domain.Produto.ProdutoEntity", b =>
                {
                    b.Navigation("Imagens");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
