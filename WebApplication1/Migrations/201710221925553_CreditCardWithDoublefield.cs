namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreditCardWithDoublefield : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UsserAccounts", "CreditCardNumber", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UsserAccounts", "CreditCardNumber", c => c.Int(nullable: false));
        }
    }
}
