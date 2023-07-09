using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Lahiye.Migrations
{
    public partial class final163 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SingleBlogs");

            migrationBuilder.AddColumn<string>(
                name: "MiddleDescription",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleTextFirst",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleTextFourth",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleTextSecond",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleTextThird",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceDescriptionFirst",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceDescriptionFourth",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceDescriptionSecond",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceDescriptionThird",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceNameFirst",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceNameFourth",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceNameSecond",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceNameThird",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ServicePriceFirst",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServicePriceFourth",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServicePriceSecond",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServicePriceThird",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiddleDescription",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "MiddleTextFirst",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "MiddleTextFourth",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "MiddleTextSecond",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "MiddleTextThird",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServiceDescriptionFirst",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServiceDescriptionFourth",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServiceDescriptionSecond",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServiceDescriptionThird",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServiceNameFirst",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServiceNameFourth",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServiceNameSecond",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServiceNameThird",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServicePriceFirst",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServicePriceFourth",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServicePriceSecond",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ServicePriceThird",
                table: "Blogs");

            migrationBuilder.CreateTable(
                name: "SingleBlogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplyInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplyInfoLeftImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplyInfoRightImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderEnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderMiddle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderUp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformationEnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformationMiddle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformationUp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleBlogs", x => x.Id);
                });
        }
    }
}
