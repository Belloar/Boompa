using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boompa.Migrations
{
    public partial class za6th : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SourceMaterials_Categories_CategoryId",
                table: "SourceMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_SourceMaterials_Questions_QuestionId",
                table: "SourceMaterials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SourceMaterials",
                table: "SourceMaterials");

            migrationBuilder.DropIndex(
                name: "IX_SourceMaterials_QuestionId",
                table: "SourceMaterials");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "SourceMaterials");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "SourceMaterials");

            migrationBuilder.RenameTable(
                name: "SourceMaterials",
                newName: "Articles");

            migrationBuilder.RenameColumn(
                name: "MediaType",
                table: "Articles",
                newName: "Mediatype");

            migrationBuilder.RenameIndex(
                name: "IX_SourceMaterials_CategoryId",
                table: "Articles",
                newName: "IX_Articles_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleName",
                table: "Questions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceMaterialId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articles",
                table: "Articles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Audios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsConvo = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Mediatype = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Path = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Mediatype = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Path = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    PhotoId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionPhotos_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionPhotos_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 17, 22, 8, 9, 619, DateTimeKind.Utc).AddTicks(8133));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 17, 22, 8, 9, 619, DateTimeKind.Utc).AddTicks(8137));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 17, 22, 8, 9, 619, DateTimeKind.Utc).AddTicks(8138));

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ArticleId",
                table: "Questions",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPhotos_PhotoId",
                table: "QuestionPhotos",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPhotos_QuestionId",
                table: "QuestionPhotos",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Articles_ArticleId",
                table: "Questions",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Articles_ArticleId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "Audios");

            migrationBuilder.DropTable(
                name: "QuestionPhotos");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ArticleId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articles",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "SourceMaterialId",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Articles",
                newName: "SourceMaterials");

            migrationBuilder.RenameColumn(
                name: "Mediatype",
                table: "SourceMaterials",
                newName: "MediaType");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_CategoryId",
                table: "SourceMaterials",
                newName: "IX_SourceMaterials_CategoryId");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "ArticleName",
                keyValue: null,
                column: "ArticleName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleName",
                table: "Questions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "SourceMaterials",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "SourceMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SourceMaterials",
                table: "SourceMaterials",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 17, 4, 10, 54, 22, DateTimeKind.Utc).AddTicks(7072));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 17, 4, 10, 54, 22, DateTimeKind.Utc).AddTicks(7075));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 17, 4, 10, 54, 22, DateTimeKind.Utc).AddTicks(7076));

            migrationBuilder.CreateIndex(
                name: "IX_SourceMaterials_QuestionId",
                table: "SourceMaterials",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SourceMaterials_Categories_CategoryId",
                table: "SourceMaterials",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SourceMaterials_Questions_QuestionId",
                table: "SourceMaterials",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
