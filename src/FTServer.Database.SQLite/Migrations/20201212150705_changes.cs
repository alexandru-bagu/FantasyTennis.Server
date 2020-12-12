using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.SQLite.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Items");

            migrationBuilder.AddColumn<bool>(
                name: "NameChangeByIcon",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameChangeByIcon",
                table: "Characters");

            migrationBuilder.AddColumn<byte>(
                name: "Category",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
