using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DB_lib.Tables
{
    public class Station
    {
        [Key]
        public int Station_Id { get; set; }
        public string Stationsname { get; set; }
        public virtual List<Aufgabe> Aufgaben { get; set; }
    }
}
