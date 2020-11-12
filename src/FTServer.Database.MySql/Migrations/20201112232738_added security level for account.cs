using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.MySql.Migrations
{
    public partial class addedsecuritylevelforaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "SecurityLevel",
                table: "Accounts",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityLevel",
                table: "Accounts");
        }
    }
}
