namespace MovieDb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotationFixed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Adresses", "AdressText", c => c.String(nullable: false));
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Comments", "CommentText", c => c.String(nullable: false, maxLength: 500));
            CreateIndex("dbo.Cities", "Name", unique: true);
            CreateIndex("dbo.Countries", "Name", unique: true);
            CreateIndex("dbo.Users", "UserName", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.Countries", new[] { "Name" });
            DropIndex("dbo.Cities", new[] { "Name" });
            AlterColumn("dbo.Comments", "CommentText", c => c.String(maxLength: 500));
            AlterColumn("dbo.Countries", "Name", c => c.String());
            AlterColumn("dbo.Cities", "Name", c => c.String());
            AlterColumn("dbo.Adresses", "AdressText", c => c.String());
        }
    }
}
