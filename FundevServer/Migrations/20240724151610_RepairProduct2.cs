using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundevServer.Migrations
{
    /// <inheritdoc />
    public partial class RepairProduct2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PathImage",
                table: "Products",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "PathImage");
        }
    }
}
