using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace login2.Migrations
{
    public partial class woidc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consoles");

            migrationBuilder.CreateTable(
                name: "Spelcomputers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Aantal = table.Column<int>(type: "int4", nullable: false),
                    Aantal_gekocht = table.Column<int>(type: "int4", nullable: false),
                    Afbeelding = table.Column<string>(type: "text", nullable: true),
                    CategorieId = table.Column<int>(type: "int4", nullable: false),
                    Kleur = table.Column<string>(type: "text", nullable: true),
                    Merk = table.Column<string>(type: "text", nullable: true),
                    Naam = table.Column<string>(type: "text", nullable: true),
                    Opties = table.Column<string>(type: "text", nullable: true),
                    Prijs = table.Column<int>(type: "int4", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Opslagcapaciteit = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spelcomputers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spelcomputers_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spelcomputers_CategorieId",
                table: "Spelcomputers",
                column: "CategorieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spelcomputers");

            migrationBuilder.CreateTable(
                name: "Consoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Aantal = table.Column<int>(nullable: false),
                    Aantal_gekocht = table.Column<int>(nullable: false),
                    Afbeelding = table.Column<string>(nullable: true),
                    CategorieId = table.Column<int>(nullable: false),
                    Kleur = table.Column<string>(nullable: true),
                    Merk = table.Column<string>(nullable: true),
                    Naam = table.Column<string>(nullable: true),
                    Opties = table.Column<string>(nullable: true),
                    Prijs = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Opslagcapaciteit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consoles_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consoles_CategorieId",
                table: "Consoles",
                column: "CategorieId");
        }
    }
}
