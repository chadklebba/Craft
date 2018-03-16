namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newreleasetobar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beers", "NewRelease", c => c.Boolean(nullable: false));
            DropColumn("dbo.Bars", "NewRelease");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bars", "NewRelease", c => c.Boolean(nullable: false));
            DropColumn("dbo.Beers", "NewRelease");
        }
    }
}
