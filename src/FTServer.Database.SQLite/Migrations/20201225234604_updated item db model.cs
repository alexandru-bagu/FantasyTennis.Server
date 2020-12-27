using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.SQLite.Migrations
{
    public partial class updateditemdbmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemTypeId",
                table: "Items",
                newName: "Unknown2");

            migrationBuilder.AddColumn<byte>(
                name: "CategoryType",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "EnchantDexterity",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "EnchantStamina",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "EnchantStrength",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "EnchantWillpower",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "Unknown1",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "EnchantDexterity",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "EnchantStamina",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "EnchantStrength",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "EnchantWillpower",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Unknown1",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Unknown2",
                table: "Items",
                newName: "ItemTypeId");
        }
    }
}
