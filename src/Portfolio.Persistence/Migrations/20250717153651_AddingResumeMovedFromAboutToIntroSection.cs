using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingResumeMovedFromAboutToIntroSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResumePath",
                table: "Abouts");

            migrationBuilder.AddColumn<string>(
                name: "ResumePath",
                table: "Intros",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResumePath",
                table: "Intros");

            migrationBuilder.AddColumn<string>(
                name: "ResumePath",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
