using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace retail.Migrations
{
    /// <inheritdoc />
    public partial class CustomerOrderUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentOrderIndex",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentOrderIndex",
                table: "Customer");
        }
    }
}
