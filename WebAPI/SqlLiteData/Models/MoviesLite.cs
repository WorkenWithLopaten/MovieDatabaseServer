namespace SqlLiteData.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class MoviesLite
    {
        private ICollection<Actors> actors;
        public MoviesLite()
        {
            this.Actors = new HashSet<Actors>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual ICollection<Actors> Actors
        {
            get { return this.actors; }
            set { this.actors = value; }
        }
    }
}
