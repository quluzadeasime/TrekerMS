using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TREKER.DAL.Migrations
{
    /// <inheritdoc />
    public partial class createDbAndModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Settings",
                newName: "Youtube");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Settings",
                newName: "Twitter");

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone1",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone2",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Phone1",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "Youtube",
                table: "Settings",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "Twitter",
                table: "Settings",
                newName: "Key");
        }
    }
}
