namespace WebAPI
{
    using System.Data.Entity;
    using MovieDb.Data;
    using MovieDb.Data.Migrations;
    using SqlLiteData.Migrations;
    using PostgreData.Migrations;
    public static class DbConfig
    {
        public static void Initiliaze()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoviesContext, ConfigurationMovieDB>());
            new MoviesContext().Database.Initialize(true);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ActorsContext, ActorsConfiguration>(true));
            new ActorsContext().Database.Initialize(true);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CinemaContext, CinemasConfiguration>(true));
            new CinemaContext().Database.Initialize(true);
        }
    }
}