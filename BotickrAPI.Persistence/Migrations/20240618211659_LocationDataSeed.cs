using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BotickrAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LocationDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Capacity", "City", "Created", "CreatedBy", "Inactivated", "InactivatedBy", "Modified", "ModifiedBy", "StatusId", "Venue" },
                values: new object[,]
                {
                    { 1, 3800, "Koszalin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", null, null, null, null, 1, "Hala Widowiskowo-Sportowa" },
                    { 2, 80000, "Warszawa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", null, null, null, null, 1, "PGE Narodowy" },
                    { 3, 15328, "Gdańsk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", null, null, null, null, 1, "Stadion Energa Gdańsk" },
                    { 4, 18000, "Kraków", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", null, null, null, null, 1, "Tauron Arena Kraków" },
                    { 5, 7500, "Wrocław", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", null, null, null, null, 1, "Hala Stulecia" },
                    { 6, 5000, "Poznań", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", null, null, null, null, 1, "Hala Arena" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
