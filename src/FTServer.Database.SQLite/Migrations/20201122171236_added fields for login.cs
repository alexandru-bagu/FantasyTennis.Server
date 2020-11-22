using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.SQLite.Migrations
{
    public partial class addedfieldsforlogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "LoginAttempts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Online",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "LoginAttempts");

            migrationBuilder.DropColumn(
                name: "Online",
                table: "Accounts");
        }
    }
}
