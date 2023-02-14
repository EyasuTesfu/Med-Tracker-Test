using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAdvisor.DataAccess.MySql.Migrations
{
    public partial class updatingprofilefield2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "UserProfiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Other",
                table: "UserProfiles");
        }
    }
}
