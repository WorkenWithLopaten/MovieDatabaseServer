using System.Data.Entity;
using MovieDb.Models;
using System.Data.Entity.Infrastructure;

namespace MovieDb.Data
{
    public interface IMoviesContext
    {
        DbSet<Comment> Comments { get; set; }
        DbSet<Dislike> Dislikes { get; set; }
        DbSet<Like> Likes { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserData> UsersData { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Adress> Adresses { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void Dispose();

        int SaveChanges();
    }
}