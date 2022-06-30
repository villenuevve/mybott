using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace botChef
{
    public class Modelio
    {
        public int id { get; set; }
        public int recipesUsed { get; set; }
        public Calories calories { get; set; }

    }

    public class Everythin
    {
        public long Id { get; set; }
        public long? ChatId { get; set; }
        public string Types { get; set; }
        public string Recipe { get; set; }
       
    }

    public class Data
    {
        public string title { get; set; }
        public string ingredients { get; set; }
        public string instructions { get; set; }
    }

    public class Calories
    {
        public string value { get; set; }
        public string unit { get; set; }
        public double standardDeviation { get; set; }
        public ConfidenceRange95Percent confidenceRange95Percent { get; set; }

        public class ConfidenceRange95Percent
        {
            public double min { get; set; }
            public double max { get; set; }
        }

    }
}


