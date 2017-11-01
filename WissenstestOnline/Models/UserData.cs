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
        public static List<Aufgabe> Aufgaben = new List<Aufgabe>();

    }
}
