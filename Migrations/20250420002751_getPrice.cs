using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace retail.Migrations
{
    /// <inheritdoc />
    public partial class getPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Cart");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderDetail",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Cart",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
