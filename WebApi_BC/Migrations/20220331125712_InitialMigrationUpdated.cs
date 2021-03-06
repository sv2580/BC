using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class InitialMigrationUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Permission",
                table: "WebApi.IDataContext.UserPermissions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Style",
                table: "WebApi.IDataContext.Texts",
                type: "text",
                nullable: true,
                oldClrType: typeof(char),
                oldType: "character(1)");

            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "WebApi.IDataContext.Courses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "WebApi.IDataContext.Users",
                columns: table => new
                {
                    User_email = table.Column<string>(type: "text", nullable: false),
                    User_password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebApi.IDataContext.Users", x => x.User_email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebApi.IDataContext.Users");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "WebApi.IDataContext.Courses");

            migrationBuilder.AlterColumn<string>(
                name: "Permission",
                table: "WebApi.IDataContext.UserPermissions",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<char>(
                name: "Style",
                table: "WebApi.IDataContext.Texts",
                type: "character(1)",
                nullable: false,
                defaultValue: ' ',
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
