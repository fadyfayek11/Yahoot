using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yahoot.Migrations
{
    public partial class EditQuizTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Quizzes_QuizId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_QuizId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "QuizUser",
                columns: table => new
                {
                    QuizzesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizUser", x => new { x.QuizzesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_QuizUser_Quizzes_QuizzesId",
                        column: x => x.QuizzesId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizUser_UsersId",
                table: "QuizUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizUser");

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_QuizId",
                table: "Users",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Quizzes_QuizId",
                table: "Users",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }
    }
}
