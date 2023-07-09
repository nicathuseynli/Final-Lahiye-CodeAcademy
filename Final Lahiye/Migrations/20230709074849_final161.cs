using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Lahiye.Migrations
{
    public partial class final161 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SingleBlogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderUp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformationUp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderMiddle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformationMiddle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderEnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformationEnd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplyInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplyInfoLeftImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplyInfoRightImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleBlogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SingleBlogs");
        }
    }
}
