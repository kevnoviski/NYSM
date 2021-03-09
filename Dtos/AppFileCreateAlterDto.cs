using System.ComponentModel.DataAnnotations;

namespace NYSM.Dtos
{
    public class AppFileCreateAlterDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}