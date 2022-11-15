using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Entities
{
    public class IssueEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }

        [Required]
        public int StatusId { get; set; }
        [Required]
        public int CustomerId { get; set; } 

        public StatusEntity Status { get; set; }
        public CustomerEntity Customer { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }
    }
}
