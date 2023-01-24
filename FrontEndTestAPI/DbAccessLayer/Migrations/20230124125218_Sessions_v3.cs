using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEndTestAPI.DbAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Sessionsv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSessions_AspNetUsers_ApplicationUserId",
                table: "AppSessions");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "AppSessions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AppSessions_ApplicationUserId",
                table: "AppSessions",
                newName: "IX_AppSessions_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSessions_AspNetUsers_UserId",
                table: "AppSessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSessions_AspNetUsers_UserId",
                table: "AppSessions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AppSessions",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AppSessions_UserId",
                table: "AppSessions",
                newName: "IX_AppSessions_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSessions_AspNetUsers_ApplicationUserId",
                table: "AppSessions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
