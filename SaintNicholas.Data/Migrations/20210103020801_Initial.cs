using Microsoft.EntityFrameworkCore.Migrations;

namespace SaintNicholas.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    City = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                });

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
                    table.ForeignKey(
                        name: "FK_BehavioralRecords_Children_ChildID",
                        column: x => x.ChildID,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChristmasPresents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contents = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ForGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForNaughtyChild = table.Column<bool>(type: "bit", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: true),
                    HandOutYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChristmasPresents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChristmasPresents_Children_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChristmasPresents_ReceiverId",
                table: "ChristmasPresents",
                column: "ReceiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BehavioralRecords");

            migrationBuilder.DropTable(
                name: "ChristmasPresents");

            migrationBuilder.DropTable(
                name: "Children");
        }
    }
}
