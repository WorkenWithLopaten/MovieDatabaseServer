namespace MovieDb.Data
{
    using PostgreData.Models;
    using System.Data.Entity;
    public class CinemaContext : DbContext, ICinemaContext
    {
        public CinemaContext()
            : base("PostgresDotNet")
        {

        }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<CinemaCity> Cities { get; set; }



    }
}
