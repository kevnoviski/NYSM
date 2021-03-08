using System.ComponentModel.DataAnnotations;

namespace NYSM.Models
{
    public  class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Password { get; set; }
    }
}