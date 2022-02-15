using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4._0_backend.Models
{
    public class Parkingspots
    {
        public int ID { get; set; }
        public int ParkingspotNumber { get; set; }
        public string LicensePlate { get; set; }
        public DateTime beginTijd { get; set; }
        public DateTime eindTijd { get; set; }
    }
}
