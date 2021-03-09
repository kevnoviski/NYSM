using System.ComponentModel.DataAnnotations;
using NYSM.Models;

namespace NYSM.Dtos
{
    public class ReadSpeedDto 
    {
        [Required]
        public double WordPerSecond { get; set; }
    }
}