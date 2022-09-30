using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boompa.Migrations
{
    public partial class za2nd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrator_Users_UserId",
                table: "Administrator");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrator",
                table: "Administrator");

            migrationBuilder.DropColumn(
                name: "DairyId",
                table: "Learners");

            migrationBuilder.RenameTable(
                name: "Administrator",
                newName: "Administrators");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Learners",
                newName: "School");

            migrationBuilder.RenameIndex(
                name: "IX_Administrator_UserId",
                table: "Administrators",
                newName: "IX_Administrators_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Learners",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Learners",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CategoryVisited",
                table: "Diaries",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Visits",
                table: "Diaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrators",
                table: "Administrators",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrators_Users_UserId",
                table: "Administrators",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Users_UserId",
                table: "Administrators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrators",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "CategoryVisited",
                table: "Diaries");

            migrationBuilder.DropColumn(
                name: "Visits",
                table: "Diaries");

            migrationBuilder.RenameTable(
                name: "Administrators",
                newName: "Administrator");

            migrationBuilder.RenameColumn(
                name: "School",
                table: "Learners",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_Administrators_UserId",
                table: "Administrator",
                newName: "IX_Administrator_UserId");

            migrationBuilder.UpdateData(
                table: "Learners",
                keyColumn: "LastName",
                keyValue: null,
                column: "LastName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Learners",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Learners",
                keyColumn: "FirstName",
                keyValue: null,
                column: "FirstName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Learners",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "DairyId",
                table: "Learners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrator",
                table: "Administrator",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeletedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeletedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LearnerId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Category_LearnerId",
                table: "Category",
                column: "LearnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrator_Users_UserId",
                table: "Administrator",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
