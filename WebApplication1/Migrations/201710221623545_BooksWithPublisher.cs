namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BooksWithPublisher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsserAccounts", "Phone_Number", c => c.Int(nullable: false));
            AddColumn("dbo.bks", "Publisher", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.bks", "Publisher");
            DropColumn("dbo.UsserAccounts", "Phone_Number");
        }
    }
}
