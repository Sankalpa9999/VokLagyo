using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce_Website.Migrations
{
    /// <inheritdoc />
    public partial class orderrating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_order_cart_id",
                table: "tbl_order");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_cart_id",
                table: "tbl_order",
                column: "cart_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_order_cart_id",
                table: "tbl_order");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_cart_id",
                table: "tbl_order",
                column: "cart_id");
        }
    }
}
