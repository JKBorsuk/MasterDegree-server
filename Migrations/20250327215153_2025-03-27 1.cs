using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterDegree.Migrations
{
    /// <inheritdoc />
    public partial class _202503271 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    WarrantyPeriod = table.Column<int>(type: "int", nullable: false),
                    ProductDimensions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductTags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorSizeVariations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductRatings = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
