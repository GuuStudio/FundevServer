using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundevServer.Migrations
{
    /// <inheritdoc />
    public partial class Update_Orders_v15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CanceledDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanceledDate",
                table: "Orders");
        }
    }
}
