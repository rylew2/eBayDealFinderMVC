namespace eBayDealFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustSellingState : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EbayDatas", "sellingState", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EbayDatas", "sellingState", c => c.String());
        }
    }
}
