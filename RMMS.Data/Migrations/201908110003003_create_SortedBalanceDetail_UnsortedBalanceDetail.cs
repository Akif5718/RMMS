namespace RMMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_SortedBalanceDetail_UnsortedBalanceDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SortedBalanceDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        U_ID = c.Int(nullable: false),
                        Rice_ID = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RiceTypes", t => t.Rice_ID, cascadeDelete: true)
                .ForeignKey("dbo.UserInfoes", t => t.U_ID, cascadeDelete: true)
                .Index(t => t.U_ID)
                .Index(t => t.Rice_ID);
            
            CreateTable(
                "dbo.UnsortedBalanceDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        U_ID = c.Int(nullable: false),
                        Rice_ID = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RiceTypes", t => t.Rice_ID, cascadeDelete: true)
                .ForeignKey("dbo.UserInfoes", t => t.U_ID, cascadeDelete: true)
                .Index(t => t.U_ID)
                .Index(t => t.Rice_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UnsortedBalanceDetails", "U_ID", "dbo.UserInfoes");
            DropForeignKey("dbo.UnsortedBalanceDetails", "Rice_ID", "dbo.RiceTypes");
            DropForeignKey("dbo.SortedBalanceDetails", "U_ID", "dbo.UserInfoes");
            DropForeignKey("dbo.SortedBalanceDetails", "Rice_ID", "dbo.RiceTypes");
            DropIndex("dbo.UnsortedBalanceDetails", new[] { "Rice_ID" });
            DropIndex("dbo.UnsortedBalanceDetails", new[] { "U_ID" });
            DropIndex("dbo.SortedBalanceDetails", new[] { "Rice_ID" });
            DropIndex("dbo.SortedBalanceDetails", new[] { "U_ID" });
            DropTable("dbo.UnsortedBalanceDetails");
            DropTable("dbo.SortedBalanceDetails");
        }
    }
}
