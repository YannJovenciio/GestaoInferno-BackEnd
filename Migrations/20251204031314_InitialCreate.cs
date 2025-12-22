using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inferno.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CodCategoria = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeCategoria = table.Column<string>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CodCategoria);
                }
            );

            migrationBuilder.CreateTable(
                name: "Caverns",
                columns: table => new
                {
                    IdCavern = table.Column<Guid>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caverns", x => x.IdCavern);
                }
            );

            migrationBuilder.CreateTable(
                name: "Hell",
                columns: table => new
                {
                    IdHell = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Nivel = table.Column<int>(type: "INTEGER", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hell", x => x.IdHell);
                }
            );

            migrationBuilder.CreateTable(
                name: "Demons",
                columns: table => new
                {
                    IdDemon = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryCodCategoria = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeDemo = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demons", x => x.IdDemon);
                    table.ForeignKey(
                        name: "FK_Demons_Categories_CategoryCodCategoria",
                        column: x => x.CategoryCodCategoria,
                        principalTable: "Categories",
                        principalColumn: "CodCategoria",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Souls",
                columns: table => new
                {
                    IdSoul = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(
                        type: "TEXT",
                        maxLength: 500,
                        nullable: false
                    ),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    CavernId = table.Column<Guid>(type: "TEXT", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Souls", x => x.IdSoul);
                    table.ForeignKey(
                        name: "FK_Souls_Caverns_CavernId",
                        column: x => x.CavernId,
                        principalTable: "Caverns",
                        principalColumn: "IdCavern"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Persecution",
                columns: table => new
                {
                    IdDemon = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdSoul = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TEXT", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persecution", x => new { x.IdDemon, x.IdSoul });
                    table.ForeignKey(
                        name: "FK_Persecution_Demons_IdDemon",
                        column: x => x.IdDemon,
                        principalTable: "Demons",
                        principalColumn: "IdDemon",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Persecution_Souls_IdSoul",
                        column: x => x.IdSoul,
                        principalTable: "Souls",
                        principalColumn: "IdSoul",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Sins",
                columns: table => new
                {
                    IdSin = table.Column<Guid>(type: "TEXT", nullable: false),
                    SinName = table.Column<string>(type: "TEXT", nullable: false),
                    SinSeverity = table.Column<int>(type: "INTEGER", nullable: false),
                    IdSoul = table.Column<Guid>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sins", x => x.IdSin);
                    table.ForeignKey(
                        name: "FK_Sins_Souls_IdSoul",
                        column: x => x.IdSoul,
                        principalTable: "Souls",
                        principalColumn: "IdSoul",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Realize",
                columns: table => new
                {
                    IdSin = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdSoul = table.Column<Guid>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realize", x => new { x.IdSin, x.IdSoul });
                    table.ForeignKey(
                        name: "FK_Realize_Sins_IdSin",
                        column: x => x.IdSin,
                        principalTable: "Sins",
                        principalColumn: "IdSin",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Realize_Souls_IdSoul",
                        column: x => x.IdSoul,
                        principalTable: "Souls",
                        principalColumn: "IdSoul",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Demons_CategoryCodCategoria",
                table: "Demons",
                column: "CategoryCodCategoria"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Persecution_IdSoul",
                table: "Persecution",
                column: "IdSoul"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Realize_IdSoul",
                table: "Realize",
                column: "IdSoul"
            );

            migrationBuilder.CreateIndex(name: "IX_Sins_IdSoul", table: "Sins", column: "IdSoul");

            migrationBuilder.CreateIndex(
                name: "IX_Souls_CavernId",
                table: "Souls",
                column: "CavernId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Hell");

            migrationBuilder.DropTable(name: "Persecution");

            migrationBuilder.DropTable(name: "Realize");

            migrationBuilder.DropTable(name: "Demons");

            migrationBuilder.DropTable(name: "Sins");

            migrationBuilder.DropTable(name: "Categories");

            migrationBuilder.DropTable(name: "Souls");

            migrationBuilder.DropTable(name: "Caverns");
        }
    }
}
