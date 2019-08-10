namespace RMMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userinfo_usercodeType_change : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfoes", "UserCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfoes", "UserCode", c => c.String(nullable: false));
        }
    }
}
