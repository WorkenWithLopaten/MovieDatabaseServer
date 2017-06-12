namespace MovieDb.Data
{
    using MovieDb.Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class MoviesContext : DbContext, IMoviesContext
    {
        public MoviesContext()
            : base("MovieDbConnection")
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }
        public DbSet<UserData> UsersData { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Adress> Adresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.OnCommentCreating(modelBuilder);
            this.OnCityCreating(modelBuilder);
            this.OnCountryCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void OnCountryCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(new IndexAttribute("IX_Name")
                    { IsUnique = true }));
        }

        private void OnCityCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(new IndexAttribute("IX_Name")
                    { IsUnique = true }));
        }

        private void OnCommentCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .Property(x => x.CommentText)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
