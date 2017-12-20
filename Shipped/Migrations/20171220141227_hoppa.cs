using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace login2.Migrations
{
    public partial class hoppa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Aantal = table.Column<int>(type: "int4", nullable: false),
                    Beschrijving = table.Column<string>(type: "text", nullable: true),
                    Merk = table.Column<string>(type: "text", nullable: true),
                    Model_naam = table.Column<string>(type: "text", nullable: true),
                    Order_nummer = table.Column<int>(type: "int4", nullable: false),
                    Prijs = table.Column<int>(type: "int4", nullable: false),
                    Product_Id = table.Column<int>(type: "int4", nullable: false),
                    User_Id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wishlist");
        }
    }
}
