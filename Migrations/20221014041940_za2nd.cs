using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boompa.Migrations
{
    public partial class za2nd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "RoleName" },
                values: new object[] { 1, null, new DateTime(2022, 10, 14, 4, 19, 40, 370, DateTimeKind.Utc).AddTicks(1147), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "the base entity in the app", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "RoleName" },
                values: new object[] { 2, null, new DateTime(2022, 10, 14, 4, 19, 40, 370, DateTimeKind.Utc).AddTicks(1153), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "the user with authority to do certain stuff on user profiles", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Description", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "RoleName" },
                values: new object[] { 3, null, new DateTime(2022, 10, 14, 4, 19, 40, 370, DateTimeKind.Utc).AddTicks(1154), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "the reason this app is being developed", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learner" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
