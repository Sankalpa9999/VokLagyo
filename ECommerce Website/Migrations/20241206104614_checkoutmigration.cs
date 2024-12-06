using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce_Website.Migrations
{
    /// <inheritdoc />
    public partial class checkoutmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_checked_out",
                table: "tbl_cart",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_checked_out",
                table: "tbl_cart");
        }
    }
}
