namespace DB_lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHeadingInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InfoContentM", "Heading", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InfoContentM", "Heading");
        }
    }
}
