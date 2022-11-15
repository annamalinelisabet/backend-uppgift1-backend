using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Entities
{
    public class CustomerEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<IssueEntity> Issues { get; set; }     
        public ICollection<CommentEntity> Comments { get; set; }
    }
}
