namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fuckingvs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CreditCards", "CCNum", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CreditCards", "CCNum");
        }
    }
}
