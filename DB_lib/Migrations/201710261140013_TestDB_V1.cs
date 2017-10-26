namespace DB_lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestDB_V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Admin_Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Can_create_acc = c.Boolean(nullable: false),
                        Can_delete_acc = c.Boolean(nullable: false),
                        Can_edit_acc = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Admin_Id);
            
            CreateTable(
                "dbo.Antwort_CheckBoxen",
                c => new
                    {
                        Inhalt_Id = c.Int(nullable: false, identity: true),
                        Anzahl = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Inhalt_Id);
            
            CreateTable(
                "dbo.CheckBoxes",
                c => new
                    {
                        CheckBox_Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        CheckBoxVal = c.Boolean(nullable: false),
                        Antwort_CheckBox_Inhalt_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CheckBox_Id)
                .ForeignKey("dbo.Antwort_CheckBoxen", t => t.Antwort_CheckBox_Inhalt_Id)
                .Index(t => t.Antwort_CheckBox_Inhalt_Id);
            
            CreateTable(
                "dbo.Antwort_DatePickers",
                c => new
                    {
                        Inhalt_Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Inhalt_Id);
            
            CreateTable(
                "dbo.Antwort_RadioButtons",
                c => new
                    {
                        Inhalt_Id = c.Int(nullable: false, identity: true),
                        Anzahl = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Inhalt_Id);
            
            CreateTable(
                "dbo.RadioButtons",
                c => new
                    {
                        Radio_Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        IsTrue = c.Boolean(nullable: false),
                        Antwort_RadioButton_Inhalt_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Radio_Id)
                .ForeignKey("dbo.Antwort_RadioButtons", t => t.Antwort_RadioButton_Inhalt_Id)
                .Index(t => t.Antwort_RadioButton_Inhalt_Id);
            
            CreateTable(
                "dbo.Antwort_Sliders",
                c => new
                    {
                        Inhalt_Id = c.Int(nullable: false, identity: true),
                        Min_val = c.Int(nullable: false),
                        Max_val = c.Int(nullable: false),
                        Sprungweite = c.Int(nullable: false),
                        RightVal = c.Int(nullable: false),
                        Slider_text = c.String(),
                    })
                .PrimaryKey(t => t.Inhalt_Id);
            
            CreateTable(
                "dbo.Antwort_Texte",
                c => new
                    {
                        Inhalt_Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Inhalt_Id);
            
            CreateTable(
                "dbo.Antwort_VerbindenM",
                c => new
                    {
                        Inhalt_id = c.Int(nullable: false, identity: true),
                        Anzahl = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Inhalt_id);
            
            CreateTable(
                "dbo.Paare",
                c => new
                    {
                        Paar_Id = c.Int(nullable: false, identity: true),
                        Teil1 = c.String(nullable: false),
                        Teil2 = c.String(nullable: false),
                        Antwort_Verbinden_Inhalt_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Paar_Id)
                .ForeignKey("dbo.Antwort_VerbindenM", t => t.Antwort_Verbinden_Inhalt_id)
                .Index(t => t.Antwort_Verbinden_Inhalt_id);
            
            CreateTable(
                "dbo.Antworten",
                c => new
                    {
                        Antwort_Id = c.Int(nullable: false, identity: true),
                        Inhalt_Id = c.Int(nullable: false),
                        Typ_Typ_Id = c.Int(nullable: false),
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
                        AufgabeBezirk = c.String(),
                        AufgabeOrt = c.String(),
                        Antwort_Antwort_Id = c.Int(nullable: false),
                        Frage_Frage_Id = c.Int(nullable: false),
                        Hintergrundbild_Hintergrundbild_Id = c.Int(),
                        Station_Station_Id = c.Int(nullable: false),
                        Stufe_Stufe_Id = c.Int(nullable: false),
                        TeilaufgabeVon_Aufgabe_Id = c.Int(),
                        Zusatzinfo_Zusatzinfo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Aufgabe_Id)
                .ForeignKey("dbo.Antworten", t => t.Antwort_Antwort_Id)
                .ForeignKey("dbo.Fragen", t => t.Frage_Frage_Id)
                .ForeignKey("dbo.Hintergrundbilder", t => t.Hintergrundbild_Hintergrundbild_Id)
                .ForeignKey("dbo.Stationen", t => t.Station_Station_Id)
                .ForeignKey("dbo.Stufen", t => t.Stufe_Stufe_Id)
                .ForeignKey("dbo.Aufgaben", t => t.TeilaufgabeVon_Aufgabe_Id)
                .ForeignKey("dbo.Zusatzinfos", t => t.Zusatzinfo_Zusatzinfo_Id)
                .Index(t => t.Antwort_Antwort_Id)
                .Index(t => t.Frage_Frage_Id)
                .Index(t => t.Hintergrundbild_Hintergrundbild_Id)
                .Index(t => t.Station_Station_Id)
                .Index(t => t.Stufe_Stufe_Id)
                .Index(t => t.TeilaufgabeVon_Aufgabe_Id)
                .Index(t => t.Zusatzinfo_Zusatzinfo_Id);
            
            CreateTable(
                "dbo.Fragen",
                c => new
                    {
                        Frage_Id = c.Int(nullable: false, identity: true),
                        Fragetext = c.String(nullable: false),
                        Fragebild = c.String(),
                        Fragevideo = c.String(),
                        Typ_Typ_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Frage_Id)
                .ForeignKey("dbo.Typendefinitionen", t => t.Typ_Typ_Id)
                .Index(t => t.Typ_Typ_Id);
            
            CreateTable(
                "dbo.Typendefinitionen",
                c => new
                    {
                        Typ_Id = c.Int(nullable: false, identity: true),
                        Typ = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Typ_Id);
            
            CreateTable(
                "dbo.Zusatzinfos",
                c => new
                    {
                        Zusatzinfo_Id = c.Int(nullable: false, identity: true),
                        Typ_Typ_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Zusatzinfo_Id)
                .ForeignKey("dbo.Typendefinitionen", t => t.Typ_Typ_Id)
                .Index(t => t.Typ_Typ_Id);
            
            CreateTable(
                "dbo.InfoContentM",
                c => new
                    {
                        Inhalt_Id = c.Int(nullable: false, identity: true),
                        Info_Content = c.String(nullable: false),
                        Heading = c.String(),
                        Zusatzinfo_Zusatzinfo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Inhalt_Id)
                .ForeignKey("dbo.Zusatzinfos", t => t.Zusatzinfo_Zusatzinfo_Id)
                .Index(t => t.Zusatzinfo_Zusatzinfo_Id);
            
            CreateTable(
                "dbo.Hintergrundbilder",
                c => new
                    {
                        Hintergrundbild_Id = c.Int(nullable: false, identity: true),
                        Bild = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Hintergrundbild_Id);
            
            CreateTable(
                "dbo.Stationen",
                c => new
                    {
                        Station_Id = c.Int(nullable: false, identity: true),
                        Stationsname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Station_Id);
            
            CreateTable(
                "dbo.Stufen",
                c => new
                    {
                        Stufe_Id = c.Int(nullable: false, identity: true),
                        Stufenname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Stufe_Id);
            
            CreateTable(
                "dbo.Bezirke",
                c => new
                    {
                        Bezirk_Id = c.Int(nullable: false, identity: true),
                        Bezirksname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Bezirk_Id);
            
            CreateTable(
                "dbo.Orte",
                c => new
                    {
                        Ort_Id = c.Int(nullable: false, identity: true),
                        Ortsname = c.String(nullable: false),
                        Bezirk_Bezirk_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Ort_Id)
                .ForeignKey("dbo.Bezirke", t => t.Bezirk_Bezirk_Id)
                .Index(t => t.Bezirk_Bezirk_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orte", "Bezirk_Bezirk_Id", "dbo.Bezirke");
            DropForeignKey("dbo.Antworten", "Typ_Typ_Id", "dbo.Typendefinitionen");
            DropForeignKey("dbo.Aufgaben", "Zusatzinfo_Zusatzinfo_Id", "dbo.Zusatzinfos");
            DropForeignKey("dbo.Aufgaben", "TeilaufgabeVon_Aufgabe_Id", "dbo.Aufgaben");
            DropForeignKey("dbo.Aufgaben", "Stufe_Stufe_Id", "dbo.Stufen");
            DropForeignKey("dbo.Aufgaben", "Station_Station_Id", "dbo.Stationen");
            DropForeignKey("dbo.Aufgaben", "Hintergrundbild_Hintergrundbild_Id", "dbo.Hintergrundbilder");
            DropForeignKey("dbo.Aufgaben", "Frage_Frage_Id", "dbo.Fragen");
            DropForeignKey("dbo.Fragen", "Typ_Typ_Id", "dbo.Typendefinitionen");
            DropForeignKey("dbo.Zusatzinfos", "Typ_Typ_Id", "dbo.Typendefinitionen");
            DropForeignKey("dbo.InfoContentM", "Zusatzinfo_Zusatzinfo_Id", "dbo.Zusatzinfos");
            DropForeignKey("dbo.Aufgaben", "Antwort_Antwort_Id", "dbo.Antworten");
            DropForeignKey("dbo.Paare", "Antwort_Verbinden_Inhalt_id", "dbo.Antwort_VerbindenM");
            DropForeignKey("dbo.RadioButtons", "Antwort_RadioButton_Inhalt_Id", "dbo.Antwort_RadioButtons");
            DropForeignKey("dbo.CheckBoxes", "Antwort_CheckBox_Inhalt_Id", "dbo.Antwort_CheckBoxen");
            DropIndex("dbo.Orte", new[] { "Bezirk_Bezirk_Id" });
            DropIndex("dbo.InfoContentM", new[] { "Zusatzinfo_Zusatzinfo_Id" });
            DropIndex("dbo.Zusatzinfos", new[] { "Typ_Typ_Id" });
            DropIndex("dbo.Fragen", new[] { "Typ_Typ_Id" });
            DropIndex("dbo.Aufgaben", new[] { "Zusatzinfo_Zusatzinfo_Id" });
            DropIndex("dbo.Aufgaben", new[] { "TeilaufgabeVon_Aufgabe_Id" });
            DropIndex("dbo.Aufgaben", new[] { "Stufe_Stufe_Id" });
            DropIndex("dbo.Aufgaben", new[] { "Station_Station_Id" });
            DropIndex("dbo.Aufgaben", new[] { "Hintergrundbild_Hintergrundbild_Id" });
            DropIndex("dbo.Aufgaben", new[] { "Frage_Frage_Id" });
            DropIndex("dbo.Aufgaben", new[] { "Antwort_Antwort_Id" });
            DropIndex("dbo.Antworten", new[] { "Typ_Typ_Id" });
            DropIndex("dbo.Paare", new[] { "Antwort_Verbinden_Inhalt_id" });
            DropIndex("dbo.RadioButtons", new[] { "Antwort_RadioButton_Inhalt_Id" });
            DropIndex("dbo.CheckBoxes", new[] { "Antwort_CheckBox_Inhalt_Id" });
            DropTable("dbo.Orte");
            DropTable("dbo.Bezirke");
            DropTable("dbo.Stufen");
            DropTable("dbo.Stationen");
            DropTable("dbo.Hintergrundbilder");
            DropTable("dbo.InfoContentM");
            DropTable("dbo.Zusatzinfos");
            DropTable("dbo.Typendefinitionen");
            DropTable("dbo.Fragen");
            DropTable("dbo.Aufgaben");
            DropTable("dbo.Antworten");
            DropTable("dbo.Paare");
            DropTable("dbo.Antwort_VerbindenM");
            DropTable("dbo.Antwort_Texte");
            DropTable("dbo.Antwort_Sliders");
            DropTable("dbo.RadioButtons");
            DropTable("dbo.Antwort_RadioButtons");
            DropTable("dbo.Antwort_DatePickers");
            DropTable("dbo.CheckBoxes");
            DropTable("dbo.Antwort_CheckBoxen");
            DropTable("dbo.Admins");
        }
    }
}
