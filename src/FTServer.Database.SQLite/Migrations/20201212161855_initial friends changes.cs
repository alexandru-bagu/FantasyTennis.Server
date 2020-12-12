using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.SQLite.Migrations
{
    public partial class initialfriendschanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "ActiveServerId",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HeroOneId = table.Column<int>(type: "INTEGER", nullable: false),
                    HeroTwoId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendships_Characters_HeroOneId",
                        column: x => x.HeroOneId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friendships_Characters_HeroTwoId",
                        column: x => x.HeroTwoId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ActiveServerId",
                table: "Accounts",
                column: "ActiveServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_HeroOneId",
                table: "Friendships",
                column: "HeroOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_HeroTwoId",
                table: "Friendships",
                column: "HeroTwoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_GameServers_ActiveServerId",
                table: "Accounts",
                column: "ActiveServerId",
                principalTable: "GameServers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_GameServers_ActiveServerId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ActiveServerId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ActiveServerId",
                table: "Accounts");
        }
    }
}
