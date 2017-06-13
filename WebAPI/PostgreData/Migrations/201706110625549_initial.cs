namespace PostgreData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cinemas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CinemaCities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CinemaCityCinemas",
                c => new
                    {
                        CinemaCity_Id = c.Int(nullable: false),
                        Cinema_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CinemaCity_Id, t.Cinema_Id })
                .ForeignKey("dbo.CinemaCities", t => t.CinemaCity_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cinemas", t => t.Cinema_Id, cascadeDelete: true)
                .Index(t => t.CinemaCity_Id)
                .Index(t => t.Cinema_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CinemaCityCinemas", "Cinema_Id", "dbo.Cinemas");
            DropForeignKey("dbo.CinemaCityCinemas", "CinemaCity_Id", "dbo.CinemaCities");
            DropIndex("dbo.CinemaCityCinemas", new[] { "Cinema_Id" });
            DropIndex("dbo.CinemaCityCinemas", new[] { "CinemaCity_Id" });
            DropTable("dbo.CinemaCityCinemas");
            DropTable("dbo.CinemaCities");
            DropTable("dbo.Cinemas");
        }
    }
}
