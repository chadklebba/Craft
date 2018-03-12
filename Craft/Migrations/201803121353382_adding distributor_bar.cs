namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingdistributor_bar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Distributor_Bar",
                c => new
                    {
                        DistributorId = c.Int(nullable: false),
                        BarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DistributorId)
                .ForeignKey("dbo.Bars", t => t.BarId, cascadeDelete: true)
                .ForeignKey("dbo.Distributors", t => t.DistributorId)
                .Index(t => t.DistributorId)
                .Index(t => t.BarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Distributor_Bar", "DistributorId", "dbo.Distributors");
            DropForeignKey("dbo.Distributor_Bar", "BarId", "dbo.Bars");
            DropIndex("dbo.Distributor_Bar", new[] { "BarId" });
            DropIndex("dbo.Distributor_Bar", new[] { "DistributorId" });
            DropTable("dbo.Distributor_Bar");
        }
    }
}
