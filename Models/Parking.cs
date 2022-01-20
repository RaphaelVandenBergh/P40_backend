using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4._0_backend.Models
{
    public class Parking
    {
        public int ID { get; set; }

        public int Free_spots { get; set; }

        public DateTime timestamp { get; set; }
    }
}
