using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.SQLite.Migrations
{
    public partial class Initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ap = table.Column<int>(type: "INTEGER", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    SecurityLevel = table.Column<byte>(type: "INTEGER", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataSeeds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelayServers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Heartbeat = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Host = table.Column<string>(type: "TEXT", nullable: true),
                    Port = table.Column<ushort>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: true),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelayServers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaximumInventoryCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Experience = table.Column<int>(type: "INTEGER", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Gold = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<byte>(type: "INTEGER", nullable: false),
                    Strength = table.Column<byte>(type: "INTEGER", nullable: false),
                    Stamina = table.Column<byte>(type: "INTEGER", nullable: false),
                    Dexterity = table.Column<byte>(type: "INTEGER", nullable: false),
                    Willpower = table.Column<byte>(type: "INTEGER", nullable: false),
                    StatusPoints = table.Column<byte>(type: "INTEGER", nullable: false),
                    NameChangeAllowed = table.Column<bool>(type: "INTEGER", nullable: false),
                    HairId = table.Column<int>(type: "INTEGER", nullable: false),
                    FaceId = table.Column<int>(type: "INTEGER", nullable: false),
                    DressId = table.Column<int>(type: "INTEGER", nullable: false),
                    PantsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SocksId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShoesId = table.Column<int>(type: "INTEGER", nullable: false),
                    GlovesId = table.Column<int>(type: "INTEGER", nullable: false),
                    RacketId = table.Column<int>(type: "INTEGER", nullable: false),
                    GlassesId = table.Column<int>(type: "INTEGER", nullable: false),
                    BagId = table.Column<int>(type: "INTEGER", nullable: false),
                    HatId = table.Column<int>(type: "INTEGER", nullable: false),
                    DyeId = table.Column<int>(type: "INTEGER", nullable: false),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Hash = table.Column<string>(type: "TEXT", nullable: false),
                    Salt = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    DisabledUntil = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logins_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<byte>(type: "INTEGER", nullable: false),
                    HousingPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    FamousPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homes_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<byte>(type: "INTEGER", nullable: false),
                    ItemTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    UseType = table.Column<byte>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ip = table.Column<string>(type: "TEXT", nullable: true),
                    LoginId = table.Column<int>(type: "INTEGER", nullable: false),
                    Successful = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginAttempts_Logins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Logins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Furniture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HomeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Unknown0 = table.Column<byte>(type: "INTEGER", nullable: false),
                    Unknown1 = table.Column<byte>(type: "INTEGER", nullable: false),
                    X = table.Column<byte>(type: "INTEGER", nullable: false),
                    Y = table.Column<byte>(type: "INTEGER", nullable: false),
                    CreationTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furniture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Furniture_Homes_HomeId",
                        column: x => x.HomeId,
                        principalTable: "Homes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_AccountId",
                table: "Characters",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Furniture_HomeId",
                table: "Furniture",
                column: "HomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Homes_CharacterId",
                table: "Homes",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CharacterId",
                table: "Items",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginAttempts_LoginId",
                table: "LoginAttempts",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_AccountId",
                table: "Logins",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RelayServers_Name",
                table: "RelayServers",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSeeds");

            migrationBuilder.DropTable(
                name: "Furniture");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "LoginAttempts");

            migrationBuilder.DropTable(
                name: "RelayServers");

            migrationBuilder.DropTable(
                name: "Homes");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
