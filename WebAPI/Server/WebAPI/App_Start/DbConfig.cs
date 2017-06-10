﻿namespace WebAPI
{
    using System.Data.Entity;
    using MovieDb.Data;
    using MovieDb.Data.Migrations;
    public static class DbConfig
    {
        public static void Initiliaze()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoviesContext, ConfigurationMovieDB>());
            new MoviesContext().Database.Initialize(true);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ActorsContext, ConfigurationActors>());
            new ActorsContext().Database.Initialize(true);
        }
    }
}