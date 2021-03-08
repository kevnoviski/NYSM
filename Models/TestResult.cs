using System.ComponentModel.DataAnnotations;

namespace NYSM.Models
{
    public class TestResult
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public TestConfig TestcCnfig { get; set; }
        [Required]
        public double Percentage { get; set; }
    }
}