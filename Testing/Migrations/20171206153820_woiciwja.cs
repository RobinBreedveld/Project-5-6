using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace login2.Migrations
{
    public partial class woiciwja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "opslagcapaciteit",
                table: "Spelcomputers",
                newName: "Opslagcapaciteit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Opslagcapaciteit",
                table: "Spelcomputers",
                newName: "opslagcapaciteit");
        }
    }
}
