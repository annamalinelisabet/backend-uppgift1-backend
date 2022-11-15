using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class StatusRequest
    {
        [Required]
        public string Status { get; set; }
    }
}
