using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public int CustomerId { get; set; }
    }
}
