using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEndTestAPI.DbAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Sessionv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSessions_AspNetUsers_UserId",
                table: "AppSessions");

            migrationBuilder.DropIndex(
                name: "IX_AppSessions_UserId",
                table: "AppSessions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppSessions");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AppSessions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AppSessions_ApplicationUserId",
                table: "AppSessions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSessions_AspNetUsers_ApplicationUserId",
                table: "AppSessions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSessions_AspNetUsers_ApplicationUserId",
                table: "AppSessions");

            migrationBuilder.DropIndex(
                name: "IX_AppSessions_ApplicationUserId",
                table: "AppSessions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AppSessions");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AppSessions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSessions_UserId",
                table: "AppSessions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSessions_AspNetUsers_UserId",
                table: "AppSessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
