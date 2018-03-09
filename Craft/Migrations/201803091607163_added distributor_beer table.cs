namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddistributor_beertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Distributor_Beer",
                c => new
                    {
                        DistributorId = c.Int(nullable: false),
                        BeerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DistributorId)
                .ForeignKey("dbo.Beers", t => t.BeerId, cascadeDelete: true)
                .ForeignKey("dbo.Distributors", t => t.DistributorId)
                .Index(t => t.DistributorId)
                .Index(t => t.BeerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Distributor_Beer", "DistributorId", "dbo.Distributors");
            DropForeignKey("dbo.Distributor_Beer", "BeerId", "dbo.Beers");
            DropIndex("dbo.Distributor_Beer", new[] { "BeerId" });
            DropIndex("dbo.Distributor_Beer", new[] { "DistributorId" });
            DropTable("dbo.Distributor_Beer");
        }
    }
}
