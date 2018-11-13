namespace Prac2mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedatatypeofbookidfrominttostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "BookID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "BookID", c => c.Int(nullable: false));
        }
    }
}
