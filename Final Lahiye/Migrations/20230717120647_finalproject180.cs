using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final_Lahiye.Migrations
{
    public partial class finalproject180 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeaderUpSocialMedias");

            migrationBuilder.DropTable(
                name: "HeaderUpTexts");

            migrationBuilder.CreateTable(
                name: "Bios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacebookLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstagramLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TwitterLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bios");

            migrationBuilder.CreateTable(
                name: "HeaderUpSocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacebookLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstagramLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TwitterLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderUpSocialMedias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeaderUpTexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderUpTexts", x => x.Id);
                });
        }
    }
}
