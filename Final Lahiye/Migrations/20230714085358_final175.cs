using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Lahiye.Migrations
{
    public partial class final175 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "FaqPaymentPages");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "FaqPages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "FaqPaymentPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "FaqPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
