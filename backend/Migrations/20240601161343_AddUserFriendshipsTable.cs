using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFriendshipsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriendships_Users_FriendId",
                table: "UserFriendships");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriendships_Users_UserId",
                table: "UserFriendships");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserFriendships");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserFriendships",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "UserFriendships",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriendships_Users_FriendId",
                table: "UserFriendships",
                column: "FriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriendships_Users_UserId",
                table: "UserFriendships",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriendships_Users_FriendId",
                table: "UserFriendships");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriendships_Users_UserId",
                table: "UserFriendships");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserFriendships");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "UserFriendships");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserFriendships",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriendships_Users_FriendId",
                table: "UserFriendships",
                column: "FriendId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriendships_Users_UserId",
                table: "UserFriendships",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
