namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bar2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bars",
                c => new
                    {
                        BarId = c.Int(nullable: false, identity: true),
                        BarName = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.BarId);
            
            AddColumn("dbo.Beers", "Bar_BarId", c => c.Int());
            CreateIndex("dbo.Beers", "Bar_BarId");
            AddForeignKey("dbo.Beers", "Bar_BarId", "dbo.Bars", "BarId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Beers", "Bar_BarId", "dbo.Bars");
            DropIndex("dbo.Beers", new[] { "Bar_BarId" });
            DropColumn("dbo.Beers", "Bar_BarId");
            DropTable("dbo.Bars");
        }
    }
}
