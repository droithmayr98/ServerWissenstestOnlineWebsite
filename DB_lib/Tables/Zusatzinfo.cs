﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_lib.Tables
{
    [Table("Zusatzinfos")]
    public class Zusatzinfo
    {
        [Key]
        public int Zusatzinfo_Id { get; set; }
        [Required]
        public string Zusatzinfo_Name { get; set; }
        [Required]
        public virtual Typendefinition Typ {get; set;}

        public virtual List<Aufgabe> Aufgaben { get; set; }
        public virtual List<InfoContent> InfoContentM { get; set; }
    }
}
