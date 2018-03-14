namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keyaddedinDistributor_Bar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Distributor_Bar", "DistributorId", "dbo.Distributors");
            DropPrimaryKey("dbo.Distributor_Bar");
            AddColumn("dbo.Distributor_Bar", "Distributor_BarId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Distributor_Bar", "Distributor_BarId");
            AddForeignKey("dbo.Distributor_Bar", "DistributorId", "dbo.Distributors", "DistributorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Distributor_Bar", "DistributorId", "dbo.Distributors");
            DropPrimaryKey("dbo.Distributor_Bar");
            DropColumn("dbo.Distributor_Bar", "Distributor_BarId");
            AddPrimaryKey("dbo.Distributor_Bar", "DistributorId");
            AddForeignKey("dbo.Distributor_Bar", "DistributorId", "dbo.Distributors", "DistributorId");
        }
    }
}
