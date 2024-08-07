using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleRabbitMQ.Watermark.Models
{
	public class Product
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName ="Decimal(9,2)")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
    }
}
