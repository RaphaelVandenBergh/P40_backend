using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4._0_backend.Models
{
    public class Sensor
    {
        public int ID { get; set; }
        public double Temprature { get; set; }
        public double Humidity { get; set; }
        public double CO2 { get; set; }
        public double TVOC { get; set; }
        public DateTime timestamp { get; set; }
    }
}
