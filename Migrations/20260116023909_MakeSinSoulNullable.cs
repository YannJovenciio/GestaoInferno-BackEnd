using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inferno.Migrations
{
    /// <inheritdoc />
    public partial class MakeSinSoulNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sins_Souls_IdSoul",
                table: "Sins");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdSoul",
                table: "Sins",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Sins_Souls_IdSoul",
                table: "Sins",
                column: "IdSoul",
                principalTable: "Souls",
                principalColumn: "IdSoul",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sins_Souls_IdSoul",
                table: "Sins");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdSoul",
                table: "Sins",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sins_Souls_IdSoul",
                table: "Sins",
                column: "IdSoul",
                principalTable: "Souls",
                principalColumn: "IdSoul");
        }
    }
}
