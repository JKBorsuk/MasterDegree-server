using System.ComponentModel.DataAnnotations;

namespace MasterDegree.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Email {  get; set; }
        [Required]
        [StringLength(100)]
        public required string Password { get; set; }
        public List<UserFavorites> Favorites { get; set; } = [];
    }
}
