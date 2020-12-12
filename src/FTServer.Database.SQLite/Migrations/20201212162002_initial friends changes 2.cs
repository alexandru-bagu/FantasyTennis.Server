using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.SQLite.Migrations
{
    public partial class initialfriendschanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_GameServers_ActiveServerId",
                table: "Accounts");

            migrationBuilder.AlterColumn<short>(
                name: "ActiveServerId",
                table: "Accounts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_GameServers_ActiveServerId",
                table: "Accounts",
                column: "ActiveServerId",
                principalTable: "GameServers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_GameServers_ActiveServerId",
                table: "Accounts");

            migrationBuilder.AlterColumn<short>(
                name: "ActiveServerId",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_GameServers_ActiveServerId",
                table: "Accounts",
                column: "ActiveServerId",
                principalTable: "GameServers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
