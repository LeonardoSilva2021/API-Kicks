using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kicks.Data.Migrations
{
    /// <inheritdoc />
    public partial class banco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    ImagemUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria", x => x.CategoriaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SKU = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QtdEstoque = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<double>(type: "double", nullable: false),
                    PrecoPromocao = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto", x => x.ProdutoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PrimeiroNome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SegundoNome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Admin = table.Column<string>(type: "varchar(1)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.UsuarioId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "produtoImagem",
                columns: table => new
                {
                    ProdutoImagemId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProdutoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ImagemUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProdutoEntityProdutoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtoImagem", x => x.ProdutoImagemId);
                    table.ForeignKey(
                        name: "FK_produtoImagem_produto_ProdutoEntityProdutoId",
                        column: x => x.ProdutoEntityProdutoId,
                        principalTable: "produto",
                        principalColumn: "ProdutoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "produtoTag",
                columns: table => new
                {
                    ProdutoTagId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProdutoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProdutoEntityProdutoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtoTag", x => x.ProdutoTagId);
                    table.ForeignKey(
                        name: "FK_produtoTag_produto_ProdutoEntityProdutoId",
                        column: x => x.ProdutoEntityProdutoId,
                        principalTable: "produto",
                        principalColumn: "ProdutoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_produtoImagem_ProdutoEntityProdutoId",
                table: "produtoImagem",
                column: "ProdutoEntityProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_produtoTag_ProdutoEntityProdutoId",
                table: "produtoTag",
                column: "ProdutoEntityProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categoria");

            migrationBuilder.DropTable(
                name: "produtoImagem");

            migrationBuilder.DropTable(
                name: "produtoTag");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "produto");
        }
    }
}
