namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtexttoreward : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RewardHistories", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RewardHistories", "Message");
        }
    }
}
