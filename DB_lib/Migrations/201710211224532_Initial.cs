namespace DB_lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Admin_Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Can_create_acc = c.Boolean(nullable: false),
                        Can_delete_acc = c.Boolean(nullable: false),
                        Can_edit_acc = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Admin_Id);
            
            CreateTable(
                "dbo.Antworten",
                c => new
                    {
                        Antwort_Id = c.Int(nullable: false, identity: true),
                        Typ_Typ_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Antwort_Id)
                .ForeignKey("dbo.Typendefinitionen", t => t.Typ_Typ_Id)
                .Index(t => t.Typ_Typ_Id);
            
            CreateTable(
                "dbo.Aufgaben",
                c => new
                    {
                        Aufgabe_Id = c.Int(nullable: false, identity: true),
                        Pflichtaufgabe = c.Boolean(nullable: false),
                        TeilaufgabeVon = c.Int(nullable: false),
                        AufgabeBezirk = c.String(),
                        AufgabeOrt = c.String(),
                        Antwort_Antwort_Id = c.Int(),
                        Frage_Frage_Id = c.Int(),
                        Zusatzinfo_Zusatzinfo_Id = c.Int(),
                        Hintergrundbild_Hintergrundbild_Id = c.Int(),
                        Station_Station_Id = c.Int(),
                        Stufe_Stufe_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Aufgabe_Id)
                .ForeignKey("dbo.Antworten", t => t.Antwort_Antwort_Id)
                .ForeignKey("dbo.Fragen", t => t.Frage_Frage_Id)
                .ForeignKey("dbo.Zusatzinfos", t => t.Zusatzinfo_Zusatzinfo_Id)
                .ForeignKey("dbo.Hintergrundbilder", t => t.Hintergrundbild_Hintergrundbild_Id)
                .ForeignKey("dbo.Stationen", t => t.Station_Station_Id)
                .ForeignKey("dbo.Stufen", t => t.Stufe_Stufe_Id)
                .Index(t => t.Antwort_Antwort_Id)
                .Index(t => t.Frage_Frage_Id)
                .Index(t => t.Zusatzinfo_Zusatzinfo_Id)
                .Index(t => t.Hintergrundbild_Hintergrundbild_Id)
                .Index(t => t.Station_Station_Id)
                .Index(t => t.Stufe_Stufe_Id);
            
            CreateTable(
                "dbo.Fragen",
                c => new
                    {
                        Frage_Id = c.Int(nullable: false, identity: true),
                        Fragetext = c.String(),
                        Fragebild = c.String(),
                        Fragevideo = c.String(),
                        Typ_Typ_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Frage_Id)
                .ForeignKey("dbo.Typendefinitionen", t => t.Typ_Typ_Id)
                .Index(t => t.Typ_Typ_Id);
            
            CreateTable(
                "dbo.Typendefinitionen",
                c => new
                    {
                        Typ_Id = c.Int(nullable: false, identity: true),
                        Typ = c.String(),
                    })
                .PrimaryKey(t => t.Typ_Id);
            
            CreateTable(
                "dbo.Zusatzinfos",
                c => new
                    {
                        Zusatzinfo_Id = c.Int(nullable: false, identity: true),
                        Typ_Typ_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Zusatzinfo_Id)
                .ForeignKey("dbo.Typendefinitionen", t => t.Typ_Typ_Id)
                .Index(t => t.Typ_Typ_Id);
            
            CreateTable(
                "dbo.Hintergrundbilder",
                c => new
                    {
                        Hintergrundbild_Id = c.Int(nullable: false, identity: true),
                        Bild = c.String(),
                    })
                .PrimaryKey(t => t.Hintergrundbild_Id);
            
            CreateTable(
                "dbo.Stationen",
                c => new
                    {
                        Station_Id = c.Int(nullable: false, identity: true),
                        Stationsname = c.String(),
                    })
                .PrimaryKey(t => t.Station_Id);
            
            CreateTable(
                "dbo.Stufen",
                c => new
                    {
                        Stufe_Id = c.Int(nullable: false, identity: true),
                        Stufenname = c.String(),
                    })
                .PrimaryKey(t => t.Stufe_Id);
            
            CreateTable(
                "dbo.Bezirke",
                c => new
                    {
                        Bezirk_Id = c.Int(nullable: false, identity: true),
                        Bezirksname = c.String(),
                    })
                .PrimaryKey(t => t.Bezirk_Id);
            
            CreateTable(
                "dbo.Orte",
                c => new
                    {
                        Ort_Id = c.Int(nullable: false, identity: true),
                        Ortsname = c.String(),
                        Bezirk_Bezirk_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Ort_Id)
                .ForeignKey("dbo.Bezirke", t => t.Bezirk_Bezirk_Id)
                .Index(t => t.Bezirk_Bezirk_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orte", "Bezirk_Bezirk_Id", "dbo.Bezirke");
            DropForeignKey("dbo.Aufgaben", "Stufe_Stufe_Id", "dbo.Stufen");
            DropForeignKey("dbo.Aufgaben", "Station_Station_Id", "dbo.Stationen");
            DropForeignKey("dbo.Aufgaben", "Hintergrundbild_Hintergrundbild_Id", "dbo.Hintergrundbilder");
            DropForeignKey("dbo.Zusatzinfos", "Typ_Typ_Id", "dbo.Typendefinitionen");
            DropForeignKey("dbo.Aufgaben", "Zusatzinfo_Zusatzinfo_Id", "dbo.Zusatzinfos");
            DropForeignKey("dbo.Fragen", "Typ_Typ_Id", "dbo.Typendefinitionen");
            DropForeignKey("dbo.Antworten", "Typ_Typ_Id", "dbo.Typendefinitionen");
            DropForeignKey("dbo.Aufgaben", "Frage_Frage_Id", "dbo.Fragen");
            DropForeignKey("dbo.Aufgaben", "Antwort_Antwort_Id", "dbo.Antworten");
            DropIndex("dbo.Orte", new[] { "Bezirk_Bezirk_Id" });
            DropIndex("dbo.Zusatzinfos", new[] { "Typ_Typ_Id" });
            DropIndex("dbo.Fragen", new[] { "Typ_Typ_Id" });
            DropIndex("dbo.Aufgaben", new[] { "Stufe_Stufe_Id" });
            DropIndex("dbo.Aufgaben", new[] { "Station_Station_Id" });
            DropIndex("dbo.Aufgaben", new[] { "Hintergrundbild_Hintergrundbild_Id" });
            DropIndex("dbo.Aufgaben", new[] { "Zusatzinfo_Zusatzinfo_Id" });
            DropIndex("dbo.Aufgaben", new[] { "Frage_Frage_Id" });
            DropIndex("dbo.Aufgaben", new[] { "Antwort_Antwort_Id" });
            DropIndex("dbo.Antworten", new[] { "Typ_Typ_Id" });
            DropTable("dbo.Orte");
            DropTable("dbo.Bezirke");
            DropTable("dbo.Stufen");
            DropTable("dbo.Stationen");
            DropTable("dbo.Hintergrundbilder");
            DropTable("dbo.Zusatzinfos");
            DropTable("dbo.Typendefinitionen");
            DropTable("dbo.Fragen");
            DropTable("dbo.Aufgaben");
            DropTable("dbo.Antworten");
            DropTable("dbo.Admins");
        }
    }
}
