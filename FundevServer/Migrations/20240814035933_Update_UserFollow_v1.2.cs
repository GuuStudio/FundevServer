using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundevServer.Migrations
{
    /// <inheritdoc />
    public partial class Update_UserFollow_v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_AspNetUsers_UserId",
                table: "UserFollows");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserFollows",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFollows_UserId",
                table: "UserFollows",
                newName: "IX_UserFollows_StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_AspNetUsers_StoreId",
                table: "UserFollows",
                column: "StoreId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollows_AspNetUsers_StoreId",
                table: "UserFollows");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "UserFollows",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFollows_StoreId",
                table: "UserFollows",
                newName: "IX_UserFollows_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollows_AspNetUsers_UserId",
                table: "UserFollows",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
