using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExtraSocialLinksFromSocialLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GitHub",
                table: "SocialLinks");

            migrationBuilder.DropColumn(
                name: "YouTube",
                table: "SocialLinks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GitHub",
                table: "SocialLinks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YouTube",
                table: "SocialLinks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
