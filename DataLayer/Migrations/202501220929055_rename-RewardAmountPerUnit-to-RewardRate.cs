namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameRewardAmountPerUnittoRewardRate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Positions", "RewardRate", c => c.Single(nullable: false));
            DropColumn("dbo.Positions", "RewardAmountPerUnit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Positions", "RewardAmountPerUnit", c => c.Single(nullable: false));
            DropColumn("dbo.Positions", "RewardRate");
        }
    }
}
