using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Lahiye.Migrations
{
    public partial class createCommentt123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Products_HomeProductId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_HomeProductId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "HomeProductId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Products_ProductId",
                table: "Comments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Products_ProductId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ProductId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "HomeProductId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_HomeProductId",
                table: "Comments",
                column: "HomeProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Products_HomeProductId",
                table: "Comments",
                column: "HomeProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
