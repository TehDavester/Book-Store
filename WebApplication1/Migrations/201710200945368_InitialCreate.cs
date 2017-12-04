namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ISBN10 = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Author = c.String(nullable: false),
                        Publishdate = c.DateTime(nullable: false),
                        Price = c.Single(nullable: false),
                        Stock = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ctgs", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ctgs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.bks", "CategoryId", "dbo.ctgs");
            DropIndex("dbo.bks", new[] { "CategoryId" });
            DropTable("dbo.ctgs");
            DropTable("dbo.bks");
        }
    }
}
