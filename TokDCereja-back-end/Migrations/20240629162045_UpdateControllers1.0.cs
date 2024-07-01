using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokDCereja_back_end.Migrations
{
    /// <inheritdoc />
    public partial class UpdateControllers10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FeedbackId",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Caixas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FerramentaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustoFixo = table.Column<float>(type: "real", nullable: false),
                    CustoUnidade = table.Column<float>(type: "real", nullable: false),
                    TotalVenda = table.Column<float>(type: "real", nullable: false),
                    FundoReserva = table.Column<float>(type: "real", nullable: false),
                    CapitalDeGiro = table.Column<float>(type: "real", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estoques",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ingrediente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantidadeMin = table.Column<int>(type: "int", nullable: false),
                    QuantidadeMax = table.Column<int>(type: "int", nullable: false),
                    QuantidadeAtual = table.Column<int>(type: "int", nullable: false),
                    Medida = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoques", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Avaliacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataFeedback = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormasDePagamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeTitular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoPlano = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    Banco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroParcelas = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasDePagamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Precificacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FerramentaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustoReceita = table.Column<float>(type: "real", nullable: false),
                    MaoDeObra = table.Column<float>(type: "real", nullable: false),
                    CustoFixoUnitario = table.Column<float>(type: "real", nullable: false),
                    CustoVariavelUnitario = table.Column<float>(type: "real", nullable: false),
                    PrecoVenda = table.Column<float>(type: "real", nullable: false),
                    Margemlucro = table.Column<float>(type: "real", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Precificacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabelasNutricionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeReceita = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Carboidrato = table.Column<float>(type: "real", nullable: false),
                    Proteina = table.Column<float>(type: "real", nullable: false),
                    GorduraSaturada = table.Column<float>(type: "real", nullable: false),
                    GorduraTotal = table.Column<float>(type: "real", nullable: false),
                    Sodio = table.Column<float>(type: "real", nullable: false),
                    ValorEnergetico = table.Column<float>(type: "real", nullable: false),
                    Porcao = table.Column<float>(type: "real", nullable: false),
                    DataFabricacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantidade = table.Column<float>(type: "real", nullable: false),
                    Gramas = table.Column<float>(type: "real", nullable: false),
                    TipoProdutoFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FerramentaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelasNutricionais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VitrinesVirtuais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagemProduto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaProduto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FerramentaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitrinesVirtuais", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FeedbackId",
                table: "Usuarios",
                column: "FeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Feedbacks_FeedbackId",
                table: "Usuarios",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Feedbacks_FeedbackId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Caixas");

            migrationBuilder.DropTable(
                name: "Estoques");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "FormasDePagamentos");

            migrationBuilder.DropTable(
                name: "Precificacoes");

            migrationBuilder.DropTable(
                name: "TabelasNutricionais");

            migrationBuilder.DropTable(
                name: "VitrinesVirtuais");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_FeedbackId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "Usuarios");
        }
    }
}
