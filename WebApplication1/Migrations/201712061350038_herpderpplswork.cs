namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class herpderpplswork : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HolderName = c.String(),
                        cvc = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UsserAccounts", "cardId", c => c.Int());
            CreateIndex("dbo.UsserAccounts", "cardId");
            AddForeignKey("dbo.UsserAccounts", "cardId", "dbo.CreditCards", "Id");
            DropColumn("dbo.UsserAccounts", "CreditCardNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UsserAccounts", "CreditCardNumber", c => c.Double(nullable: false));
            DropForeignKey("dbo.UsserAccounts", "cardId", "dbo.CreditCards");
            DropIndex("dbo.UsserAccounts", new[] { "cardId" });
            DropColumn("dbo.UsserAccounts", "cardId");
            DropTable("dbo.CreditCards");
        }
    }
}
