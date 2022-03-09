using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Api.Infrastructures.Migrations
{
    public partial class Add_UserId_For_Application : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "ApplicationUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ApplicationUsers");
        }
    }
}
