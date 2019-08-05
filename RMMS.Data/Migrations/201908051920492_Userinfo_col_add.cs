namespace RMMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Userinfo_col_add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "UserCode", c => c.String(nullable: false));
            AddColumn("dbo.UserInfoes", "Address", c => c.String(nullable: false));
            AddColumn("dbo.UserInfoes", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfoes", "IsActive");
            DropColumn("dbo.UserInfoes", "Address");
            DropColumn("dbo.UserInfoes", "UserCode");
        }
    }
}
