namespace MovieDb.Data
{
    using System.Data.Entity;
    using SqlLiteData.Models;
    using System.Data.Entity.Infrastructure;
    public interface IActorsContext
    {
        DbSet<MoviesLite> Movies { get; set; }
        DbSet<Actors> Actors { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void Dispose();
        int SaveChanges();
    }
}