using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace TokDCereja_back_end.Migrations
{
    public partial class PlanosTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Criar tabela Planos
            migrationBuilder.CreateTable(
                name: "Planos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descricao = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos", x => x.Id);
                });

            // Inserir plano padrão
            migrationBuilder.InsertData(
                table: "Planos",
                columns: new[] { "Id", "Nome", "Preco", "Descricao" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "Gratuito", 0m, "Plano gratuito padrão" }
            );

            // Atualizar registros existentes em Usuarios para referenciar o plano padrão
            migrationBuilder.Sql("UPDATE Usuarios SET PlanoId = '00000000-0000-0000-0000-000000000001' WHERE PlanoId IS NULL");

            // Criar índice e chave estrangeira
            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PlanoId",
                table: "Usuarios",
                column: "PlanoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Planos_PlanoId",
                table: "Usuarios",
                column: "PlanoId",
                principalTable: "Planos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Planos_PlanoId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Planos");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_PlanoId",
                table: "Usuarios");
        }
    }
}
