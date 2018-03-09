namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bars : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beers", "Distributor_DistributorId", c => c.Int());
            AddColumn("dbo.Distributors", "EmailAddress", c => c.String());
            AddColumn("dbo.Distributors", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Beers", "Distributor_DistributorId");
            CreateIndex("dbo.Distributors", "UserId");
            AddForeignKey("dbo.Beers", "Distributor_DistributorId", "dbo.Distributors", "DistributorId");
            AddForeignKey("dbo.Distributors", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Distributors", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Beers", "Distributor_DistributorId", "dbo.Distributors");
            DropIndex("dbo.Distributors", new[] { "UserId" });
            DropIndex("dbo.Beers", new[] { "Distributor_DistributorId" });
            DropColumn("dbo.Distributors", "UserId");
            DropColumn("dbo.Distributors", "EmailAddress");
            DropColumn("dbo.Beers", "Distributor_DistributorId");
        }
    }
}
