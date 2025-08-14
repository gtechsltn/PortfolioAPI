using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInterceptorToOtherEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SkillDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SkillDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SkillDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SkillDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Services",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Services",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Reviews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Reviews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SkillDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SkillDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SkillDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SkillDetails");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Reviews");
        }
    }
}
