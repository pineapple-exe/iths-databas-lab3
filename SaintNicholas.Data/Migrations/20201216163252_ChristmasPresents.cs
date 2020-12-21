using Microsoft.EntityFrameworkCore.Migrations;

namespace SaintNicholas.Data.Migrations
{
    public partial class ChristmasPresents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChristmasPresents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contents = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ForNaughtyChild = table.Column<bool>(type: "bit", nullable: false),
                    Receiver = table.Column<int>(type: "int", nullable: false),
                    HandOutYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChristmasPresents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChristmasPresents");
        }
    }
}
