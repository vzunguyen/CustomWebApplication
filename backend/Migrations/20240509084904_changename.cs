using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class changename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Users_AuthorId",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Content",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_AuthorId",
                table: "Content",
                newName: "IX_Content_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Users_UserId",
                table: "Content",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Users_UserId",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Content",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_UserId",
                table: "Content",
                newName: "IX_Content_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Users_AuthorId",
                table: "Content",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
