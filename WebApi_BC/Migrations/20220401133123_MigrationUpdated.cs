using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebApi.Migrations
{
    public partial class MigrationUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebApi.IDataContext.Texts");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "WebApi.IDataContext.Lessons",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "WebApi.IDataContext.Lessons");

            migrationBuilder.CreateTable(
                name: "WebApi.IDataContext.Texts",
                columns: table => new
                {
                    Id_text = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_lesson = table.Column<int>(type: "integer", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    Style = table.Column<string>(type: "text", nullable: true),
                    Text_ = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebApi.IDataContext.Texts", x => x.Id_text);
                });
        }
    }
}
