namespace MovieDb.Data.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;
    public sealed class ConfigurationActors : DbMigrationsConfiguration<ActorsContext>
    {
        public ConfigurationActors()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ActorsContext context)
        {
            
        }
    }
}
