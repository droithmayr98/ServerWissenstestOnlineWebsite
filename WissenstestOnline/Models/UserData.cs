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
        public static List<DB_lib.Aufgabe> Aufgaben { get; set; } = new List<DB_lib.Aufgabe>();
        public static int AufgabenCount { get; set; } = 0;
        public static string AktuelleStation { get; set; } = "";
        public static string pressedButtonLearn { get; set; } = "";
        public static string AntwortTyp { get; set; } = "";
        public static DB_lib.Aufgabe Aufgabe { get; set; } = new DB_lib.Aufgabe();
        public static int PracticePoints { get; set; } = 0;
        public static bool lastPracticeAufgabeCorrect { get; set; } = false;
    }
}
