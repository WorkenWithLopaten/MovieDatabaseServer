using System.ComponentModel.DataAnnotations;

namespace MovieDb.Models
{
    public class Like
    {
        public int Id { get; set; }
        [Required]
        public virtual int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
