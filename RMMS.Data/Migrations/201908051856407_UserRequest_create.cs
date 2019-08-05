namespace RMMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRequest_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Approved_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserRequests");
        }
    }
}
