using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inferno.Migrations
{
    /// <inheritdoc />
    public partial class StandarlizingPropertiesNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Souls",
                newName: "SoulName");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Hell",
                newName: "HellName");

            migrationBuilder.AddColumn<string>(
                name: "CavernName",
                table: "Caverns",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CavernName",
                table: "Caverns");

            migrationBuilder.RenameColumn(
                name: "SoulName",
                table: "Souls",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "HellName",
                table: "Hell",
                newName: "Nome");
        }
    }
}
