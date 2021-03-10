using Microsoft.EntityFrameworkCore.Migrations;

namespace Frigo_API_DB.Migrations
{
    public partial class FirstSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hoeveelheden",
                columns: new[] { "Id", "Aantal", "Naam" },
                values: new object[] { 1, 0, "Cola-blik" });

            migrationBuilder.InsertData(
                table: "Hoeveelheden",
                columns: new[] { "Id", "Aantal", "Naam" },
                values: new object[] { 2, 0, "Fanta" });

            migrationBuilder.InsertData(
                table: "Hoeveelheden",
                columns: new[] { "Id", "Aantal", "Naam" },
                values: new object[] { 3, 0, "Sprite-Lemon-blik" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hoeveelheden",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hoeveelheden",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hoeveelheden",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
