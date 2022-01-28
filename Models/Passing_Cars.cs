using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4._0_backend.Models
{
    public class Passing_Cars
    {
        public int ID { get; set; }

        public int Amount_cars { get; set; }

        public int Amount_trucks { get; set; }
        public int Amount_bikers { get; set; }
        public int Amount_motorcycle { get; set; }
        public int Amount_bus { get; set; }

        public DateTime timestamp { get; set; }
    }
}
