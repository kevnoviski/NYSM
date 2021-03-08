using System.ComponentModel.DataAnnotations;

namespace NYSM.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public AppFile AppFile { get; set; }
        [Required]
        [MaxLength(5000)]
        public string QuestionContent { get; set; }
        [Required]
        [MaxLength(5000)]
        public string CorretAnwser { get; set; }
        [Required]
        [MaxLength(5000)]
        public string Alternative1 { get; set; }
        [Required]
        [MaxLength(5000)]
        public string Alternative2 { get; set; }
        [Required]
        [MaxLength(5000)]
        public string Alternative3 { get; set; }
        [Required]
        [MaxLength(5000)]
        public string Alternative4 { get; set; }

    }
}