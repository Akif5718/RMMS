namespace RMMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Userrequsrcol_add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRequests", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRequests", "Address");
        }
    }
}
