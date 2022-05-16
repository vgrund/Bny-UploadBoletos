using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bny.UploadBoletos.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoCliente = table.Column<string>(type: "varchar(100)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(100)", nullable: false),
                    IdBolsa = table.Column<string>(type: "varchar(100)", nullable: false),
                    CodigoAtivo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Corretora = table.Column<string>(type: "varchar(100)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorFinanceiro = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorDesconto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StatusBoleto = table.Column<string>(type: "varchar(100)", nullable: false),
                    Mensagem = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacoes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operacoes");
        }
    }
}
