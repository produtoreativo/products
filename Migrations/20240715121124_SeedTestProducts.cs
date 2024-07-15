using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Produtos.Data.Migrations
{
    public partial class SeedTestProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                migrationBuilder.InsertData(
                    table: "Produtos",
                    columns: new[] { "Id", "Nome", "Preco", "Categoria" },
                    values: new object[,]
                    {
                        { Guid.NewGuid(), "Coca Cola 600ml", 4.50m, "Bebidas" },
                        { Guid.NewGuid(), "Bolacha Trakinas", 3.00m, "Snacks" },
                        { Guid.NewGuid(), "Suco de Laranja 1L", 7.00m, "Bebidas" },
                        { Guid.NewGuid(), "Chocolate Bis", 8.50m, "Snacks" }
                    });
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                migrationBuilder.Sql("DELETE FROM Produtos WHERE Nome IN ('Coca Cola 600ml', 'Bolacha Trakinas', 'Suco de Laranja 1L', 'Chocolate Bis')");
            }
        }
    }
}