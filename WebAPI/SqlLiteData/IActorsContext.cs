namespace MovieDb.Data
{
    using System.Data.Entity;
    using SqlLiteData.Models;
    public interface IActorsContext
    {
        DbSet<MoviesLite> Movies { get; set; }
        DbSet<Actors> Actors { get; set; }
        void Dispose();
        int SaveChanges();
    }
}