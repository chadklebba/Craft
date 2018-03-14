namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bar_Beeraddition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bar_Beer",
                c => new
                    {
                        Bar_BeerId = c.Int(nullable: false, identity: true),
                        BeerId = c.Int(nullable: false),
                        BarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Bar_BeerId)
                .ForeignKey("dbo.Bars", t => t.BarId, cascadeDelete: true)
                .ForeignKey("dbo.Beers", t => t.BeerId, cascadeDelete: true)
                .Index(t => t.BeerId)
                .Index(t => t.BarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bar_Beer", "BeerId", "dbo.Beers");
            DropForeignKey("dbo.Bar_Beer", "BarId", "dbo.Bars");
            DropIndex("dbo.Bar_Beer", new[] { "BarId" });
            DropIndex("dbo.Bar_Beer", new[] { "BeerId" });
            DropTable("dbo.Bar_Beer");
        }
    }
}
