using System.ComponentModel.DataAnnotations;

namespace NYSM.Models
{
    public class ReadSpeed
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double WordPerSecond { get; set; }
    }
}