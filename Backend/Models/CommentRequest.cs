using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class CommentRequest
    {
        public string Comment { get; set; }
        public int IssueId { get; set; }
        public int CustomerId { get; set; }
    }
}
