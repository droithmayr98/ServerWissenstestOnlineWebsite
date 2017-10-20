using DB_lib.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public TestDB_Context() : base("TestDB_Context") {

        }

    }
}
