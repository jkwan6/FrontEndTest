using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEndTestAPI.DbAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Sessionv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppSessionId",
                table: "RefreshTokens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppSessionId",
                table: "RefreshTokens",
                column: "AppSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AppSessions_AppSessionId",
                table: "RefreshTokens",
                column: "AppSessionId",
                principalTable: "AppSessions",
                principalColumn: "AppSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AppSessions_AppSessionId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_AppSessionId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "AppSessionId",
                table: "RefreshTokens");
        }
    }
}
