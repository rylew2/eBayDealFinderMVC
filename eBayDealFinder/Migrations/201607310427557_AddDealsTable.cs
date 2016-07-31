namespace eBayDealFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDealsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumID = c.Int(nullable: false, identity: true),
                        MyProperty = c.String(),
                    })
                .PrimaryKey(t => t.AlbumID);
            
            CreateTable(
                "dbo.DealData",
                c => new
                    {
                        DealDataID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.DealDataID);
            
            CreateTable(
                "dbo.Deals",
                c => new
                    {
                        DealID = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        price = c.Double(nullable: false),
                        pub = c.DateTimeOffset(nullable: false, precision: 7),
                        compEbayPrice = c.Double(nullable: false),
                        dealURL = c.String(),
                        DealData_DealDataID = c.Int(),
                    })
                .PrimaryKey(t => t.DealID)
                .ForeignKey("dbo.DealData", t => t.DealData_DealDataID)
                .Index(t => t.DealData_DealDataID);
            
            DropTable("dbo.EbayData");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EbayData",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        title = c.String(),
                        bidCount = c.Int(nullable: false),
                        sellingState = c.String(maxLength: 100),
                        endPrice = c.Double(nullable: false),
                        condID = c.Int(nullable: false),
                        condDisplayName = c.String(),
                        listingType = c.String(),
                        endTime = c.DateTime(nullable: false),
                        categoryID = c.String(),
                        categoryName = c.String(),
                        ebayURL = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            DropForeignKey("dbo.Deals", "DealData_DealDataID", "dbo.DealData");
            DropIndex("dbo.Deals", new[] { "DealData_DealDataID" });
            DropTable("dbo.Deals");
            DropTable("dbo.DealDatas");
            DropTable("dbo.Albums");
        }
    }
}
