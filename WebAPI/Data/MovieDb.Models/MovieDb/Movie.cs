using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDb.Models
{
    public class Movie
    {
        private ICollection<Comment> comments;
        private ICollection<Like> likes;
        private ICollection<Dislike> dislikes;

        public Movie()
        {
            this.LikesNumber = 0;
            this.DislikesNumber = 0;
            this.Comments = new HashSet<Comment>();
            this.Dislikes = new HashSet<Dislike>();
            this.Likes = new HashSet<Like>();

        }
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public int LikesNumber { get; set; }

        public int DislikesNumber { get; set; }
        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Like> Likes
        {
            get { return this.likes; }
            set { this.likes = value; }
        }

        public virtual ICollection<Dislike> Dislikes
        {
            get { return this.dislikes; }
            set { this.dislikes = value; }
        }
    }
}
