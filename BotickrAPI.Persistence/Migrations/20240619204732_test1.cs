using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BotickrAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventArtists_Artists_ArtistId",
                table: "EventArtists");

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
                name: "FK_EventArtists_Artists_ArtistId",
                table: "EventArtists");

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
