using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mottracker.Migrations
{
    /// <inheritdoc />
    public partial class FIAPOracleDBMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MT_PATIO",
                columns: table => new
                {
                    IdPatio = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomePatio = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MotosTotaisPatio = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MotosDisponiveisPatio = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataPatio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MT_PATIO", x => x.IdPatio);
                });

            migrationBuilder.CreateTable(
                name: "MT_PERMISSAO",
                columns: table => new
                {
                    IdPermissao = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomePermissao = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MT_PERMISSAO", x => x.IdPermissao);
                });

            migrationBuilder.CreateTable(
                name: "MT_USUARIO",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeUsuario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CPFUsuario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SenhaUsuario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CNHUsuario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EmailUsuario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TokenUsuario = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataNascimentoUsuario = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CriadoEmUsuario = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MT_USUARIO", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "MT_CAMERA",
                columns: table => new
                {
                    IdCamera = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeCamera = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IpCamera = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PosX = table.Column<float>(type: "BINARY_FLOAT", nullable: true),
                    PosY = table.Column<float>(type: "BINARY_FLOAT", nullable: true),
                    PatioId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MT_CAMERA", x => x.IdCamera);
                    table.ForeignKey(
                        name: "FK_MT_CAMERA_MT_PATIO_PatioId",
                        column: x => x.PatioId,
                        principalTable: "MT_PATIO",
                        principalColumn: "IdPatio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MT_ENDERECO",
                columns: table => new
                {
                    IdEndereco = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Logradouro = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    Numero = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    CEP = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    Referencia = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    PatioEnderecoId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MT_ENDERECO", x => x.IdEndereco);
                    table.ForeignKey(
                        name: "FK_MT_ENDERECO_MT_PATIO_PatioEnderecoId",
                        column: x => x.PatioEnderecoId,
                        principalTable: "MT_PATIO",
                        principalColumn: "IdPatio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MT_LAYOUT_PATIO",
                columns: table => new
                {
                    IdLayoutPatio = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Largura = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    Comprimento = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    Altura = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    PatioLayoutPatioId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MT_LAYOUT_PATIO", x => x.IdLayoutPatio);
                    table.ForeignKey(
                        name: "FK_MT_LAYOUT_PATIO_MT_PATIO_PatioLayoutPatioId",
                        column: x => x.PatioLayoutPatioId,
                        principalTable: "MT_PATIO",
                        principalColumn: "IdPatio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MT_CONTRATO",
                columns: table => new
                {
                    IdContrato = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClausulasContrato = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataDeEntradaContrato = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    HorarioDeDevolucaoContrato = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataDeExpiracaoContrato = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    RenovacaoAutomaticaContrato = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DataUltimaRenovacaoContrato = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    NumeroRenovacoesContrato = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AtivoContrato = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ValorToralContrato = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    QuantidadeParcelas = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UsuarioContratoId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MT_CONTRATO", x => x.IdContrato);
                    table.ForeignKey(
                        name: "FK_MT_CONTRATO_MT_USUARIO_UsuarioContratoId",
                        column: x => x.UsuarioContratoId,
                        principalTable: "MT_USUARIO",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MT_TELEFONE",
                columns: table => new
                {
                    IdTelefone = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Numero = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MT_TELEFONE", x => x.IdTelefone);
                    table.ForeignKey(
                        name: "FK_MT_TELEFONE_MT_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "MT_USUARIO",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MT_USUARIO_PERMISSAO",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PermissaoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Papel = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MT_USUARIO_PERMISSAO", x => new { x.UsuarioId, x.PermissaoId });
                    table.ForeignKey(
                        name: "FK_MT_USUARIO_PERMISSAO_MT_PERMISSAO_PermissaoId",
                        column: x => x.PermissaoId,
                        principalTable: "MT_PERMISSAO",
                        principalColumn: "IdPermissao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MT_USUARIO_PERMISSAO_MT_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "MT_USUARIO",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MT_QRCODE_PONTO",
                columns: table => new
                {
                    IdQrCodePonto = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdentificadorQrCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PosX = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    PosY = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    LayoutPatioId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MT_QRCODE_PONTO", x => x.IdQrCodePonto);
                    table.ForeignKey(
                        name: "FK_MT_QRCODE_PONTO_MT_LAYOUT_PATIO_LayoutPatioId",
                        column: x => x.LayoutPatioId,
                        principalTable: "MT_LAYOUT_PATIO",
                        principalColumn: "IdLayoutPatio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MT_MOTO",
                columns: table => new
                {
                    IdMoto = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PlacaMoto = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ModeloMoto = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AnoMoto = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdentificadorMoto = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    QuilometragemMoto = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EstadoMoto = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CondicoesMoto = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ContratoMotoId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    MotoPatioAtualId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    MotoPatioOrigemId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MT_MOTO", x => x.IdMoto);
                    table.ForeignKey(
                        name: "FK_MT_MOTO_MT_CONTRATO_ContratoMotoId",
                        column: x => x.ContratoMotoId,
                        principalTable: "MT_CONTRATO",
                        principalColumn: "IdContrato");
                    table.ForeignKey(
                        name: "FK_MT_MOTO_MT_PATIO_MotoPatioAtualId",
                        column: x => x.MotoPatioAtualId,
                        principalTable: "MT_PATIO",
                        principalColumn: "IdPatio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MT_CAMERA_PatioId",
                table: "MT_CAMERA",
                column: "PatioId");

            migrationBuilder.CreateIndex(
                name: "IX_MT_CONTRATO_UsuarioContratoId",
                table: "MT_CONTRATO",
                column: "UsuarioContratoId",
                unique: true,
                filter: "\"UsuarioContratoId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MT_ENDERECO_PatioEnderecoId",
                table: "MT_ENDERECO",
                column: "PatioEnderecoId",
                unique: true,
                filter: "\"PatioEnderecoId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MT_LAYOUT_PATIO_PatioLayoutPatioId",
                table: "MT_LAYOUT_PATIO",
                column: "PatioLayoutPatioId",
                unique: true,
                filter: "\"PatioLayoutPatioId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MT_MOTO_ContratoMotoId",
                table: "MT_MOTO",
                column: "ContratoMotoId",
                unique: true,
                filter: "\"ContratoMotoId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MT_MOTO_MotoPatioAtualId",
                table: "MT_MOTO",
                column: "MotoPatioAtualId");

            migrationBuilder.CreateIndex(
                name: "IX_MT_QRCODE_PONTO_LayoutPatioId",
                table: "MT_QRCODE_PONTO",
                column: "LayoutPatioId");

            migrationBuilder.CreateIndex(
                name: "IX_MT_TELEFONE_UsuarioId",
                table: "MT_TELEFONE",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MT_USUARIO_PERMISSAO_PermissaoId",
                table: "MT_USUARIO_PERMISSAO",
                column: "PermissaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MT_CAMERA");

            migrationBuilder.DropTable(
                name: "MT_ENDERECO");

            migrationBuilder.DropTable(
                name: "MT_MOTO");

            migrationBuilder.DropTable(
                name: "MT_QRCODE_PONTO");

            migrationBuilder.DropTable(
                name: "MT_TELEFONE");

            migrationBuilder.DropTable(
                name: "MT_USUARIO_PERMISSAO");

            migrationBuilder.DropTable(
                name: "MT_CONTRATO");

            migrationBuilder.DropTable(
                name: "MT_LAYOUT_PATIO");

            migrationBuilder.DropTable(
                name: "MT_PERMISSAO");

            migrationBuilder.DropTable(
                name: "MT_USUARIO");

            migrationBuilder.DropTable(
                name: "MT_PATIO");
        }
    }
}
