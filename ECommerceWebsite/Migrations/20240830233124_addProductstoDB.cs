using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceWebsite.Migrations
{
    /// <inheritdoc />
    public partial class addProductstoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Brand", "Description", "ListPrice", "Price", "Price100", "Price50", "ProductName" },
                values: new object[,]
                {
                    { 1, "CozyWool", "A soft and warm 100% wool yarn bundle ideal for knitting scarves, sweaters, and blankets. This bundle includes 5 skeins in various colors, providing ample material for multiple projects.", 49.990000000000002, 44.990000000000002, 39.990000000000002, 42.990000000000002, "CozyWool Knitting Yarn Bundle" },
                    { 2, "Artist’s Choice", "Premium watercolor paint set with 24 vibrant colors, perfect for both beginners and professionals. The high-pigment paints blend seamlessly, providing rich and smooth coverage for all your watercolor projects.", 39.990000000000002, 34.990000000000002, 30.989999999999998, 32.990000000000002, "Watercolor Paint Set" },
                    { 3, "CandleCraft", "This DIY kit includes everything you need to create your own scented candles at home. The kit comes with soy wax, wicks, essential oils, and reusable containers, making it perfect for crafting unique, personalized candles.", 59.990000000000002, 54.990000000000002, 49.990000000000002, 52.990000000000002, "CandleCraft DIY Candle Making Kit" },
                    { 4, "VibrantColors", "A versatile acrylic paint set with 12 tubes of richly pigmented colors, perfect for all types of art projects. These fast-drying paints are suitable for use on canvas, wood, fabric, and more.", 29.989999999999998, 24.989999999999998, 20.989999999999998, 22.989999999999998, "VibrantColors Acrylic Paint Set" },
                    { 5, "KnitPro", " A set of 10 high-quality bamboo knitting needles in various sizes. Lightweight and smooth, these needles provide a comfortable knitting experience and are ideal for beginners and seasoned knitters alike.", 39.990000000000002, 34.990000000000002, 29.989999999999998, 32.990000000000002, "KnitPro Bamboo Needle Set" },
                    { 6, "PureCraft", "Create your own luxurious soaps with this DIY kit. It includes natural soap bases, molds, essential oils, and colorants, allowing you to craft custom soaps at home with ease.", 49.990000000000002, 44.990000000000002, 39.990000000000002, 42.990000000000002, "PureCraft DIY Soap Making Kit" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
