using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.SQLite.Migrations
{
    public partial class addedcharacterandaccountfieldsseparatedgameserverfromrelayservers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "RelayServers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "RelayServers");

            migrationBuilder.AddColumn<bool>(
                name: "IsCreated",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "Level",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Characters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Key1",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Key2",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastCharacterId",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GameServers",
                columns: table => new
                {
                    Id = table.Column<ushort>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Heartbeat = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Host = table.Column<string>(type: "TEXT", nullable: true),
                    Port = table.Column<ushort>(type: "INTEGER", nullable: false),
                    ShowName = table.Column<bool>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    OnlineCount = table.Column<ushort>(type: "INTEGER", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameServers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_Name",
                table: "Characters",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameServers_Name",
                table: "GameServers",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameServers");

            migrationBuilder.DropIndex(
                name: "IX_Characters_Name",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "IsCreated",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Key1",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Key2",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastCharacterId",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "RelayServers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "RelayServers",
                type: "INTEGER",
                nullable: true);
        }
    }
}
