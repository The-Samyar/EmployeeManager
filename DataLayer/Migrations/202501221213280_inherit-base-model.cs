namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inheritbasemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EditedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employees", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Managers", "EditedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Managers", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Managers", "IsDeleted");
            DropColumn("dbo.Managers", "EditedAt");
            DropColumn("dbo.Employees", "IsDeleted");
            DropColumn("dbo.Employees", "EditedAt");
        }
    }
}
