namespace RMMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userInfo_Add_Name : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfoes", "Name");
        }
    }
}
