using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Lahiye.Migrations
{
    public partial class final155 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_StockStatuses_StockStatusId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "StockStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Products_StockStatusId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockStatusId",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "StockStatusId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StockStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InStock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockStatusId",
                table: "Products",
                column: "StockStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StockStatuses_StockStatusId",
                table: "Products",
                column: "StockStatusId",
                principalTable: "StockStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
