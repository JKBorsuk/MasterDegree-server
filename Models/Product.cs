using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace MasterDegree.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCategory { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int WarrantyPeriod { get; set; }
        public string? ProductDimensions { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
