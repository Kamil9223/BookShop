using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ksiegarnia.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Addresses_Users_UserId1",
            //    table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            //migrationBuilder.DropIndex(
            //    name: "IX_Addresses_UserId1",
            //    table: "Addresses");

            //migrationBuilder.DropColumn(
            //    name: "UserId1",
            //    table: "Addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Addresses",
                nullable: false,
                defaultValue: new Guid());

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Addresses_Users_UserId",
            //    table: "Addresses",
            //    column: "UserId",
            //    principalTable: "Users",
            //    principalColumn: "UserId",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_UserId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId1",
                table: "Addresses",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_UserId1",
                table: "Addresses",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
