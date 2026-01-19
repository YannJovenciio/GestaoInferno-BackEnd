using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inferno.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSinEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sins_Souls_IdSoul",
                table: "Sins");

            migrationBuilder.AddForeignKey(
                name: "FK_Sins_Souls_IdSoul",
                table: "Sins",
                column: "IdSoul",
                principalTable: "Souls",
                principalColumn: "IdSoul");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sins_Souls_IdSoul",
                table: "Sins");

            migrationBuilder.AddForeignKey(
                name: "FK_Sins_Souls_IdSoul",
                table: "Sins",
                column: "IdSoul",
                principalTable: "Souls",
                principalColumn: "IdSoul",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
