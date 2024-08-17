using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundevServer.Migrations
{
    /// <inheritdoc />
    public partial class Update_CartItem_v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CartItems",
                newName: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "CartItems",
                newName: "UserId");
        }
    }
}
