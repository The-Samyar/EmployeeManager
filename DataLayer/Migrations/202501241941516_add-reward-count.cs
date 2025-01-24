namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrewardcount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "RewardCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "RewardCount");
        }
    }
}
