using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourSoundCompnay.RelationalData.Migrations
{
    /// <inheritdoc />
    public partial class _13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "User",
                newName: "UrlImageProfile");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "User",
                newName: "NumberPhone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlImageProfile",
                table: "User",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "NumberPhone",
                table: "User",
                newName: "RefreshToken");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "User",
                type: "text",
                nullable: true);
        }
    }
}
