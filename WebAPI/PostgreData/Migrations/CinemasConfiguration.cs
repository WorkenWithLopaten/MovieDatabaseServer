namespace PostgreData.Migrations
{
    using MovieDb.Data;
    using System.Data.Entity.Migrations;

    public sealed class CinemasConfiguration : DbMigrationsConfiguration<CinemaContext>
    {
        public CinemasConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CinemaContext context)
        {
            
        }
    }
}
