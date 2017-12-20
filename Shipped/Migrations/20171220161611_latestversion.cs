using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace login2.Migrations
{
    public partial class latestversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int4", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bool", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bool", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamptz", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bool", nullable: false),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bool", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Aantal = table.Column<int>(type: "int4", nullable: false),
                    Beschrijving = table.Column<string>(type: "text", nullable: true),
                    Merk = table.Column<string>(type: "text", nullable: true),
                    Model_naam = table.Column<string>(type: "text", nullable: true),
                    Order_nummer = table.Column<string>(type: "text", nullable: true),
                    Prijs = table.Column<int>(type: "int4", nullable: false),
                    Product_Id = table.Column<int>(type: "int4", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    User_Id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistory", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Aantal = table.Column<int>(type: "int4", nullable: false),
                    Aantal_gekocht = table.Column<int>(type: "int4", nullable: false),
                    Aantal_rotors = table.Column<int>(type: "int4", nullable: false),
                    Afbeelding = table.Column<string>(type: "text", nullable: true),
                    CategorieId = table.Column<int>(type: "int4", nullable: false),
                    Grootte = table.Column<int>(type: "int4", nullable: false),
                    Kleur = table.Column<string>(type: "text", nullable: true),
                    Merk = table.Column<string>(type: "text", nullable: true),
                    Naam = table.Column<string>(type: "text", nullable: true),
                    Prijs = table.Column<int>(type: "int4", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drones_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fotocameras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Aantal = table.Column<int>(type: "int4", nullable: false),
                    Aantal_gekocht = table.Column<int>(type: "int4", nullable: false),
                    Afbeelding = table.Column<string>(type: "text", nullable: true),
                    CategorieId = table.Column<int>(type: "int4", nullable: false),
                    Flits = table.Column<string>(type: "text", nullable: true),
                    Kleur = table.Column<string>(type: "text", nullable: true),
                    Max_Bereik = table.Column<int>(type: "int4", nullable: false),
                    MegaPixels = table.Column<int>(type: "int4", nullable: false),
                    Merk = table.Column<string>(type: "text", nullable: true),
                    Min_Bereik = table.Column<int>(type: "int4", nullable: false),
                    Naam = table.Column<string>(type: "text", nullable: true),
                    Prijs = table.Column<int>(type: "int4", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotocameras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fotocameras_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Horloges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Aantal = table.Column<int>(type: "int4", nullable: false),
                    Aantal_gekocht = table.Column<int>(type: "int4", nullable: false),
                    Afbeelding = table.Column<string>(type: "text", nullable: true),
                    CategorieId = table.Column<int>(type: "int4", nullable: false),
                    Geslacht = table.Column<string>(type: "text", nullable: true),
                    Grootte = table.Column<int>(type: "int4", nullable: false),
                    Kleur = table.Column<string>(type: "text", nullable: true),
                    Materiaal = table.Column<string>(type: "text", nullable: true),
                    Merk = table.Column<string>(type: "text", nullable: true),
                    Naam = table.Column<string>(type: "text", nullable: true),
                    Prijs = table.Column<int>(type: "int4", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horloges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horloges_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kabels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Aantal = table.Column<int>(type: "int4", nullable: false),
                    Aantal_gekocht = table.Column<int>(type: "int4", nullable: false),
                    Afbeelding = table.Column<string>(type: "text", nullable: true),
                    CategorieId = table.Column<int>(type: "int4", nullable: false),
                    Kleur = table.Column<string>(type: "text", nullable: true),
                    Lengte = table.Column<int>(type: "int4", nullable: false),
                    Merk = table.Column<string>(type: "text", nullable: true),
                    Naam = table.Column<string>(type: "text", nullable: true),
                    Prijs = table.Column<int>(type: "int4", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kabels_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schoenen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Aantal = table.Column<int>(type: "int4", nullable: false),
                    Aantal_gekocht = table.Column<int>(type: "int4", nullable: false),
                    Afbeelding = table.Column<string>(type: "text", nullable: true),
                    CategorieId = table.Column<int>(type: "int4", nullable: false),
                    Geslacht = table.Column<string>(type: "text", nullable: true),
                    Kleur = table.Column<string>(type: "text", nullable: true),
                    Maat = table.Column<int>(type: "int4", nullable: false),
                    Materiaal = table.Column<string>(type: "text", nullable: true),
                    Merk = table.Column<string>(type: "text", nullable: true),
                    Naam = table.Column<string>(type: "text", nullable: true),
                    Prijs = table.Column<int>(type: "int4", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schoenen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schoenen_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    Opslagcapaciteit = table.Column<int>(type: "int4", nullable: false),
                    Opties = table.Column<string>(type: "text", nullable: true),
                    Prijs = table.Column<int>(type: "int4", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true)
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drones_CategorieId",
                table: "Drones",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Fotocameras_CategorieId",
                table: "Fotocameras",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Horloges_CategorieId",
                table: "Horloges",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Kabels_CategorieId",
                table: "Kabels",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Schoenen_CategorieId",
                table: "Schoenen",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Spelcomputers_CategorieId",
                table: "Spelcomputers",
                column: "CategorieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Drones");

            migrationBuilder.DropTable(
                name: "Fotocameras");

            migrationBuilder.DropTable(
                name: "Horloges");

            migrationBuilder.DropTable(
                name: "Kabels");

            migrationBuilder.DropTable(
                name: "OrderHistory");

            migrationBuilder.DropTable(
                name: "Schoenen");

            migrationBuilder.DropTable(
                name: "Spelcomputers");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
