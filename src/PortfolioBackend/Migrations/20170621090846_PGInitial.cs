using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PortfolioBackend.Migrations
{
    public partial class PGInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cultures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalizedStrings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CultureId = table.Column<int>(nullable: false),
                    DefaultValue = table.Column<string>(type: "text", nullable: true),
                    Key = table.Column<string>(maxLength: 1024, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedStrings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    IsBlocked = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Login = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    OwnerId = table.Column<int>(nullable: true),
                    PasswordHash = table.Column<string>(maxLength: 50, nullable: true),
                    PasswordSalt = table.Column<string>(maxLength: 50, nullable: true),
                    Photo = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission_Locales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CultureId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1023, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LocalizableEntityId = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission_Locales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Locales_Cultures_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Cultures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permission_Locales_Permissions_LocalizableEntityId",
                        column: x => x.LocalizableEntityId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role_Locales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CultureId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LocalizableEntityId = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Locales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Locales_Cultures_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Cultures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Locales_Roles_LocalizableEntityId",
                        column: x => x.LocalizableEntityId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleInPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IsAccessible = table.Column<bool>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleInPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleInPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Locales_CultureId",
                table: "Permission_Locales",
                column: "CultureId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Locales_LocalizableEntityId",
                table: "Permission_Locales",
                column: "LocalizableEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Locales_CultureId",
                table: "Role_Locales",
                column: "CultureId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Locales_LocalizableEntityId",
                table: "Role_Locales",
                column: "LocalizableEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleInPermissions_PermissionId",
                table: "RoleInPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleInPermissions_RoleId",
                table: "RoleInPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login_Email",
                table: "Users",
                columns: new[] { "Login", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInRoles_RoleId",
                table: "UserInRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInRoles_UserId",
                table: "UserInRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalizedStrings");

            migrationBuilder.DropTable(
                name: "Permission_Locales");

            migrationBuilder.DropTable(
                name: "Role_Locales");

            migrationBuilder.DropTable(
                name: "RoleInPermissions");

            migrationBuilder.DropTable(
                name: "UserInRoles");

            migrationBuilder.DropTable(
                name: "Cultures");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
