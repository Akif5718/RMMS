namespace RMMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userinfo_userrequest_coledit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "Created_at", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserRequests", "Approved_at");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRequests", "Approved_at", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserInfoes", "Created_at");
        }
    }
}
