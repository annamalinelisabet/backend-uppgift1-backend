using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class IssueResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public StatusResponse Status { get; set; }
        public CustomerResponse Customer { get; set; }
        public IEnumerable<CommentResponse> Comments { get; set; }
    }
}
