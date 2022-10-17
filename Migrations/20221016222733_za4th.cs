using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boompa.Migrations
{
    public partial class za4th : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayMessage",
                table: "Diaries",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryVisited",
                table: "Diaries",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 16, 22, 27, 32, 836, DateTimeKind.Utc).AddTicks(7457));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 16, 22, 27, 32, 836, DateTimeKind.Utc).AddTicks(7460));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 16, 22, 27, 32, 836, DateTimeKind.Utc).AddTicks(7461));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Diaries",
                keyColumn: "DisplayMessage",
                keyValue: null,
                column: "DisplayMessage",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayMessage",
                table: "Diaries",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Diaries",
                keyColumn: "CategoryVisited",
                keyValue: null,
                column: "CategoryVisited",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryVisited",
                table: "Diaries",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 16, 22, 8, 57, 136, DateTimeKind.Utc).AddTicks(3765));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 16, 22, 8, 57, 136, DateTimeKind.Utc).AddTicks(3769));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 16, 22, 8, 57, 136, DateTimeKind.Utc).AddTicks(3770));
        }
    }
}
