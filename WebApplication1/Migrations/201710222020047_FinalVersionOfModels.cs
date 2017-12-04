namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalVersionOfModels : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UsserAccounts", "Phone_Number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UsserAccounts", "Phone_Number", c => c.Int(nullable: false));
        }
    }
}
