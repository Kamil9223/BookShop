using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseAccess.MSSQL_BookShop
{
    public partial class MakeSimplest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_TypeCategories_TypeCategoryId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "TypeCategories");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.RenameColumn(
                name: "TypeCategoryId",
                table: "Books",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_TypeCategoryId",
                table: "Books",
                newName: "IX_Books_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Addresses",
                type: "Char(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<string>(
                name: "FlatNumber",
                table: "Addresses",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Books",
                newName: "TypeCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                newName: "IX_Books_TypeCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Addresses",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Char(6)");

            migrationBuilder.AlterColumn<string>(
                name: "FlatNumber",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeId = table.Column<Guid>(nullable: false),
                    TypeName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "TypeCategories",
                columns: table => new
                {
                    TypeCategoryId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    TypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeCategories", x => x.TypeCategoryId);
                    table.ForeignKey(
                        name: "FK_TypeCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypeCategories_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypeCategories_CategoryId",
                table: "TypeCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeCategories_TypeId",
                table: "TypeCategories",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_TypeCategories_TypeCategoryId",
                table: "Books",
                column: "TypeCategoryId",
                principalTable: "TypeCategories",
                principalColumn: "TypeCategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
