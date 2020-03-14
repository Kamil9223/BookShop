using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ksiegarnia.Migrations
{
    public partial class RefreshTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoggedUsers",
                columns: table => new
                {
                    RefreshToken = table.Column<Guid>(nullable: false),
                    JwtId = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggedUsers", x => x.RefreshToken);
                    table.ForeignKey(
                        name: "FK_LoggedUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoggedUsers_UserId",
                table: "LoggedUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoggedUsers");
        }
    }
}
