﻿// <auto-generated />
using System;
using GM.Discord.Bot.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GM.Discord.Bot.Migrations
{
    [DbContext(typeof(GypsyContext))]
    [Migration("20201203200218_SpawnPrimaryKey2")]
    partial class SpawnPrimaryKey2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GM.Discord.Bot.Entities.ServerSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("ServerId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<decimal>("SpawnChannelId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.ToTable("ServerSettings");
                });

            modelBuilder.Entity("GM.Discord.Bot.Entities.Spawn", b =>
                {
                    b.Property<decimal>("ServerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<string[]>("AlternativeNames")
                        .HasColumnType("text[]");

                    b.Property<bool>("Caught")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ServerId");

                    b.ToTable("Spawns");
                });
#pragma warning restore 612, 618
        }
    }
}
