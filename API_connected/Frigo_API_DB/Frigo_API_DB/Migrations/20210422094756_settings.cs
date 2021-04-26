using Microsoft.EntityFrameworkCore.Migrations;

namespace Frigo_API_DB.Migrations
{
    public partial class settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SettingsTable",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmailForNotifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinumumAmount = table.Column<int>(type: "int", nullable: false),
                    RecieveNotification = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingsTable", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SettingsTable");
        }
    }
}
