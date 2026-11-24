using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boompa.Migrations
{
    /// <inheritdoc />
    public partial class za2nd0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseQuestion_Option_AnswerId",
                table: "BaseQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_Option_BaseQuestion_MultiChoiceQuestionId",
                table: "Option");

            migrationBuilder.DropForeignKey(
                name: "FK_Option_BaseQuestion_QuestionId",
                table: "Option");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Option",
                table: "Option");

            migrationBuilder.RenameTable(
                name: "Option",
                newName: "Options");

            migrationBuilder.RenameIndex(
                name: "IX_Option_QuestionId",
                table: "Options",
                newName: "IX_Options_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Option_MultiChoiceQuestionId",
                table: "Options",
                newName: "IX_Options_MultiChoiceQuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Options",
                table: "Options",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseQuestion_Options_AnswerId",
                table: "BaseQuestion",
                column: "AnswerId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_BaseQuestion_MultiChoiceQuestionId",
                table: "Options",
                column: "MultiChoiceQuestionId",
                principalTable: "BaseQuestion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_BaseQuestion_QuestionId",
                table: "Options",
                column: "QuestionId",
                principalTable: "BaseQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseQuestion_Options_AnswerId",
                table: "BaseQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_BaseQuestion_MultiChoiceQuestionId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_BaseQuestion_QuestionId",
                table: "Options");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Options",
                table: "Options");

            migrationBuilder.RenameTable(
                name: "Options",
                newName: "Option");

            migrationBuilder.RenameIndex(
                name: "IX_Options_QuestionId",
                table: "Option",
                newName: "IX_Option_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_MultiChoiceQuestionId",
                table: "Option",
                newName: "IX_Option_MultiChoiceQuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Option",
                table: "Option",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseQuestion_Option_AnswerId",
                table: "BaseQuestion",
                column: "AnswerId",
                principalTable: "Option",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Option_BaseQuestion_MultiChoiceQuestionId",
                table: "Option",
                column: "MultiChoiceQuestionId",
                principalTable: "BaseQuestion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Option_BaseQuestion_QuestionId",
                table: "Option",
                column: "QuestionId",
                principalTable: "BaseQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
