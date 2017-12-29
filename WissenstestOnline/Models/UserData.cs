using DB_lib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.Models
{
    public static class UserData
    {
        public static string Bezirk { get; set; } = "";
        public static string Ort { get; set; } = "";
        public static string Stufe { get; set; } = "";
        public static string Mode { get; set; } = "";
        public static string Stations { get; set; } = "";
        public static int AufgabeNr { get; set; } = 0;
        public static List<Aufgabe> Aufgaben { get; set; } = new List<Aufgabe>();
        public static int AufgabenCount { get; set; } = 0;
        public static string AktuelleStation { get; set; } = "";
        public static string pressedButtonLearn { get; set; } = "";
        public static string AntwortTyp { get; set; } = "";
        public static Aufgabe Aufgabe { get; set; } = new Aufgabe();
        public static int PracticePoints { get; set; } = 0;

        public static List<Antwort> AufgabenInputs { get; set; } = new List<Antwort>();
    }
}
