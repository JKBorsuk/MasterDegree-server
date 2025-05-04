using System.ComponentModel.DataAnnotations;

namespace MasterDegree.Models
{
    public class UserFavorites
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required User User { get; set; }
        public int UserId { get; set; }
        [Required]
        public required Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
