using System.ComponentModel.DataAnnotations;

namespace NYSM.Models
{
    public class TestConfig
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public AppFile AppFile { get; set; }
        [Required]
        public ReadSpeed ReadSpeed { get; set; }
    }
}