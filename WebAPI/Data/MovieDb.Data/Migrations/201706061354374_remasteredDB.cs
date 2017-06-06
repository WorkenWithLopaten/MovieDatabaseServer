namespace MovieDb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remasteredDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdressText = c.String(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserDatas",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        isMale = c.Boolean(nullable: false),
                        AdressId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adresses", t => t.AdressId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.AdressId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Expire = c.Boolean(nullable: false),
                        UserDataId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentText = c.String(maxLength: 500),
                        CreatedOn = c.DateTime(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        LikesNumber = c.Int(nullable: false),
                        DislikesNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dislikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDatas", "Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Likes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Likes", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Dislikes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Dislikes", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Comments", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.UserDatas", "AdressId", "dbo.Adresses");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Adresses", "CityId", "dbo.Cities");
            DropIndex("dbo.Likes", new[] { "MovieId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropIndex("dbo.Dislikes", new[] { "MovieId" });
            DropIndex("dbo.Dislikes", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "MovieId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.UserDatas", new[] { "AdressId" });
            DropIndex("dbo.UserDatas", new[] { "Id" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Adresses", new[] { "CityId" });
            DropTable("dbo.Likes");
            DropTable("dbo.Dislikes");
            DropTable("dbo.Movies");
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
            DropTable("dbo.UserDatas");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Adresses");
        }
    }
}
