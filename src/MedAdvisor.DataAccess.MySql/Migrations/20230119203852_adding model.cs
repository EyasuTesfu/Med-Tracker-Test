using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAdvisor.DataAccess.MySql.Migrations
{
    public partial class addingmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_Id",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Documents",
                newName: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_UserId",
                table: "Documents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_UserId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_UserId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "Documents",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_Id",
                table: "Documents",
                column: "Id",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
