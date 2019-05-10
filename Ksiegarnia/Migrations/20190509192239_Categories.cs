using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ksiegarnia.Migrations
{
    public partial class Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "NumberOfBooks",
                table: "Books",
                newName: "NumberOfPieces");

            migrationBuilder.AddColumn<Guid>(
                name: "TypeCategoryId",
                table: "Books",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    CategoryName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

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
                name: "IX_Books_TypeCategoryId",
                table: "Books",
                column: "TypeCategoryId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_TypeCategories_TypeCategoryId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "TypeCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Books_TypeCategoryId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TypeCategoryId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "NumberOfPieces",
                table: "Books",
                newName: "NumberOfBooks");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Books",
                nullable: false,
                defaultValue: 0);
        }
    }
}
