using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class IssueRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
    }
}
