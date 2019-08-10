namespace RMMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Admin_Employee_customer_RiceType_ExtraType_ExtraBalanceDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        A_ID = c.Int(nullable: false),
                        ExtraBalance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.A_ID)
                .ForeignKey("dbo.UserInfoes", t => t.A_ID)
                .Index(t => t.A_ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        C_ID = c.Int(nullable: false),
                        SortedBalance = c.Double(nullable: false),
                        UnsortedBalance = c.Double(nullable: false),
                        Value = c.Double(nullable: false),
                        Rate = c.Double(nullable: false),
                        ExtraBalance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.C_ID)
                .ForeignKey("dbo.UserInfoes", t => t.C_ID)
                .Index(t => t.C_ID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        E_ID = c.Int(nullable: false),
                        Salary = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.E_ID)
                .ForeignKey("dbo.UserInfoes", t => t.E_ID)
                .Index(t => t.E_ID);
            
            CreateTable(
                "dbo.ExtraBalanceDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        U_ID = c.Int(nullable: false),
                        Ex_ID = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExtraTypes", t => t.Ex_ID, cascadeDelete: true)
                .ForeignKey("dbo.UserInfoes", t => t.U_ID, cascadeDelete: true)
                .Index(t => t.U_ID)
                .Index(t => t.Ex_ID);
            
            CreateTable(
                "dbo.ExtraTypes",
                c => new
                    {
                        Extra_ID = c.Int(nullable: false, identity: true),
                        ExtraName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Extra_ID);
            
            CreateTable(
                "dbo.RiceTypes",
                c => new
                    {
                        Rice_ID = c.Int(nullable: false, identity: true),
                        RiceName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Rice_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExtraBalanceDetails", "U_ID", "dbo.UserInfoes");
            DropForeignKey("dbo.ExtraBalanceDetails", "Ex_ID", "dbo.ExtraTypes");
            DropForeignKey("dbo.Employees", "E_ID", "dbo.UserInfoes");
            DropForeignKey("dbo.Customers", "C_ID", "dbo.UserInfoes");
            DropForeignKey("dbo.Admins", "A_ID", "dbo.UserInfoes");
            DropIndex("dbo.ExtraBalanceDetails", new[] { "Ex_ID" });
            DropIndex("dbo.ExtraBalanceDetails", new[] { "U_ID" });
            DropIndex("dbo.Employees", new[] { "E_ID" });
            DropIndex("dbo.Customers", new[] { "C_ID" });
            DropIndex("dbo.Admins", new[] { "A_ID" });
            DropTable("dbo.RiceTypes");
            DropTable("dbo.ExtraTypes");
            DropTable("dbo.ExtraBalanceDetails");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
            DropTable("dbo.Admins");
        }
    }
}
