using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BotickrAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AristEntityAddedPropertyImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Artists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Artists");
        }
    }
}
