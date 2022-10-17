using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boompa.Migrations
{
    public partial class za3rd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitId",
                table: "Diaries");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VisitId",
                table: "Diaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 14, 4, 19, 40, 370, DateTimeKind.Utc).AddTicks(1147));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 14, 4, 19, 40, 370, DateTimeKind.Utc).AddTicks(1153));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2022, 10, 14, 4, 19, 40, 370, DateTimeKind.Utc).AddTicks(1154));
        }
    }
}
