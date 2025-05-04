using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterDegree.Migrations
{
    /// <inheritdoc />
    public partial class _202503275 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorSizeVariations",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductRatings",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTags",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SKU",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorSizeVariations",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductRatings",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductTags",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
