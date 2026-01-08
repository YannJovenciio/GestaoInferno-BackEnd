using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inferno.Migrations
{
    /// <inheritdoc />
    public partial class ImproveEntitys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demons_Categories_CategoryCodCategoria",
                table: "Demons");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Persecution");

            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "Persecution",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "NomeDemo",
                table: "Demons",
                newName: "DemonName");

            migrationBuilder.RenameColumn(
                name: "CategoryCodCategoria",
                table: "Demons",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Demons_CategoryCodCategoria",
                table: "Demons",
                newName: "IX_Demons_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CodCategoria",
                table: "Categories",
                newName: "IdCategoria");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Demons_Categories_CategoryId",
                table: "Demons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Demons_DemonIdDemon",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Demons_Categories_CategoryId",
                table: "Demons");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DemonIdDemon",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DemonIdDemon",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Persecution",
                newName: "DataInicio");

            migrationBuilder.RenameColumn(
                name: "DemonName",
                table: "Demons",
                newName: "NomeDemo");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Demons",
                newName: "CategoryCodCategoria");

            migrationBuilder.RenameIndex(
                name: "IX_Demons_CategoryId",
                table: "Demons",
                newName: "IX_Demons_CategoryCodCategoria");

            migrationBuilder.RenameColumn(
                name: "IdCategoria",
                table: "Categories",
                newName: "CodCategoria");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Persecution",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Demons_Categories_CategoryCodCategoria",
                table: "Demons",
                column: "CategoryCodCategoria",
                principalTable: "Categories",
                principalColumn: "CodCategoria",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
