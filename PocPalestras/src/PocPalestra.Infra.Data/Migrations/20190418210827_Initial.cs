using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PocPalestra.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    Cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Palestras",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    DescricaoAbrev = table.Column<string>(type: "varchar(150)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(max)", nullable: true),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    Gratuito = table.Column<bool>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Online = table.Column<bool>(nullable: false),
                    NomeEmpresa = table.Column<string>(type: "varchar(150)", nullable: false),
                    Excluido = table.Column<bool>(nullable: false),
                    CategoriaId = table.Column<Guid>(nullable: true),
                    EnderecoId = table.Column<Guid>(nullable: true),
                    OrganizadorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palestras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Palestras_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Palestras_Organizadores_OrganizadorId",
                        column: x => x.OrganizadorId,
                        principalTable: "Organizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Numero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CEP = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PalestraId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Palestras_PalestraId",
                        column: x => x.PalestraId,
                        principalTable: "Palestras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_PalestraId",
                table: "Enderecos",
                column: "PalestraId",
                unique: true,
                filter: "[PalestraId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Palestras_CategoriaId",
                table: "Palestras",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Palestras_OrganizadorId",
                table: "Palestras",
                column: "OrganizadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Palestras");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Organizadores");
        }
    }
}
