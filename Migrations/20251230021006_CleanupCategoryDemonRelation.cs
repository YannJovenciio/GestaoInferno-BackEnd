using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inferno.Migrations
{
    /// <inheritdoc />
    public partial class CleanupCategoryDemonRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Demons_DemonIdDemon",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DemonIdDemon",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DemonIdDemon",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DemonIdDemon",
                table: "Categories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DemonIdDemon",
                table: "Categories",
                column: "DemonIdDemon");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Demons_DemonIdDemon",
                table: "Categories",
                column: "DemonIdDemon",
                principalTable: "Demons",
                principalColumn: "IdDemon");
        }
    }
}
