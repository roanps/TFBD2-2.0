using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoeMais.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aeroportos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoIATA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeroportos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmpresasAereas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresasAereas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Avioes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prefixo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidade = table.Column<int>(type: "int", nullable: false),
                    EmpresaAereaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avioes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avioes_EmpresasAereas_EmpresaAereaId",
                        column: x => x.EmpresaAereaId,
                        principalTable: "EmpresasAereas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poltronas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AviaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poltronas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poltronas_Avioes_AviaoId",
                        column: x => x.AviaoId,
                        principalTable: "Avioes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Voos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AviaoId = table.Column<int>(type: "int", nullable: false),
                    OrigemId = table.Column<int>(type: "int", nullable: false),
                    DestinoId = table.Column<int>(type: "int", nullable: false),
                    Partida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Chegada = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voos_Aeroportos_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Aeroportos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Voos_Aeroportos_OrigemId",
                        column: x => x.OrigemId,
                        principalTable: "Aeroportos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Voos_Avioes_AviaoId",
                        column: x => x.AviaoId,
                        principalTable: "Avioes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Escalas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VooId = table.Column<int>(type: "int", nullable: false),
                    AeroportoId = table.Column<int>(type: "int", nullable: false),
                    Chegada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Partida = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escalas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Escalas_Aeroportos_AeroportoId",
                        column: x => x.AeroportoId,
                        principalTable: "Aeroportos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Escalas_Voos_VooId",
                        column: x => x.VooId,
                        principalTable: "Voos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoosPoltronas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VooId = table.Column<int>(type: "int", nullable: false),
                    PoltronaId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoosPoltronas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoosPoltronas_Poltronas_PoltronaId",
                        column: x => x.PoltronaId,
                        principalTable: "Poltronas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoosPoltronas_Voos_VooId",
                        column: x => x.VooId,
                        principalTable: "Voos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Passagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    VooPoltronaId = table.Column<int>(type: "int", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passagens_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Passagens_VoosPoltronas_VooPoltronaId",
                        column: x => x.VooPoltronaId,
                        principalTable: "VoosPoltronas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avioes_EmpresaAereaId",
                table: "Avioes",
                column: "EmpresaAereaId");

            migrationBuilder.CreateIndex(
                name: "IX_Escalas_AeroportoId",
                table: "Escalas",
                column: "AeroportoId");

            migrationBuilder.CreateIndex(
                name: "IX_Escalas_VooId",
                table: "Escalas",
                column: "VooId");

            migrationBuilder.CreateIndex(
                name: "IX_Passagens_ClienteId",
                table: "Passagens",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Passagens_VooPoltronaId",
                table: "Passagens",
                column: "VooPoltronaId");

            migrationBuilder.CreateIndex(
                name: "IX_Poltronas_AviaoId",
                table: "Poltronas",
                column: "AviaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Voos_AviaoId",
                table: "Voos",
                column: "AviaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Voos_DestinoId",
                table: "Voos",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Voos_OrigemId",
                table: "Voos",
                column: "OrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_VoosPoltronas_PoltronaId",
                table: "VoosPoltronas",
                column: "PoltronaId");

            migrationBuilder.CreateIndex(
                name: "IX_VoosPoltronas_VooId",
                table: "VoosPoltronas",
                column: "VooId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Escalas");

            migrationBuilder.DropTable(
                name: "Passagens");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "VoosPoltronas");

            migrationBuilder.DropTable(
                name: "Poltronas");

            migrationBuilder.DropTable(
                name: "Voos");

            migrationBuilder.DropTable(
                name: "Aeroportos");

            migrationBuilder.DropTable(
                name: "Avioes");

            migrationBuilder.DropTable(
                name: "EmpresasAereas");
        }
    }
}
