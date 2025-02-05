namespace nimaptask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRowVersionToCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "RowVersion");
        }
    }
}
