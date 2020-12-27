using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.SQLite.Migrations
{
    public partial class addedtutorialandchallengeprogress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChallengeProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChallengeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Completed = table.Column<int>(type: "INTEGER", nullable: false),
                    Attempts = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeProgress_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorialProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TutorialId = table.Column<int>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Completed = table.Column<int>(type: "INTEGER", nullable: false),
                    Attempts = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorialProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorialProgress_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeProgress_CharacterId",
                table: "ChallengeProgress",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorialProgress_CharacterId",
                table: "TutorialProgress",
                column: "CharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeProgress");

            migrationBuilder.DropTable(
                name: "TutorialProgress");
        }
    }
}
