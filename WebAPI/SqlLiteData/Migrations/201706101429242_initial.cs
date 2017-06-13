namespace SqlLiteData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MoviesLites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MoviesLiteActors",
                c => new
                    {
                        MoviesLite_Id = c.Int(nullable: false),
                        Actors_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MoviesLite_Id, t.Actors_Id })
                .ForeignKey("dbo.MoviesLites", t => t.MoviesLite_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actors_Id, cascadeDelete: true)
                .Index(t => t.MoviesLite_Id)
                .Index(t => t.Actors_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MoviesLiteActors", "Actors_Id", "dbo.Actors");
            DropForeignKey("dbo.MoviesLiteActors", "MoviesLite_Id", "dbo.MoviesLites");
            DropIndex("dbo.MoviesLiteActors", new[] { "Actors_Id" });
            DropIndex("dbo.MoviesLiteActors", new[] { "MoviesLite_Id" });
            DropTable("dbo.MoviesLiteActors");
            DropTable("dbo.MoviesLites");
            DropTable("dbo.Actors");
        }
    }
}
