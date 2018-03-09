namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmoretables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beers",
                c => new
                    {
                        BeerId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Type = c.String(),
                        ABV = c.Double(nullable: false),
                        Customer_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.BeerId)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .Index(t => t.Customer_CustomerId);
            
            CreateTable(
                "dbo.Distributors",
                c => new
                    {
                        DistributorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DistributorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Beers", "Customer_CustomerId", "dbo.Customers");
            DropIndex("dbo.Beers", new[] { "Customer_CustomerId" });
            DropTable("dbo.Distributors");
            DropTable("dbo.Beers");
        }
    }
}
