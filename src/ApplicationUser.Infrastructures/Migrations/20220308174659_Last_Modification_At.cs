using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationUser.Infrastructures.Migrations
{
    public partial class Last_Modification_At : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Users",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "UserRoleMappings",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "UserRoleMappings",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Tenants",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Tenants",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Roles",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Roles",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "RolePermissionMappings",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "RolePermissionMappings",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Permissions",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Permissions",
                newName: "LastModifiedAt");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserRoleMappings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "RolePermissionMappings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Permissions",
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
                table: "Users",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "UserRoleMappings",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "UserRoleMappings",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Tenants",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Tenants",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Roles",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Roles",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "RolePermissionMappings",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "RolePermissionMappings",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Permissions",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Permissions",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserRoleMappings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "RolePermissionMappings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
