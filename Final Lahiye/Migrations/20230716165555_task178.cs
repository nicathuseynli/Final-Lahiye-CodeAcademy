using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Lahiye.Migrations
{
    public partial class task178 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
