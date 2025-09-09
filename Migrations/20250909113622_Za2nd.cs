using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boompa.Migrations
{
    /// <inheritdoc />
    public partial class Za2nd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileDetails_Questions_QuestionId",
                table: "FileDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_FileDetails_SourceMaterials_SourceMaterialId",
                table: "FileDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileDetails",
                table: "FileDetails");

            migrationBuilder.RenameTable(
                name: "FileDetails",
                newName: "SourceFileDetails");

            migrationBuilder.RenameIndex(
                name: "IX_FileDetails_SourceMaterialId",
                table: "SourceFileDetails",
                newName: "IX_SourceFileDetails_SourceMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_FileDetails_QuestionId",
                table: "SourceFileDetails",
                newName: "IX_SourceFileDetails_QuestionId");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "SourceFileDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SourceFileDetails",
                table: "SourceFileDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "QuestionFileDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionFileDetails", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_SourceFileDetails_Questions_QuestionId",
                table: "SourceFileDetails",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SourceFileDetails_SourceMaterials_SourceMaterialId",
                table: "SourceFileDetails",
                column: "SourceMaterialId",
                principalTable: "SourceMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SourceFileDetails_Questions_QuestionId",
                table: "SourceFileDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SourceFileDetails_SourceMaterials_SourceMaterialId",
                table: "SourceFileDetails");

            migrationBuilder.DropTable(
                name: "QuestionFileDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SourceFileDetails",
                table: "SourceFileDetails");

            migrationBuilder.RenameTable(
                name: "SourceFileDetails",
                newName: "FileDetails");

            migrationBuilder.RenameIndex(
                name: "IX_SourceFileDetails_SourceMaterialId",
                table: "FileDetails",
                newName: "IX_FileDetails_SourceMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_SourceFileDetails_QuestionId",
                table: "FileDetails",
                newName: "IX_FileDetails_QuestionId");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "FileDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileDetails",
                table: "FileDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileDetails_Questions_QuestionId",
                table: "FileDetails",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileDetails_SourceMaterials_SourceMaterialId",
                table: "FileDetails",
                column: "SourceMaterialId",
                principalTable: "SourceMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
