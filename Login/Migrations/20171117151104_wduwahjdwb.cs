using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace login2.Migrations
{
    public partial class wduwahjdwb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Intvalue",
                table: "Specs",
                type: "int4",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Intvalue",
                table: "Specs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int4",
                oldNullable: true);
        }
    }
}
