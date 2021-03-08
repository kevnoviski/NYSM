using System.ComponentModel.DataAnnotations;

namespace NYSM.Models
{
    public class AppFile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public int LineCount { get; set; }
        [Required]
        public int WordCount { get; set; }
    }
}