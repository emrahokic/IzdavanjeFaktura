namespace IzdavanjeFaktura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fakturas",
                c => new
                    {
                        FakturaID = c.Int(nullable: false, identity: true),
                        BrojFakture = c.String(),
                        DatumFakture = c.DateTime(nullable: false),
                        DatumPlacanjaFakture = c.DateTime(nullable: false),
                        UkupnaCijenaBezPoreza = c.Double(nullable: false),
                        UkupnaCijenaSaPorezom = c.Double(nullable: false),
                        StvarateljRacunaID = c.Int(nullable: false),
                        NazivPrimatelja = c.String(),
                        PorezID = c.Int(nullable: false),
                        StvarateljRacuna_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FakturaID)
                .ForeignKey("dbo.Porezs", t => t.PorezID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StvarateljRacuna_Id)
                .Index(t => t.PorezID)
                .Index(t => t.StvarateljRacuna_Id);
            
            CreateTable(
                "dbo.Porezs",
                c => new
                    {
                        PorezID = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Iznos = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PorezID);
            
            CreateTable(
                "dbo.FakturaStavkas",
                c => new
                    {
                        FakturaStavkaID = c.Int(nullable: false, identity: true),
                        FakturaID = c.Int(nullable: false),
                        StavkaID = c.Int(nullable: false),
                        Kolicina = c.Int(nullable: false),
                        UkupnaCijenaBezPoreza = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.FakturaStavkaID)
                .ForeignKey("dbo.Fakturas", t => t.FakturaID, cascadeDelete: true)
                .ForeignKey("dbo.Stavkas", t => t.StavkaID, cascadeDelete: true)
                .Index(t => t.FakturaID)
                .Index(t => t.StavkaID);
            
            CreateTable(
                "dbo.Stavkas",
                c => new
                    {
                        StavkaID = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Cijena = c.Double(nullable: false),
                        Opis = c.String(),
                    })
                .PrimaryKey(t => t.StavkaID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Fakturas", "StvarateljRacuna_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FakturaStavkas", "StavkaID", "dbo.Stavkas");
            DropForeignKey("dbo.FakturaStavkas", "FakturaID", "dbo.Fakturas");
            DropForeignKey("dbo.Fakturas", "PorezID", "dbo.Porezs");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.FakturaStavkas", new[] { "StavkaID" });
            DropIndex("dbo.FakturaStavkas", new[] { "FakturaID" });
            DropIndex("dbo.Fakturas", new[] { "StvarateljRacuna_Id" });
            DropIndex("dbo.Fakturas", new[] { "PorezID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Stavkas");
            DropTable("dbo.FakturaStavkas");
            DropTable("dbo.Porezs");
            DropTable("dbo.Fakturas");
        }
    }
}
