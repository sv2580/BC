using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebApi.Migrations
{
    public partial class AnotherMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebApi.IDataContext.Lessons",
                columns: table => new
                {
                    Id_lesson = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    Id_chapter = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebApi.IDataContext.Lessons", x => x.Id_lesson);
                });

            migrationBuilder.CreateTable(
                name: "WebApi.IDataContext.Texts",
                columns: table => new
                {
                    Id_text = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text_ = table.Column<string>(type: "text", nullable: true),
                    Style = table.Column<char>(type: "character(1)", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    Id_lesson = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebApi.IDataContext.Texts", x => x.Id_text);
                });

            migrationBuilder.CreateTable(
                name: "WebApi.IDataContext.UserPermissions",
                columns: table => new
                {
                    User_email = table.Column<string>(type: "text", nullable: false),
                    Permission = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebApi.IDataContext.UserPermissions", x => x.User_email);
                });

            migrationBuilder.CreateTable(
                name: "WebApi.IDataContext.UserRoles",
                columns: table => new
                {
                    User_email = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebApi.IDataContext.UserRoles", x => x.User_email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebApi.IDataContext.Lessons");

            migrationBuilder.DropTable(
                name: "WebApi.IDataContext.Texts");

            migrationBuilder.DropTable(
                name: "WebApi.IDataContext.UserPermissions");

            migrationBuilder.DropTable(
                name: "WebApi.IDataContext.UserRoles");
        }
    }
}
