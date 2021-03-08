using System.ComponentModel.DataAnnotations;

namespace NYSM.Dtos
{
    public class UserReadDto
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }
    }
}