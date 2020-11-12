using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.MySql.Migrations
{
    public partial class addeddataseedtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataSeeds",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreationTimestamp = table.Column<DateTime>(nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSeeds", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSeeds");
        }
    }
}
