using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boompa.Migrations
{
    /// <inheritdoc />
    public partial class za4suh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestRecords_Challenger_ChallengerId",
                table: "ContestRecords");

            migrationBuilder.DropTable(
                name: "CategoryChallengers");

            migrationBuilder.DropTable(
                name: "Challenger");

            migrationBuilder.RenameColumn(
                name: "ChallengerId",
                table: "ContestRecords",
                newName: "LearnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ContestRecords_ChallengerId",
                table: "ContestRecords",
                newName: "IX_ContestRecords_LearnerId");

            migrationBuilder.AlterColumn<string>(
                name: "SpeedAccuracyRatio",
                table: "ContestRecords",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "ContestRecords",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ContestRecords_Learners_LearnerId",
                table: "ContestRecords",
                column: "LearnerId",
                principalTable: "Learners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestRecords_Learners_LearnerId",
                table: "ContestRecords");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "ContestRecords");

            migrationBuilder.RenameColumn(
                name: "LearnerId",
                table: "ContestRecords",
                newName: "ChallengerId");

            migrationBuilder.RenameIndex(
                name: "IX_ContestRecords_LearnerId",
                table: "ContestRecords",
                newName: "IX_ContestRecords_ChallengerId");

            migrationBuilder.UpdateData(
                table: "ContestRecords",
                keyColumn: "SpeedAccuracyRatio",
                keyValue: null,
                column: "SpeedAccuracyRatio",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "SpeedAccuracyRatio",
                table: "ContestRecords",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Challenger",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LearnerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Challenger_Learners_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CategoryChallengers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChallengerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SpeedAccuracyRatio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryChallengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryChallengers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryChallengers_Challenger_ChallengerId",
                        column: x => x.ChallengerId,
                        principalTable: "Challenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryChallengers_CategoryId",
                table: "CategoryChallengers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryChallengers_ChallengerId",
                table: "CategoryChallengers",
                column: "ChallengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenger_LearnerId",
                table: "Challenger",
                column: "LearnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContestRecords_Challenger_ChallengerId",
                table: "ContestRecords",
                column: "ChallengerId",
                principalTable: "Challenger",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
