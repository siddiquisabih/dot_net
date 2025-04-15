using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global.Project.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Questions_QuestionId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_QuestionId",
                table: "Exams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Exams_QuestionId",
                table: "Exams",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Questions_QuestionId",
                table: "Exams",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
