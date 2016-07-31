namespace eBayDealFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SwitchFromDealData_ToDeals : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deals", "DealData_DealDataID", "dbo.DealData");
            DropIndex("dbo.Deals", new[] { "DealData_DealDataID" });
            DropColumn("dbo.Deals", "DealData_DealDataID");
            DropTable("dbo.DealData");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DealData",
                c => new
                    {
                        DealDataID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.DealDataID);
            
            AddColumn("dbo.Deals", "DealData_DealDataID", c => c.Int());
            CreateIndex("dbo.Deals", "DealData_DealDataID");
            AddForeignKey("dbo.Deals", "DealData_DealDataID", "dbo.DealData", "DealDataID");
        }
    }
}
