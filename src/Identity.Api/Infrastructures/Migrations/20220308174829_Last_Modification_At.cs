using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Api.Infrastructures.Migrations
{
    public partial class Last_Modification_At : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "ApplicationUsers",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ApplicationUsers",
                newName: "LastModifiedAt");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "ApplicationUsers",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "ApplicationUsers",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
