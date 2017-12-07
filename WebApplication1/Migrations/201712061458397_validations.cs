namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CreditCards", "CCNum", c => c.String(nullable: false));
            AlterColumn("dbo.CreditCards", "HolderName", c => c.String(nullable: false));
            AlterColumn("dbo.CreditCards", "cvc", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CreditCards", "cvc", c => c.Int(nullable: false));
            AlterColumn("dbo.CreditCards", "HolderName", c => c.String());
            AlterColumn("dbo.CreditCards", "CCNum", c => c.String());
        }
    }
}
