using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Lahiye.Migrations
{
    public partial class final160 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplyInfo",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplyInfoLeftImage",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplyInfoRightImage",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeaderEnd",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeaderMiddle",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeaderUp",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InformationEnd",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InformationMiddle",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InformationUp",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceInfo",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyInfo",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ApplyInfoLeftImage",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ApplyInfoRightImage",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "HeaderEnd",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "HeaderMiddle",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "HeaderUp",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "InformationEnd",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "InformationMiddle",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "InformationUp",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServiceInfo",
                table: "Blogs");
        }
    }
}
