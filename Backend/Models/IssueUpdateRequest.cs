using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class IssueUpdateRequest
    {
        public int Id { get; set; }
        public DateTime Updated { get; set; }
        public int StatusId { get; set; }
    }
}
