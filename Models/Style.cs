using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4._0_backend.Models
{
    public class Style
    {
        public int ID { get; set; }

        public string PrimaryColor { get; set; }

        public string AccentColor { get; set; }
        public string LogoUrl { get; set; }
        public bool Active { get; set; }
    }
}
