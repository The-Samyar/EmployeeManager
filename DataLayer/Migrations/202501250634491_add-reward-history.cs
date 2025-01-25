namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrewardhistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RewardHistories",
                c => new
                    {
                        RewardHistoryId = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Rate = c.Double(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        EditedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RewardHistoryId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RewardHistories", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.RewardHistories", new[] { "EmployeeId" });
            DropTable("dbo.RewardHistories");
        }
    }
}
