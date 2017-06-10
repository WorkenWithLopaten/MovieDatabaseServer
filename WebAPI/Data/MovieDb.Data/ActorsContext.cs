namespace MovieDb.Data
{
    using MovieDb.Models;
    using MovieDb.Models.Actors;
    using System.Data.Entity;

    public class ActorsContext : DbContext
    {
        public ActorsContext()
            : base("ActorsConnection")
        {

        }
        public DbSet<MoviesLite> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }

    }
}
