﻿// <auto-generated />
using System;
using FTServer.Database.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FTServer.Database.SQLite.Migrations
{
    [DbContext(typeof(SQLiteDbContext))]
    [Migration("20201201183525_added character and account fields; separated gameserver from relayservers;")]
    partial class addedcharacterandaccountfieldsseparatedgameserverfromrelayservers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("FTServer.Database.Model.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ap")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Key1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Key2")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LastCharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Online")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("SecurityLevel")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("FTServer.Database.Model.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BagId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<byte>("Dexterity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DressId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DyeId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Experience")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FaceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GlassesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GlovesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gold")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HairId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HatId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCreated")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaximumInventoryCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("NameChangeAllowed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PantsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RacketId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShoesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SocksId")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Stamina")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("StatusPoints")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Strength")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Willpower")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("FTServer.Database.Model.DataSeed", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionTimestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DataSeeds");
                });

            modelBuilder.Entity("FTServer.Database.Model.Furniture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("HomeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Unknown0")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Unknown1")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("X")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Y")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HomeId");

                    b.ToTable("Furniture");
                });

            modelBuilder.Entity("FTServer.Database.Model.GameServer", b =>
                {
                    b.Property<ushort>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Heartbeat")
                        .HasColumnType("TEXT");

                    b.Property<string>("Host")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<ushort>("OnlineCount")
                        .HasColumnType("INTEGER");

                    b.Property<ushort>("Port")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ShowName")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("GameServers");
                });

            modelBuilder.Entity("FTServer.Database.Model.Home", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("FamousPoints")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HousingPoints")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Level")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Homes");
                });

            modelBuilder.Entity("FTServer.Database.Model.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Category")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("UseType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("FTServer.Database.Model.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DisabledUntil")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("FTServer.Database.Model.LoginAttempt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ip")
                        .HasColumnType("TEXT");

                    b.Property<int>("LoginId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Successful")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LoginId");

                    b.ToTable("LoginAttempts");
                });

            modelBuilder.Entity("FTServer.Database.Model.RelayServer", b =>
                {
                    b.Property<ushort>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Heartbeat")
                        .HasColumnType("TEXT");

                    b.Property<string>("Host")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<ushort>("Port")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("RelayServers");
                });

            modelBuilder.Entity("FTServer.Database.Model.Character", b =>
                {
                    b.HasOne("FTServer.Database.Model.Account", "Account")
                        .WithMany("Characters")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("FTServer.Database.Model.Furniture", b =>
                {
                    b.HasOne("FTServer.Database.Model.Home", "Home")
                        .WithMany("Furniture")
                        .HasForeignKey("HomeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Home");
                });

            modelBuilder.Entity("FTServer.Database.Model.Home", b =>
                {
                    b.HasOne("FTServer.Database.Model.Character", "Character")
                        .WithMany("Homes")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("FTServer.Database.Model.Item", b =>
                {
                    b.HasOne("FTServer.Database.Model.Character", "Character")
                        .WithMany("Items")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("FTServer.Database.Model.Login", b =>
                {
                    b.HasOne("FTServer.Database.Model.Account", "Account")
                        .WithMany("Logins")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("FTServer.Database.Model.LoginAttempt", b =>
                {
                    b.HasOne("FTServer.Database.Model.Login", "Login")
                        .WithMany()
                        .HasForeignKey("LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Login");
                });

            modelBuilder.Entity("FTServer.Database.Model.Account", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Logins");
                });

            modelBuilder.Entity("FTServer.Database.Model.Character", b =>
                {
                    b.Navigation("Homes");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("FTServer.Database.Model.Home", b =>
                {
                    b.Navigation("Furniture");
                });
#pragma warning restore 612, 618
        }
    }
}
