namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfavoritetobeer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beers", "Favorite", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Beers", "Favorite");
        }
    }
}
