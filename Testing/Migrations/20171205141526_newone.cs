using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace login2.Migrations
{
    public partial class newone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ShoppingCart_ShoppingCartId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drones_ShoppingCart_ShoppingCartId",
                table: "Drones");

            migrationBuilder.DropForeignKey(
                name: "FK_Fotocameras_ShoppingCart_ShoppingCartId",
                table: "Fotocameras");

            migrationBuilder.DropForeignKey(
                name: "FK_Horloges_ShoppingCart_ShoppingCartId",
                table: "Horloges");

            migrationBuilder.DropForeignKey(
                name: "FK_Kabels_ShoppingCart_ShoppingCartId",
                table: "Kabels");

            migrationBuilder.DropForeignKey(
                name: "FK_Schoenen_ShoppingCart_ShoppingCartId",
                table: "Schoenen");

            migrationBuilder.DropForeignKey(
                name: "FK_Spelcomputers_ShoppingCart_ShoppingCartId",
                table: "Spelcomputers");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_Spelcomputers_ShoppingCartId",
                table: "Spelcomputers");

            migrationBuilder.DropIndex(
                name: "IX_Schoenen_ShoppingCartId",
                table: "Schoenen");

            migrationBuilder.DropIndex(
                name: "IX_Kabels_ShoppingCartId",
                table: "Kabels");

            migrationBuilder.DropIndex(
                name: "IX_Horloges_ShoppingCartId",
                table: "Horloges");

            migrationBuilder.DropIndex(
                name: "IX_Fotocameras_ShoppingCartId",
                table: "Fotocameras");

            migrationBuilder.DropIndex(
                name: "IX_Drones_ShoppingCartId",
                table: "Drones");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ShoppingCartId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Spelcomputers");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Schoenen");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Kabels");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Horloges");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Fotocameras");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Cart",
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
                    table.PrimaryKey("PK_Cart", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Spelcomputers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Schoenen",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Kabels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Horloges",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Fotocameras",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Drones",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spelcomputers_ShoppingCartId",
                table: "Spelcomputers",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Schoenen_ShoppingCartId",
                table: "Schoenen",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Kabels_ShoppingCartId",
                table: "Kabels",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Horloges_ShoppingCartId",
                table: "Horloges",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Fotocameras_ShoppingCartId",
                table: "Fotocameras",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Drones_ShoppingCartId",
                table: "Drones",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ShoppingCartId",
                table: "AspNetUsers",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ShoppingCart_ShoppingCartId",
                table: "AspNetUsers",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drones_ShoppingCart_ShoppingCartId",
                table: "Drones",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fotocameras_ShoppingCart_ShoppingCartId",
                table: "Fotocameras",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Horloges_ShoppingCart_ShoppingCartId",
                table: "Horloges",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kabels_ShoppingCart_ShoppingCartId",
                table: "Kabels",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schoenen_ShoppingCart_ShoppingCartId",
                table: "Schoenen",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Spelcomputers_ShoppingCart_ShoppingCartId",
                table: "Spelcomputers",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
