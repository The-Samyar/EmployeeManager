namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addattributes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Employees", "Employer_UserId", "dbo.Users");
            DropForeignKey("dbo.Employees", "User_UserId1", "dbo.Users");
            DropIndex("dbo.Employees", new[] { "User_UserId" });
            DropIndex("dbo.Employees", new[] { "Employer_UserId" });
            DropIndex("dbo.Employees", new[] { "User_UserId1" });
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ManagerId);
            
            AddColumn("dbo.Employees", "ManagerId", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "Email", c => c.String());
            AddColumn("dbo.Employees", "Username", c => c.String());
            AddColumn("dbo.Employees", "Password", c => c.String());
            CreateIndex("dbo.Employees", "ManagerId");
            AddForeignKey("dbo.Employees", "ManagerId", "dbo.Managers", "ManagerId", cascadeDelete: true);
            DropColumn("dbo.Employees", "UserId");
            DropColumn("dbo.Employees", "EmployerId");
            DropColumn("dbo.Employees", "User_UserId");
            DropColumn("dbo.Employees", "Employer_UserId");
            DropColumn("dbo.Employees", "User_UserId1");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Employees", "User_UserId1", c => c.Int());
            AddColumn("dbo.Employees", "Employer_UserId", c => c.Int());
            AddColumn("dbo.Employees", "User_UserId", c => c.Int());
            AddColumn("dbo.Employees", "EmployerId", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Employees", "ManagerId", "dbo.Managers");
            DropIndex("dbo.Employees", new[] { "ManagerId" });
            DropColumn("dbo.Employees", "Password");
            DropColumn("dbo.Employees", "Username");
            DropColumn("dbo.Employees", "Email");
            DropColumn("dbo.Employees", "ManagerId");
            DropTable("dbo.Managers");
            CreateIndex("dbo.Employees", "User_UserId1");
            CreateIndex("dbo.Employees", "Employer_UserId");
            CreateIndex("dbo.Employees", "User_UserId");
            AddForeignKey("dbo.Employees", "User_UserId1", "dbo.Users", "UserId");
            AddForeignKey("dbo.Employees", "Employer_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Employees", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
