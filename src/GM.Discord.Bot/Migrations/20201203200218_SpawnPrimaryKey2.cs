using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GM.Discord.Bot.Migrations
{
    public partial class SpawnPrimaryKey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Spawns",
                table: "Spawns");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Spawns");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spawns",
                table: "Spawns",
                column: "ServerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Spawns",
                table: "Spawns");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Spawns",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spawns",
                table: "Spawns",
                column: "Id");
        }
    }
}
