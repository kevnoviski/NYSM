using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
        [MaxLength(150)]
    
        public string Email { get; set; }
        [Required]
        [MaxLength(500)]
        public string Password { get; set; }
    }
}