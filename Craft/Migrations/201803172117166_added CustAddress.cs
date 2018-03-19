namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCustAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bars", "CustAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bars", "CustAddress");
        }
    }
}
