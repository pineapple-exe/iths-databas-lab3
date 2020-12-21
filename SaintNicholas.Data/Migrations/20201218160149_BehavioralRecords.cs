using Microsoft.EntityFrameworkCore.Migrations;

namespace SaintNicholas.Data.Migrations
{
    public partial class BehavioralRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BehavioralRecords",
                columns: table => new
                {
                    ChildID = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Naughty = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehavioralRecords", x => new { x.ChildID, x.Year });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BehavioralRecords");
        }
    }
}
