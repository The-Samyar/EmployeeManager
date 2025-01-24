namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        HiringDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                        PositionId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        EditedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Users", t => t.ManagerId, cascadeDelete: false)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ManagerId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        IsManager = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        EditedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        RewardRate = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.PositionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "UserId", "dbo.Users");
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Employees", "ManagerId", "dbo.Users");
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropIndex("dbo.Employees", new[] { "ManagerId" });
            DropIndex("dbo.Employees", new[] { "UserId" });
            DropTable("dbo.Positions");
            DropTable("dbo.Users");
            DropTable("dbo.Employees");
        }
    }
}
