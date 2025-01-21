namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        HiringDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        PositionId = c.Int(nullable: false),
                        EmployerId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Dob = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Users", t => t.EmployeeId)
                .ForeignKey("dbo.Employers", t => t.EmployerId)
                .ForeignKey("dbo.Positions", t => t.PositionId)
                .Index(t => t.EmployeeId)
                .Index(t => t.PositionId)
                .Index(t => t.EmployerId);
            
            CreateTable(
                "dbo.Employers",
                c => new
                    {
                        EmployerId = c.Int(nullable: false),
                        CompanyName = c.String(),
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Dob = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmployerId)
                .ForeignKey("dbo.Users", t => t.EmployerId)
                .Index(t => t.EmployerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PasswordHash = c.String(),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        RewardAmountPerUnit = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.PositionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Employees", "EmployerId", "dbo.Employers");
            DropForeignKey("dbo.Employers", "EmployerId", "dbo.Users");
            DropForeignKey("dbo.Employees", "EmployeeId", "dbo.Users");
            DropIndex("dbo.Employers", new[] { "EmployerId" });
            DropIndex("dbo.Employees", new[] { "EmployerId" });
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropIndex("dbo.Employees", new[] { "EmployeeId" });
            DropTable("dbo.Positions");
            DropTable("dbo.Users");
            DropTable("dbo.Employers");
            DropTable("dbo.Employees");
        }
    }
}
