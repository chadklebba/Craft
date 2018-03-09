namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class registerupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Name", c => c.String());
            DropColumn("dbo.Customers", "FirstName");
            DropColumn("dbo.Customers", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "LastName", c => c.String());
            AddColumn("dbo.Customers", "FirstName", c => c.String());
            DropColumn("dbo.Customers", "Name");
        }
    }
}
