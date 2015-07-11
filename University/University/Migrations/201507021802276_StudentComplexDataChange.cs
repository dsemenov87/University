namespace University.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentComplexDataChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollment", "StudentID", "dbo.Student");
            RenameColumn(table: "dbo.Student", name: "FirstMidName", newName: "FirstName");
            DropPrimaryKey("dbo.Student");
            AlterColumn("dbo.Student", "StudentID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Student", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Student", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.Student", "StudentID");
            AddForeignKey("dbo.Enrollment", "StudentID", "dbo.Student", "StudentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollment", "StudentID", "dbo.Student");
            DropPrimaryKey("dbo.Student");
            AlterColumn("dbo.Student", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "StudentID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Student", "StudentID");
            RenameColumn(table: "dbo.Student", name: "FirstName", newName: "FirstMidName");
            AddForeignKey("dbo.Enrollment", "StudentID", "dbo.Student", "StudentID", cascadeDelete: true);
        }
    }
}
