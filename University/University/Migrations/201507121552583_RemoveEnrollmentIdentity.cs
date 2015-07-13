namespace University.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEnrollmentIdentity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enrollment", "EnrollmentID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enrollment", "EnrollmentID", c => c.Int(nullable: false, identity: true));
        }
    }
}
