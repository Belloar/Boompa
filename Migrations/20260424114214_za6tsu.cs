using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boompa.Migrations
{
    /// <inheritdoc />
    public partial class za6tsu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SourceMaterials_Categories_CategoryId",
                table: "SourceMaterials");

            migrationBuilder.DropIndex(
                name: "IX_SourceMaterials_CategoryId",
                table: "SourceMaterials");

            migrationBuilder.CreateTable(
                name: "CategorySourceMaterial",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SourceMaterialsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySourceMaterial", x => new { x.CategoriesId, x.SourceMaterialsId });
                    table.ForeignKey(
                        name: "FK_CategorySourceMaterial_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorySourceMaterial_SourceMaterials_SourceMaterialsId",
                        column: x => x.SourceMaterialsId,
                        principalTable: "SourceMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CategorySourceMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SourceMaterialId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySourceMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorySourceMaterials_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorySourceMaterials_SourceMaterials_SourceMaterialId",
                        column: x => x.SourceMaterialId,
                        principalTable: "SourceMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySourceMaterial_SourceMaterialsId",
                table: "CategorySourceMaterial",
                column: "SourceMaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySourceMaterials_CategoryId",
                table: "CategorySourceMaterials",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySourceMaterials_SourceMaterialId",
                table: "CategorySourceMaterials",
                column: "SourceMaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategorySourceMaterial");

            migrationBuilder.DropTable(
                name: "CategorySourceMaterials");

            migrationBuilder.CreateIndex(
                name: "IX_SourceMaterials_CategoryId",
                table: "SourceMaterials",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SourceMaterials_Categories_CategoryId",
                table: "SourceMaterials",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
