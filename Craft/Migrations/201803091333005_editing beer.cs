namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editingbeer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beers", "BeerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Beers", "BeerName");
        }
    }
}
