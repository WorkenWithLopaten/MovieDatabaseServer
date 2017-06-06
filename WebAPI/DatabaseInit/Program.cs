using MovieDb.Data;
using MovieDb.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInit
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoviesContext, ConfigurationMovieDB>());
            var db = new MoviesContext();
            db.Adresses.ToList();
        }
    }
}
