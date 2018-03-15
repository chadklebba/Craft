namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlegendtobar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bars", "Legend", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bars", "Legend");
        }
    }
}
