namespace MovieDb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalFixForONEtoOne : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserDatas", name: "Id", newName: "UserId");
            RenameIndex(table: "dbo.UserDatas", name: "IX_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserDatas", name: "IX_UserId", newName: "IX_Id");
            RenameColumn(table: "dbo.UserDatas", name: "UserId", newName: "Id");
        }
    }
}
