using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FTServer.Database.MySql.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTimestamp = table.Column<DateTime>(nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(nullable: true),
                    Ap = table.Column<int>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelayServers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTimestamp = table.Column<DateTime>(nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Heartbeat = table.Column<DateTime>(nullable: false),
                    Host = table.Column<string>(nullable: true),
                    Port = table.Column<ushort>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelayServers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTimestamp = table.Column<DateTime>(nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(nullable: true),
                    MaximumInventoryCount = table.Column<int>(nullable: false),
                    Experience = table.Column<int>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Gold = table.Column<int>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    Strength = table.Column<byte>(nullable: false),
                    Stamina = table.Column<byte>(nullable: false),
                    Dexterity = table.Column<byte>(nullable: false),
                    Willpower = table.Column<byte>(nullable: false),
                    StatusPoints = table.Column<byte>(nullable: false),
                    NameChangeAllowed = table.Column<bool>(nullable: false),
                    HairId = table.Column<int>(nullable: false),
                    FaceId = table.Column<int>(nullable: false),
                    DressId = table.Column<int>(nullable: false),
                    PantsId = table.Column<int>(nullable: false),
                    SocksId = table.Column<int>(nullable: false),
                    ShoesId = table.Column<int>(nullable: false),
                    GlovesId = table.Column<int>(nullable: false),
                    RacketId = table.Column<int>(nullable: false),
                    GlassesId = table.Column<int>(nullable: false),
                    BagId = table.Column<int>(nullable: false),
                    HatId = table.Column<int>(nullable: false),
                    DyeId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTimestamp = table.Column<DateTime>(nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(nullable: true),
                    AccountId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Hash = table.Column<string>(nullable: false),
                    Salt = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    DisabledUntil = table.Column<DateTime>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTimestamp = table.Column<DateTime>(nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(nullable: true),
                    CharacterId = table.Column<int>(nullable: false),
                    Level = table.Column<byte>(nullable: false),
                    HousingPoints = table.Column<int>(nullable: false),
                    FamousPoints = table.Column<int>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTimestamp = table.Column<DateTime>(nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(nullable: true),
                    Category = table.Column<byte>(nullable: false),
                    ItemTypeId = table.Column<int>(nullable: false),
                    UseType = table.Column<byte>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CharacterId = table.Column<int>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTimestamp = table.Column<DateTime>(nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(nullable: true),
                    Ip = table.Column<string>(nullable: true),
                    LoginId = table.Column<int>(nullable: false),
                    Successful = table.Column<bool>(nullable: false)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTimestamp = table.Column<DateTime>(nullable: false),
                    DeletionTimestamp = table.Column<DateTime>(nullable: true),
                    HomeId = table.Column<int>(nullable: false),
                    ItemTypeId = table.Column<int>(nullable: false),
                    Unknown0 = table.Column<byte>(nullable: false),
                    Unknown1 = table.Column<byte>(nullable: false),
                    X = table.Column<byte>(nullable: false),
                    Y = table.Column<byte>(nullable: false)
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
