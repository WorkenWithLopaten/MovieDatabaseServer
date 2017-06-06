namespace MovieDb.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Comment
    {
        public Comment()
        {
            this.CreatedOn = DateTime.Now;
            this.isDeleted = false;
        }
        public int Id
        {
            get; set;
        }

        public string CommentText { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool isDeleted { get; set; }

        public virtual int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

    }
}
