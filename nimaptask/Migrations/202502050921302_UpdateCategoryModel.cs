namespace nimaptask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategoryModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "RowVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
    }
}
