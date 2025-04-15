using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global.Project.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Exams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
