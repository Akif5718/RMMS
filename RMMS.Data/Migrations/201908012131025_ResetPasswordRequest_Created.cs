namespace RMMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResetPasswordRequest_Created : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResetPasswordRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RequestOn = c.DateTime(nullable: false),
                        GUID = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserInfoes", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResetPasswordRequests", "UserID", "dbo.UserInfoes");
            DropIndex("dbo.ResetPasswordRequests", new[] { "UserID" });
            DropTable("dbo.ResetPasswordRequests");
        }
    }
}
