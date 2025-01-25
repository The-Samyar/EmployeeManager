namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateposition : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Positions", "RewardRate", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Positions", "RewardRate", c => c.Single(nullable: false));
        }
    }
}
