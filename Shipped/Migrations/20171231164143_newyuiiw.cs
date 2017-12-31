using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace login2.Migrations
{
    public partial class newyuiiw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategorieId",
                table: "OrderHistory",
                type: "int4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_CategorieId",
                table: "OrderHistory",
                column: "CategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_Categories_CategorieId",
                table: "OrderHistory",
                column: "CategorieId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_Categories_CategorieId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_CategorieId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "CategorieId",
                table: "OrderHistory");
        }
    }
}
