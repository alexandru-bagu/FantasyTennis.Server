using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.SQLite.Migrations
{
    public partial class removevisualfieldsfromcharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BagId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "DressId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "DyeId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "FaceId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "GlassesId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "GlovesId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "HairId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "HatId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PantsId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "RacketId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "ShoesId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "SocksId",
                table: "Characters");

            migrationBuilder.AddColumn<bool>(
                name: "Equipped",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipped",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "BagId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DressId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DyeId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FaceId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GlassesId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GlovesId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HairId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HatId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PantsId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RacketId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShoesId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SocksId",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
