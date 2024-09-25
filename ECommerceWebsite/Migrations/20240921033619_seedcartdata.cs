using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWebsite.Migrations
{
    /// <inheritdoc />
    public partial class seedcartdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "carts",
                columns: new[] { "Id", "ApplicationUserId", "Count", "ProductId" },
                values: new object[] { 1, "940aad94-45ca-465e-9449-38dc0250ba56", 3, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "carts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
