namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabasewiththeAccountTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsserAccounts",
                c => new
                    {
                        UsedId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        CreditCardNumber = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UsedId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UsserAccounts");
        }
    }
}
