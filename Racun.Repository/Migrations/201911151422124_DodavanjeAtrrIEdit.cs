namespace IzdavanjeFaktura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodavanjeAtrrIEdit : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Fakturas", new[] { "StvarateljRacuna_Id" });
            DropColumn("dbo.Fakturas", "StvarateljRacunaID");
            RenameColumn(table: "dbo.Fakturas", name: "StvarateljRacuna_Id", newName: "StvarateljRacunaID");
            AddColumn("dbo.Fakturas", "Placeno", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Fakturas", "StvarateljRacunaID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Fakturas", "StvarateljRacunaID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Fakturas", new[] { "StvarateljRacunaID" });
            AlterColumn("dbo.Fakturas", "StvarateljRacunaID", c => c.Int(nullable: false));
            DropColumn("dbo.Fakturas", "Placeno");
            RenameColumn(table: "dbo.Fakturas", name: "StvarateljRacunaID", newName: "StvarateljRacuna_Id");
            AddColumn("dbo.Fakturas", "StvarateljRacunaID", c => c.Int(nullable: false));
            CreateIndex("dbo.Fakturas", "StvarateljRacuna_Id");
        }
    }
}
