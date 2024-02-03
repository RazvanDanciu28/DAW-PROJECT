using System.ComponentModel.DataAnnotations;


namespace AngularApp1.Server.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductType { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Color { get; set; }

        public string? Description { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public string PhotoUrl { get; set; }
    }
}
