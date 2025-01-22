namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeemployee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employers", "EmployerId", "dbo.Users");
            DropForeignKey("dbo.Employees", "EmployerId", "dbo.Employers");
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Employees", "EmployeeId", "dbo.Users");
            DropIndex("dbo.Employees", new[] { "EmployeeId" });
            DropIndex("dbo.Employees", new[] { "EmployerId" });
            DropIndex("dbo.Employers", new[] { "EmployerId" });
            DropPrimaryKey("dbo.Employees");
            AddColumn("dbo.Employees", "User_UserId", c => c.Int());
            AddColumn("dbo.Employees", "Employer_UserId", c => c.Int());
            AddColumn("dbo.Employees", "User_UserId1", c => c.Int());
            AlterColumn("dbo.Employees", "EmployeeId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Employees", "EmployeeId");
            CreateIndex("dbo.Employees", "User_UserId");
            CreateIndex("dbo.Employees", "Employer_UserId");
            CreateIndex("dbo.Employees", "User_UserId1");
            AddForeignKey("dbo.Employees", "User_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Employees", "Employer_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Employees", "PositionId", "dbo.Positions", "PositionId", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "User_UserId1", "dbo.Users", "UserId");
            DropColumn("dbo.Employees", "Email");
            DropColumn("dbo.Employees", "Dob");
            DropTable("dbo.Employers");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.EmployerId);
            
            AddColumn("dbo.Employees", "Dob", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employees", "Email", c => c.String());
            DropForeignKey("dbo.Employees", "User_UserId1", "dbo.Users");
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Employees", "Employer_UserId", "dbo.Users");
            DropForeignKey("dbo.Employees", "User_UserId", "dbo.Users");
            DropIndex("dbo.Employees", new[] { "User_UserId1" });
            DropIndex("dbo.Employees", new[] { "Employer_UserId" });
            DropIndex("dbo.Employees", new[] { "User_UserId" });
            DropPrimaryKey("dbo.Employees");
            AlterColumn("dbo.Employees", "EmployeeId", c => c.Int(nullable: false));
            DropColumn("dbo.Employees", "User_UserId1");
            DropColumn("dbo.Employees", "Employer_UserId");
            DropColumn("dbo.Employees", "User_UserId");
            AddPrimaryKey("dbo.Employees", "EmployeeId");
            CreateIndex("dbo.Employers", "EmployerId");
            CreateIndex("dbo.Employees", "EmployerId");
            CreateIndex("dbo.Employees", "EmployeeId");
            AddForeignKey("dbo.Employees", "EmployeeId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Employees", "PositionId", "dbo.Positions", "PositionId");
            AddForeignKey("dbo.Employees", "EmployerId", "dbo.Employers", "EmployerId");
            AddForeignKey("dbo.Employers", "EmployerId", "dbo.Users", "UserId");
        }
    }
}
