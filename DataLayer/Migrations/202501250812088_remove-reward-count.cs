namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removerewardcount : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "RewardCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "RewardCount", c => c.Int(nullable: false));
        }
    }
}
