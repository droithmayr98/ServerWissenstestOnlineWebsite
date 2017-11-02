using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WissenstestOnlineWebseite.Models
{
    public class AntwortSlider_Model
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Sprungweite { get; set; }
        public int RightVal { get; set; }
        public string Slider_Text { get; set; } = "";
    }
}
