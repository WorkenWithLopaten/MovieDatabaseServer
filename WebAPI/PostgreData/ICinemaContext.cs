namespace MovieDb.Data
{
    using PostgreData.Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    public interface ICinemaContext
    {
        DbSet<Cinema> Cinemas { get; set; }
        DbSet<CinemaCity> Cities { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void Dispose();
        int SaveChanges();
    }
}