using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inferno.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demons_Categories_CategoryId",
                table: "Demons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persecution_Demons_IdDemon",
                table: "Persecution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Demons",
                table: "Demons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Demons",
                newName: "Demon");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Demons_CategoryId",
                table: "Demon",
                newName: "IX_Demon_CategoryId");

            migrationBuilder.RenameColumn(
                name: "NomeCategoria",
                table: "Category",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "IdCategoria",
                table: "Category",
                newName: "IdCategory");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Demon",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Demon",
                table: "Demon",
                column: "IdDemon");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "IdCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_Demon_Category_CategoryId",
                table: "Demon",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "IdCategory",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persecution_Demon_IdDemon",
                table: "Persecution",
                column: "IdDemon",
                principalTable: "Demon",
                principalColumn: "IdDemon",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demon_Category_CategoryId",
                table: "Demon");

            migrationBuilder.DropForeignKey(
                name: "FK_Persecution_Demon_IdDemon",
                table: "Persecution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Demon",
                table: "Demon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Demon",
                newName: "Demons");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Demon_CategoryId",
                table: "Demons",
                newName: "IX_Demons_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "NomeCategoria");

            migrationBuilder.RenameColumn(
                name: "IdCategory",
                table: "Categories",
                newName: "IdCategoria");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Demons",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Demons",
                table: "Demons",
                column: "IdDemon");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "IdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Demons_Categories_CategoryId",
                table: "Demons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persecution_Demons_IdDemon",
                table: "Persecution",
                column: "IdDemon",
                principalTable: "Demons",
                principalColumn: "IdDemon",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
