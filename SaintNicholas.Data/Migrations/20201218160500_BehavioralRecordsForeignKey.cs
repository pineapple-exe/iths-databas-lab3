using Microsoft.EntityFrameworkCore.Migrations;

namespace SaintNicholas.Data.Migrations
{
    public partial class BehavioralRecordsForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_BehavioralRecords_Children_ChildID",
                table: "BehavioralRecords",
                column: "ChildID",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BehavioralRecords_Children_ChildID",
                table: "BehavioralRecords");
        }
    }
}
