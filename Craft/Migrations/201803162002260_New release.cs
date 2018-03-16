namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newrelease : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bars", "NewRelease", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bars", "NewRelease");
        }
    }
}
