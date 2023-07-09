using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Lahiye.Migrations
{
    public partial class final159 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colours_Colours_ColourId",
                table: "Colours");

            migrationBuilder.DropIndex(
                name: "IX_Colours_ColourId",
                table: "Colours");

            migrationBuilder.DropColumn(
                name: "ColourId",
                table: "Colours");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColourId",
                table: "Colours",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colours_ColourId",
                table: "Colours",
                column: "ColourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colours_Colours_ColourId",
                table: "Colours",
                column: "ColourId",
                principalTable: "Colours",
                principalColumn: "Id");
        }
    }
}
