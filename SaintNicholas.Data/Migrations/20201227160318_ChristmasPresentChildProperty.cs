using Microsoft.EntityFrameworkCore.Migrations;

namespace SaintNicholas.Data.Migrations
{
    public partial class ChristmasPresentChildProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Receiver",
                table: "ChristmasPresents",
                newName: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ChristmasPresents_ReceiverId",
                table: "ChristmasPresents",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChristmasPresents_Children_ReceiverId",
                table: "ChristmasPresents",
                column: "ReceiverId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChristmasPresents_Children_ReceiverId",
                table: "ChristmasPresents");

            migrationBuilder.DropIndex(
                name: "IX_ChristmasPresents_ReceiverId",
                table: "ChristmasPresents");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "ChristmasPresents",
                newName: "Receiver");
        }
    }
}
