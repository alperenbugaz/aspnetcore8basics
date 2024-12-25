using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class IdentityTablesColumnNameUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateAddes",
                table: "AspNetUsers",
                newName: "DateAdded");

            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "AspNetRoles",
                newName: "DateAdded");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "AspNetUsers",
                newName: "DateAddes");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "AspNetRoles",
                newName: "MyProperty");
        }
    }
}
