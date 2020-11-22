using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.MySql.Migrations
{
    public partial class addnewfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                table: "Logins",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "LoginAttempts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Online",
                table: "Accounts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "LoginAttempts");

            migrationBuilder.DropColumn(
                name: "Online",
                table: "Accounts");
        }
    }
}
