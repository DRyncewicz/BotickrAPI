using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BotickrAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionOfDatabaseRelationConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Tickets_TicketId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EventArtists_Artists_ArtistId",
                table: "EventArtists");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_TicketId",
                table: "BookingDetails");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_EventId",
                table: "Bookings",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Tickets_BookingId",
                table: "BookingDetails",
                column: "BookingId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Events_EventId",
                table: "Bookings",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventArtists_Artists_ArtistId",
                table: "EventArtists",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Tickets_BookingId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Events_EventId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_EventArtists_Artists_ArtistId",
                table: "EventArtists");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_EventId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_TicketId",
                table: "BookingDetails",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Tickets_TicketId",
                table: "BookingDetails",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventArtists_Artists_ArtistId",
                table: "EventArtists",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
