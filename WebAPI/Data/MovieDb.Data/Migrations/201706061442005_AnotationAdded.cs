namespace MovieDb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotationAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Adresses", "AdressText", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Adresses", "AdressText", c => c.String(nullable: false));
        }
    }
}
