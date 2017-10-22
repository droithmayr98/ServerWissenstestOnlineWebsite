using DB_lib.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_lib
{
    public class TestDB_Context : DbContext
    {
        public DbSet<Admintable> Admins { get; set; }
        public DbSet<Bezirk> Bezirke { get; set; }
        public DbSet<Ort> Orte { get; set; }

        public DbSet<Station> Stationen { get; set; }
        public DbSet<Stufe> Stufen { get; set; }
        public DbSet<Hintergrundbild> Hintergrundbilder { get; set; }

        public DbSet<Typendefinition> Typendefinitionen { get; set; }

        public DbSet<Frage> Fragen { get; set; }
        public DbSet<Zusatzinfo> Zusatzinfos { get; set; }
        public DbSet<Antwort> Antworten { get; set; }
        
        public DbSet<Aufgabe> Aufgaben { get; set; }

        public DbSet<InfoContent> InfoContentM { get; set; }
        public DbSet<Antwort_Text> Antwort_Texte { get; set; }
        public DbSet<Antwort_CheckBox> Antwort_CheckBoxes { get; set; }
        public DbSet<Antwort_DatePicker> Antwort_DatePickerM { get; set; }
        public DbSet<Antwort_RadioButton> Antwort_RadioButtons { get; set; }
        public DbSet<Antwort_Slider> Antwort_Sliders { get; set; }
        public DbSet<Antwort_Verbinden> Antwort_VerbindenM { get; set; }
        public DbSet<CheckBox> CheckBoxes { get; set; }
        public DbSet<RadioButton> RadioButtons { get; set; }
        public DbSet<Paar> Paare { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public TestDB_Context() : base("TestDB") {

        }

    }
}
