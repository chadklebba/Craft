namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingfavoritestodb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        FavoriteId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        BeerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FavoriteId)
                .ForeignKey("dbo.Beers", t => t.BeerId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.BeerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorites", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Favorites", "BeerId", "dbo.Beers");
            DropIndex("dbo.Favorites", new[] { "BeerId" });
            DropIndex("dbo.Favorites", new[] { "CustomerId" });
            DropTable("dbo.Favorites");
        }
    }
}
