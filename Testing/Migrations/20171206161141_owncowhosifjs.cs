using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace login2.Migrations
{
    public partial class owncowhosifjs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Grootte",
                table: "Horloges",
                type: "int4",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grootte",
                table: "Horloges");
        }
    }
}
