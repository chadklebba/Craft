namespace Craft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmoretodistributor2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Distributors", "Street", c => c.String());
            AddColumn("dbo.Distributors", "City", c => c.String());
            AddColumn("dbo.Distributors", "State", c => c.String());
            AddColumn("dbo.Distributors", "ZipCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Distributors", "ZipCode");
            DropColumn("dbo.Distributors", "State");
            DropColumn("dbo.Distributors", "City");
            DropColumn("dbo.Distributors", "Street");
        }
    }
}
