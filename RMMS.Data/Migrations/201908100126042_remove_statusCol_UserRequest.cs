namespace RMMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_statusCol_UserRequest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserRequests", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRequests", "Status", c => c.Boolean(nullable: false));
        }
    }
}
