namespace SqlLiteData.Migrations
{
    using MovieDb.Data;
    using System.Data.Entity.Migrations;
    using System.Data.SQLite.EF6.Migrations;
    public sealed class ActorsConfiguration : DbMigrationsConfiguration<ActorsContext>
    {
        public ActorsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
        }

        protected override void Seed(ActorsContext context)
        {
        }
    }
}
