namespace MovieDb.Data
{
    using System.Data.Entity;
    using SqlLiteData.Models;
    public class ActorsContext : DbContext, IActorsContext
    {
        public ActorsContext()
            : base("ActorsConnection")
        {

        }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<MoviesLite> Movies { get; set; }

    }
}
