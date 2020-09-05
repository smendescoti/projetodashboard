using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NomeConta = table.Column<string>(maxLength: 150, nullable: false),
                    DataConta = table.Column<DateTime>(type: "date", nullable: false),
                    ValorConta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observacoes = table.Column<string>(maxLength: 500, nullable: false),
                    Categoria = table.Column<int>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    FormaDePagamento = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contas");
        }
    }
}
