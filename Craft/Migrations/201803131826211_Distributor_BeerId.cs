namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Distributor_BeerId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Distributor_Beer", "DistributorId", "dbo.Distributors");
            DropPrimaryKey("dbo.Distributor_Beer");
            AddColumn("dbo.Distributor_Beer", "Distributor_BeerId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Distributor_Beer", "Distributor_BeerId");
            AddForeignKey("dbo.Distributor_Beer", "DistributorId", "dbo.Distributors", "DistributorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Distributor_Beer", "DistributorId", "dbo.Distributors");
            DropPrimaryKey("dbo.Distributor_Beer");
            DropColumn("dbo.Distributor_Beer", "Distributor_BeerId");
            AddPrimaryKey("dbo.Distributor_Beer", "DistributorId");
            AddForeignKey("dbo.Distributor_Beer", "DistributorId", "dbo.Distributors", "DistributorId");
        }
    }
}
