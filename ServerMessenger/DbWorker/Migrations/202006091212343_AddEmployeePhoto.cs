namespace DbWorker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeePhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "Photo");
        }
    }
}
