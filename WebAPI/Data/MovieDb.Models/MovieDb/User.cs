using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDb.Models
{
    public class User
    {
        private ICollection<Comment> comments;
        private ICollection<Like> likes;
        private ICollection<Dislike> dislikes;

        public User()
        {
            this.comments = new HashSet<Comment>();
            this.dislikes = new HashSet<Dislike>();
            this.likes = new HashSet<Like>();
            this.Expire = false;
        }
        public int Id { get; set; }
       
        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string UserName { get; set; }

        public bool Expire { get; set; }

        public virtual UserData UserData { get; set; }

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
