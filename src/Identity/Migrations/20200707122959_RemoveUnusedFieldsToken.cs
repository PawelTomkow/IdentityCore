using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Identity.Migrations
{
    public partial class RemoveUnusedFieldsToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "ExperienceTime",
                table: "Tokens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TokenId",
                table: "Tokens",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "Tokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ExperienceTime",
                table: "Tokens",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens",
                column: "TokenId");
        }
    }
}
