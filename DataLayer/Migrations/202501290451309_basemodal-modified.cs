namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basemodalmodified : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "EditedAt", c => c.DateTime());
            AlterColumn("dbo.Positions", "EditedAt", c => c.DateTime());
            AlterColumn("dbo.Users", "EditedAt", c => c.DateTime());
            AlterColumn("dbo.RewardHistories", "EditedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RewardHistories", "EditedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "EditedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Positions", "EditedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "EditedAt", c => c.DateTime(nullable: false));
        }
    }
}
