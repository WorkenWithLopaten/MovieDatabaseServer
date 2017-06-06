namespace MovieDb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserDatas", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDatas", "UserId", c => c.Int(nullable: false));
        }
    }
}
