using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogEngine.Migrations.SqliteMigrations
{
    public partial class blockuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "users");
        }
    }
}
