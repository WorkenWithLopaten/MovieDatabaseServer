namespace SqlLiteData.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Actors
    {
        private ICollection<MoviesLite> movies;
        public Actors()
        {
            this.Movies = new HashSet<MoviesLite>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual ICollection<MoviesLite> Movies
        {
            get { return this.movies; }
            set { this.movies = value; }
        }
    }
}
