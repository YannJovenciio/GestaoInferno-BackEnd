using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inferno.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureEntityDemonWithCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demons_Categories_CategoryId",
                table: "Demons");

            migrationBuilder.AddForeignKey(
                name: "FK_Demons_Categories_CategoryId",
                table: "Demons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demons_Categories_CategoryId",
                table: "Demons");

            migrationBuilder.AddForeignKey(
                name: "FK_Demons_Categories_CategoryId",
                table: "Demons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
